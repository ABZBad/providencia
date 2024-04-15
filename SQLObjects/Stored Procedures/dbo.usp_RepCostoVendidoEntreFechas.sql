SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE procedure [dbo].[usp_RepCostoVendidoEntreFechas]
(
	@fecha_desde	as date,
	@fecha_hasta	as date
)
as
set nocount on
set transaction isolation level read uncommitted
/*
creación de la tabla maestra
*/
---
create table #tabla_maestra
(	[UID]			[int] IDENTITY (1, 1) not null,
	MODELO			[varchar](20),
	DESCRIPCION		[varchar](300),
	FACTURA			[float] default(0),
	NC				[float]	default(0),
	COSTO			[float]	default(0),
	COMENTARIO		[varchar](300)
)
----variable con el total de registros a iterar, también se declara el contador del While
declare	@iTot	int
declare @iCont	int
----

--alter table #tabla_maestra add constraint pk_uid primary key([UID])
create unique clustered index idx on #tabla_maestra([UID])
---
insert into #tabla_maestra (MODELO,DESCRIPCION,FACTURA,NC)
SELECT
        CVE_ART AS MODELO,
        DESCR,
        ISNULL((SELECT
                SUM(FY.CANT)
        FROM aspel_sae50.dbo.FACTF01 F
        INNER JOIN aspel_sae50.dbo.PAR_FACTF01 FY
                ON F.CVE_DOC = FY.CVE_DOC
        WHERE FY.CVE_ART = I.CVE_ART
        AND F.TIP_DOC = 'F'
        AND F.STATUS <> 'C'
        --AND FECHA_DOC BETWEEN '20140701 00:00:00' AND '20140731 23:59:59'), 0) AS FACTURADAS,
        AND convert(date,FECHA_DOC) BETWEEN @fecha_desde AND @fecha_hasta), 0) AS FACTURADAS,
        ISNULL((SELECT
                SUM(FY.CANT)
        FROM aspel_sae50.dbo.FACTD01 F
        INNER JOIN aspel_sae50.dbo.PAR_FACTD01 FY
                ON F.CVE_DOC = FY.CVE_DOC
        WHERE FY.CVE_ART = I.CVE_ART
        AND F.TIP_DOC = 'D'
        AND F.STATUS <> 'C'
        --AND FECHA_DOC BETWEEN '20140701 00:00:00' AND '20140731 23:59:59'), 0) AS DEVUELTAS
        AND convert(date,FECHA_DOC) BETWEEN @fecha_desde AND @fecha_hasta), 0) AS DEVUELTAS
FROM aspel_sae50.dbo.INVE01 I
WHERE I.TIPO_ELE = 'P' --and CVE_ART IN ('BA65MOZY3001','BA65MOSH3601')
AND I.LIN_PROD <> 'MP'
AND (ISNULL((SELECT
        SUM(FY.CANT)
FROM aspel_sae50.dbo.FACTF01 F
INNER JOIN aspel_sae50.dbo.PAR_FACTF01 FY
        ON F.CVE_DOC = FY.CVE_DOC
WHERE FY.CVE_ART = I.CVE_ART
AND F.TIP_DOC = 'F'
AND F.STATUS <> 'C'
--AND FECHA_DOC BETWEEN '20140701 00:00:00' AND '20140731 23:59:59'), 0) <> 0
AND convert(date,FECHA_DOC) BETWEEN @fecha_desde AND @fecha_hasta), 0) <> 0
OR ISNULL((SELECT
        SUM(FY.CANT)
FROM aspel_sae50.dbo.FACTD01 F
INNER JOIN aspel_sae50.dbo.PAR_FACTD01 FY
        ON F.CVE_DOC = FY.CVE_DOC
WHERE FY.CVE_ART = I.CVE_ART
AND F.TIP_DOC = 'D'
AND F.STATUS <> 'C'
--AND FECHA_DOC BETWEEN '20140701 00:00:00' AND '20140731 23:59:59'), 0) <> 0)
AND convert(date,FECHA_DOC) BETWEEN @fecha_desde AND @fecha_hasta), 0) <> 0)
ORDER BY I.CVE_ART


