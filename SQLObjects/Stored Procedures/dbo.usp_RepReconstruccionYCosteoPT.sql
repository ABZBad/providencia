SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE procedure [dbo].[usp_RepReconstruccionYCosteoPT]
	(
		@fecha	date
	)
as

--set @fecha = '01-08-2014'

set transaction isolation level read uncommitted
set ansi_warnings off
set nocount on
create table #tabla_maestra
(	
	CLV_ART		[varchar](20),
	EXIST		[float],
	LIN_PROD	[varchar](30),
	ENTRADAS	[float]	default(0),
	SALIDAS		[float] default(0),
	COSTO		[float]	default(0),
	COMENTARIO	[varchar](300)
)


--se obtiene el total general
declare @iTotGral	as int
set @iTotGral = 0

insert into #tabla_maestra
SELECT 
	CVE_ART				AS CLV_ART,
	isnull(EXIST,0)		AS EXIST,
	LIN_PROD,
	0,
	0,
	0,
	null
FROM
	aspel_sae50.dbo.INVE01 inve
WHERE
	/*CVE_ART = 'CAPRVOTJ4801'
	AND*/
	LIN_PROD <> 'MP' AND LIN_PROD <> 'HABI' AND LIN_PROD <> 'NINVE' AND LIN_PROD <> 'INSU' AND TIPO_ELE='P' ORDER BY CLV_ART

set @iTotGral = @@rowcount



UPDATE #tabla_maestra SET #tabla_maestra.ENTRADAS = Entradas.ENTRADAS
from (
SELECT
	LTRIM(RTRIM(CVE_ART)) AS CVE_ART,
	ISNULL(SUM(CANT),0) AS ENTRADAS
From
	aspel_sae50.dbo.MINVE01
WHERE
	SIGNO =  1 and convert(date,FECHA_DOCU) >= @fecha
group by LTRIM(RTRIM(CVE_ART))
) as Entradas INNER JOIN #tabla_maestra on #tabla_maestra.CLV_ART = Entradas.CVE_ART collate Latin1_General_BIN

UPDATE #tabla_maestra SET #tabla_maestra.SALIDAS = Salidas.SALIDAS
from (
SELECT
	LTRIM(RTRIM(CVE_ART)) AS CVE_ART,
	ISNULL(SUM(CANT),0) AS SALIDAS
From
	aspel_sae50.dbo.MINVE01
WHERE
	SIGNO =  -1 and convert(date,FECHA_DOCU) >= @fecha
group by LTRIM(RTRIM(CVE_ART))
) as Salidas INNER JOIN #tabla_maestra on #tabla_maestra.CLV_ART = Salidas.CVE_ART collate Latin1_General_BIN
----
declare @iTotBorrados	as int
set @iTotBorrados = 0
delete #tabla_maestra where Exist + Entradas + Salidas = 0
set @iTotBorrados = @@rowcount
----
alter table #tabla_maestra add [UID] int identity(1,1)
create unique clustered index idx on #tabla_maestra([UID])

----
----Contadores de los While
declare @iCont	as	int
declare @iCont2	as	int
declare @iTot	as	int
declare @iTot2	as	int
set @iTot = @iTotGral - @iTotBorrados