set @iTot = @@rowcount

--------------------
/*sirve para almacenar los componentes según la clave del artículo / modelo*/
create table #componentes_PT_DET01
(
	COMPONENTE	[varchar](30),
	CANTIDAD	[float],
	TIPOCOMP	[int],
	COSTOU		[float]	
)
alter table #componentes_PT_DET01 add [UID] int identity(1,1)
create unique clustered index idx on #componentes_PT_DET01([UID])
--------------------
declare	@CLV_ART varchar(20)
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
declare @iTot2		int	--para almacenar el total de componentes x artículo
declare @iCont2		int	--para llevar el control de las iteraciones de los componentes
--------------------

/*se inicializa el contador del cliclo*/
----
set @iCont = 1
set @Costo = 0
----
/*se recorre la tabla maestra*/
----
while @iCont <= @iTot
	begin
		--print 'Vuelta : >' + convert(varchar(100),getdate(),114) + ' No. ' + cast(@iCont as varchar(200))
		/*inicialización de variables por c/ciclo */
		----
		--print 'antes de inicializaión>' + cast(@Costo as varchar(200))
		set @Costo = 0
		set @CostoQry = 0
		set @Comentario = ''
		--print 'después de inicializaión>' + cast(@Costo as varchar(200))
		----
		/*se graba en una temporal la lista de componentes según el artículo*/
		----
		--saco la clave del modelo a buscar
		select @CLV_ART = MODELO from #tabla_maestra where [UID] = @iCont
		--PRINT '----------------------------->' + @CLV_ART
		----
		--insert los componentes encontrados a la tabla temporal de componentes...
		insert into #componentes_PT_DET01		
		SELECT
				COMPONENTE,
				CANTIDAD,
				TIPOCOMP,
				COSTOU				
		FROM aspel_prod30..PT_DET01
		WHERE CLAVE = @CLV_ART
		AND (COMPONENTE NOT IN (SELECT
				CVE_ART
		FROM aspel_sae50.dbo.INVE01
		WHERE CVE_ART LIKE 'BOT%'
		OR CVE_ART LIKE 'CIN%'
		OR CVE_ART LIKE 'ESC%'
		OR CVE_ART LIKE 'ET%')
		AND COMPONENTE NOT IN (SELECT
				CLAVE
		FROM aspel_prod30..INSUMOS01
		WHERE CLAVE LIKE 'CINTA %'
		OR CLAVE LIKE 'EMPAQ%')
		)
		
		set @iTot2 = @@rowcount	--Obtengo el total de registros de los componente por artículo				
		----
		----select * from #componentes_PT_DET01
		----
		--Ahora recorro la tabla de los componentes:
		set @iCont2 = 1 --inicializo el contador que recorrerá los componentes x artículo / modelo
		while @iCont2 <= @iTot2
		begin
			select
				@COMPONENTE	=	COMPONENTE,
				@CANTIDAD	=	CANTIDAD,
				@TIPOCOMP	=	TIPOCOMP,
				@COSTOU		=	isnull(COSTOU,0)
			from
				#componentes_PT_DET01
			where
				[UID] = @iCont2			
			----
			if @TIPOCOMP = 49
				begin
					SELECT TOP 1 @CostoQry = isnull(CY.COST,0),@fecha_doc = C.FECHA_DOC FROM aspel_sae50.dbo.COMPC01 C INNER JOIN aspel_sae50.dbo.PAR_COMPC01 CY ON C.CVE_DOC=CY.CVE_DOC WHERE C.TIP_DOC='c' AND C.STATUS<>'C' AND LTRIM(RTRIM(CVE_ART)) = @COMPONENTE AND convert(date,C.FECHA_DOC) <= @fecha_hasta ORDER BY C.FECHA_DOC DESC, C.CVE_DOC DESC
					if @@rowcount = 0 --RS1
						begin
							--print 'no RS1 entra para ' + @componente
							SELECT TOP 1 @CostoQry = isnull(CY.COST,0),@fecha_doc = C.FECHA_DOC FROM aspel_sae50.dbo.COMPC01 C INNER JOIN aspel_sae50.dbo.PAR_COMPC01 CY ON C.CVE_DOC=CY.CVE_DOC WHERE C.TIP_DOC='c' AND C.STATUS<>'C' AND LTRIM(RTRIM(CVE_ART)) = @COMPONENTE AND convert(date,C.FECHA_DOC) > @fecha_hasta ORDER BY C.FECHA_DOC ASC, C.CVE_DOC ASC
							if @@rowcount = 0 --RS2
								begin
									--print 'no RS2 entra para ' + @componente
									--print '>>>>>>>' + cast(@CostoQry as varchar(100)) + '<<<<<<<'
									SELECT TOP 1 @CostoQry = COSTO, @fecha_doc = FECHA_DOCU FROM aspel_sae50.dbo.MINVE01 WHERE CVE_CPTO=1 AND LTRIM(RTRIM(CVE_ART)) = @componente AND convert(date,FECHA_DOCU) <= @fecha_hasta ORDER BY FECHA_DOCU DESC, NUM_MOV DESC												
									--print '>>>>>>>' + cast(@CostoQry as varchar(100)) + '<<<<<<<'
									if @@rowcount = 0 --RS3
										begin
											--print 'entró en NO RS3 ' + @componente
											SELECT TOP 1 @CostoQry = COSTO, @fecha_doc = FECHA_DOCU FROM aspel_sae50.dbo.MINVE01 WHERE CVE_CPTO=1 AND LTRIM(RTRIM(CVE_ART)) = @componente AND convert(date,FECHA_DOCU) > @fecha_hasta ORDER BY FECHA_DOCU ASC, NUM_MOV ASC
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
							--print 'Valor de Costo Antes para RS1>---' + cast(@Costo as varchar(200))
							--print 'Valor de CANTIDAD y COSTO para 49>---'+ cast(@CANTIDAD as varchar(200))+ ' * ' +cast(@CostoQry as varchar(200))
							set @Costo = @Costo + (@CANTIDAD * @CostoQry)
							--print 'Valor de Costo para RS1>' + cast(@Costo as varchar(200))
						end						
				end
			else
				begin
					--print 'Valor de Costo Antes para NO 49>---' + cast(@Costo as varchar(200))								
					--print 'Valor de CANTIDAD y COSTOU para NO 49>---'+ cast(@CANTIDAD as varchar(200))+ ' * ' +cast(@COSTOU as varchar(200))
					set @Costo = @Costo + (@CANTIDAD * @COSTOU)
					--print 'Valor de Costo para NO 49>---' + cast(@Costo as varchar(200))								
				end
			SET @TIPOCOMP	= 0
			SET @COSTOU		= 0
			SET @CANTIDAD	= 0
			SET @COMPONENTE	= ''
			set @CostoQry = 0
			set @iCont2 = @iCont2 + 1
		end
		----
		delete #componentes_PT_DET01
		DBCC CHECKIDENT('#componentes_PT_DET01', RESEED, 0)
		--print '004>' + cast(@iCont as varchar(300))
		--PRINT 'Costo antes de UPDATE>' + cast(@Costo as varchar(100))
		update #tabla_maestra set COSTO = @Costo,COMENTARIO = @Comentario where [UID] = @iCont
		---select * from #tabla_maestra
		----
		set @iCont = @iCont + 1
		--PRINT '------------------------------'
	end

SELECT * FROM #tabla_maestra


drop table #tabla_maestra
drop table #componentes_PT_DET01
GO