----
set @iCont	= 1	--contador del while superior
set @iCont2	= 0	--contador del while anidado
declare @CLV_ART	varchar(20)
--------------------
declare @COMPONENTE	varchar(30)
declare @CANTIDAD	float
declare @TIPOCOMP	int
declare @COSTOU		float
--------------------
declare @Costo		float
declare @CostoQry	float
declare	@Comentario	varchar(300)
declare @fecha_doc	date
--------------------
create table #componentes_PT_DET01
(
	COMPONENTE	[varchar](30),
	CANTIDAD	[float],
	TIPOCOMP	[int],
	COSTOU		[float]	
)
--------------------
alter table #componentes_PT_DET01 add [UID] int identity(1,1)
create unique clustered index idx on #componentes_PT_DET01([UID])
--------------------
while @iCont <= @iTot
	begin
		--print 'contador: ' + cast(@iCont as varchar(10))
		----
		set @Costo = 0
		set @CostoQry = 0
		set @Comentario = ''
		---
		select @CLV_ART = CLV_ART from #tabla_maestra where [UID] = @iCont
		---
		insert into #componentes_PT_DET01
		select
			COMPONENTE,
			CANTIDAD,
			TIPOCOMP,
			COSTOU
		from aspel_prod30.dbo.PT_DET01 where CLAVE = @CLV_ART
		----DBG:
		--SELECT * FROM #componentes_PT_DET01
		---
		set @iTot2 = @@rowcount	--Obtengo el total de registros de los componente por artículo		
		---
		if @iTot2 > 0	--si hay componentes...
			begin				
				set @iCont2 = 1
				while @iCont2 <= @iTot2
					begin						
						----
						select
							@COMPONENTE	=	COMPONENTE,
							@CANTIDAD	=	CANTIDAD,
							@TIPOCOMP	=	TIPOCOMP,
							@COSTOU		=	isnull(COSTOU,0)
						from
							#componentes_PT_DET01
						where
							[UID] = @iCont2
						----DBG:
						--SELECT @COMPONENTE AS [COMPONENTE ACTUAL],@TIPOCOMP AS [TIPO COMPONENTE],@CANTIDAD as [CANTIDAD],@COSTOU as [COSTOU], @@ROWCOUNT AS NUM_ROWS,@iCont2 AS [iCont]
						----
						if @TIPOCOMP = 49
							begin
								--SELECT 'ENTRÓ'
								SELECT TOP 1 @CostoQry = isnull(CY.COST,0),@fecha_doc = C.FECHA_DOC FROM aspel_sae50.dbo.COMPC01 C INNER JOIN aspel_sae50.dbo.PAR_COMPC01 CY ON C.CVE_DOC=CY.CVE_DOC WHERE C.TIP_DOC='c' AND C.STATUS<>'C' AND LTRIM(RTRIM(CVE_ART)) = @COMPONENTE AND convert(date,C.FECHA_DOC) <= @fecha ORDER BY C.FECHA_DOC DESC, C.CVE_DOC DESC
								if @@rowcount = 0 --RS1
									begin
										--print 'no RS1 entra para ' + @componente
										SELECT TOP 1 @CostoQry = isnull(CY.COST,0),@fecha_doc = C.FECHA_DOC FROM aspel_sae50.dbo.COMPC01 C INNER JOIN aspel_sae50.dbo.PAR_COMPC01 CY ON C.CVE_DOC=CY.CVE_DOC WHERE C.TIP_DOC='c' AND C.STATUS<>'C' AND LTRIM(RTRIM(CVE_ART)) = @COMPONENTE AND convert(date,C.FECHA_DOC) > @fecha ORDER BY C.FECHA_DOC ASC, C.CVE_DOC ASC
										if @@rowcount = 0 --RS2
											begin
												--print 'no RS2 entra para ' + @componente
												--print '>>>>>>>' + cast(@CostoQry as varchar(100)) + '<<<<<<<'
												SELECT TOP 1 @CostoQry = COSTO, @fecha_doc = FECHA_DOCU FROM aspel_sae50.dbo.MINVE01 WHERE CVE_CPTO=1 AND LTRIM(RTRIM(CVE_ART)) = @componente AND convert(date,FECHA_DOCU) <= @fecha ORDER BY FECHA_DOCU DESC, NUM_MOV DESC												
												--print '>>>>>>>' + cast(@CostoQry as varchar(100)) + '<<<<<<<'
												if @@rowcount = 0 --RS3
													begin
														--print 'entró en NO RS3 ' + @componente
														SELECT TOP 1 @CostoQry = COSTO, @fecha_doc = FECHA_DOCU FROM aspel_sae50.dbo.MINVE01 WHERE CVE_CPTO=1 AND LTRIM(RTRIM(CVE_ART)) = @componente AND convert(date,FECHA_DOCU) > @fecha ORDER BY FECHA_DOCU ASC, NUM_MOV ASC
														if @@rowcount = 0 --RS4
															begin																
																set @Comentario = 'No hay Compras Efectivas Anteriores ni Posteriores; ni Movimientos de Compra por Ajuste Anteriores ni Posteriores para el Componente ''' + @componente + '''.'
																--set @Costo = @Costo + 0
															end
														else
															begin
																set @Comentario = 'No hay Compras Efectivas Anteriores ni Posteriores; ni Movimientos de Compra por Ajuste Anteriores para el Componente ''' + @componente + '''. Se costea con el Movimiento de Compra por Ajuste del ' + cast(@fecha_doc as varchar(20))
																set @Costo = @Costo + (@CANTIDAD * @CostoQry)
															end							
													end
												else
													begin
														--print 'entró en RS3 ' + @componente
														--print 'Cantidad : ' + cast(@CANTIDAD as varchar(100)) + ' + ' + 'CostoQry : ' + cast(@CostoQry as varchar(100))
														set @Comentario = 'No hay Compras Efectivas Anteriores ni Posteriores para el Componente ''' + @componente + '''. Se costea con el Movimiento de Compra por Ajuste del ' + cast(@fecha_doc as varchar(20))
														--print 'Costo antes : ' + cast(@Costo as varchar(100))
														set @Costo = @Costo + (@CANTIDAD * @CostoQry)
														--print 'Costo después : ' + cast(@Costo as varchar(100))
													end
											end
										else
											begin
												set @Comentario = 'No hay Compras Efectivas Anteriores para el Componente ''' + @componente + '''. Se costea con la compra del ' + cast(@fecha_doc as varchar(20))												
												set @Costo = @Costo + (@CANTIDAD * @CostoQry)												
											end											
									end
								else
									begin										
										set @Costo = @Costo + (@CANTIDAD * @CostoQry)
										--PRINT cast(@CANTIDAD as varchar(10)) + ' * ' + cast(@CostoQry as varchar(10)) + ' * ' + cast(@Costo as varchar(10))
									end
							end
						else
							begin
								--SELECT 'NO ES 49'
								set @Costo = @Costo + @COSTOU
								--PRINT cast(@COSTOU as varchar(10)) + ' + ' + cast(@Costo as varchar(10))
							end
						----
						SET @TIPOCOMP	= 0
						SET @COSTOU		= 0
						SET @CANTIDAD	= 0
						SET @COMPONENTE	= ''					
						set @iCont2 = @iCont2 + 1						
					end
			end			
		----
		delete #componentes_PT_DET01
		DBCC CHECKIDENT('#componentes_PT_DET01', RESEED, 0)
		----
		update #tabla_maestra set COSTO = @Costo,COMENTARIO = @Comentario where [UID] = @iCont
						----
		set @iCont = @iCont + 1
		----------------------------
		
		
		
		
		
	end
----
select * from #tabla_maestra


drop table #tabla_maestra
drop table #componentes_PT_DET01
GO
