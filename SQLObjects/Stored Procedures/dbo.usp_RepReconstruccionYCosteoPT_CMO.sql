SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE procedure [dbo].[usp_RepReconstruccionYCosteoPT_CMO]
(
	@fecha	date
)
as

set transaction isolation level read uncommitted
set nocount on

-----------------------------------------------------------------------
---variables para controlar el total de registros de la tabla principal
-----------------------------------------------------------------------
declare @iTotInsertados		int	
declare @iTotBorrados		int
declare	@iTot				int
declare @iCont				int

set @iTot				= 0
set	@iTotInsertados		= 0
set @iTotBorrados		= 0
-----------------------------------------------------------------------
/*Tabla principal que será devuelta*/
create table #tabla_maestra
(
	Articulo		[varchar](20),
	Existencia		float default(0),
	Entradas		float default(0),
	Salidas			float default(0),
	LIN_PROD		[varchar](20),
	Costo			float default(0),
	COMENTARIO		[varchar](300)
)
--------------------
/*sirve para almacenar los componentes según la clave del artículo / modelo*/
create table #componentes_PT_DET01
(
	COMPONENTE	[varchar](30),
	CANTIDAD	[float] default (0),
	TIPOCOMP	[smallint],
	COSTOU		[float]	 default (0)
)
alter table #componentes_PT_DET01 add [UID] int identity(1,1)
create unique clustered index idx on #componentes_PT_DET01([UID])
--------------------


--se inserta el universo de registros
insert into #tabla_maestra (Articulo,Existencia,LIN_PROD)
SELECT
        CVE_ART AS CLV_ART,
        isnull(EXIST,0),
        LIN_PROD
FROM aspel_sae50..INVE01
WHERE LIN_PROD <> 'MP'
	AND LIN_PROD <> 'HABI'
	AND LIN_PROD <> 'NINVE'
	AND LIN_PROD <> 'INSU'
ORDER BY CLV_ART
set @iTotInsertados	= @@rowcount

--se actualiza el total de Etradas
update #tabla_maestra set ENTRADAS = Entradas.ENTRADAS
from #tabla_maestra inner join (SELECT
			LTRIM(RTRIM(CVE_ART)) as Articulo,
			ISNULL(SUM(CANT), 0) AS ENTRADAS
		FROM aspel_sae50..MINVE01
		WHERE 
			CVE_CPTO >= 1
		AND
			CVE_CPTO <= 7
		AND
			convert(date,FECHA_DOCU) >= @fecha
		group by
			LTRIM(RTRIM(CVE_ART))) as Entradas
on
	#tabla_maestra.Articulo = Entradas.Articulo collate Latin1_General_BIN


--se actualiza el total de Salidas
update #tabla_maestra set SALIDAS = Salidas.SALIDAS
from #tabla_maestra inner join (SELECT
			LTRIM(RTRIM(CVE_ART)) as Articulo,
			ISNULL(SUM(CANT), 0) AS SALIDAS
		FROM aspel_sae50..MINVE01
		WHERE 
			CVE_CPTO >= 51
		AND
			CVE_CPTO <= 58
		AND
			convert(date,FECHA_DOCU) >= @fecha
		group by
			LTRIM(RTRIM(CVE_ART))) as Salidas
on
	#tabla_maestra.Articulo = Salidas.Articulo collate Latin1_General_BIN


delete #tabla_maestra where Existencia + Entradas + Salidas = 0
set @iTotBorrados	= @@rowcount


alter table #tabla_maestra add [UID] int identity(1,1)
create unique clustered index idx on #tabla_maestra([UID])

------------------------------------------------------
--			se recorre la tabla principal			--
------------------------------------------------------
set @iTot	= @iTotInsertados - @iTotBorrados			--<----total de registros a iterar
set @iCont	= 1
------------------------------------------------------
--			variables a usar dentro del ciclo
------------------------------------------------------
declare @Costo		float
declare @CostoQry	float
declare @fecha_docu	date
declare @CVE_ART	varchar(20)
declare @comentario	varchar(300)

declare @excepcion	varchar(100)
declare @articulo	varchar(20)

declare	@COMPONENTE	varchar(20)
declare @CANTIDAD	float	
declare @TIPOCOMP	smallint
declare @COSTOU		float

declare @iTot2		int	--para almacenar el total de componentes x artículo
declare @iCont2		int	--para llevar el control de las iteraciones de los componentes

set @excepcion = ''
set @articulo = ''
set @comentario = ''
set @iTot2 = 0
set @iCont2 = 0
------------------------------------------------------
while (@iCont <= @iTot)
	begin
		set @Costo = 0
		set @CostoQry = 0		
		------------------------------
		--se extrae el componente del regirtso actual
		------------------------------
		select @articulo = Articulo from #tabla_maestra where [UID] = @iCont
		------------------------------
		--se consultan las excepciones
		------------------------------
		SELECT 
			@excepcion = LOWER(RTrim(LTrim(CAMPLIB1)))
		FROM
			aspel_sae50..INVE_CLIB01
		WHERE
			CVE_PROD = @articulo
		------------------------------		
		if @excepcion = 'zapato' or @excepcion = 'playera'	------<-----------si hay excepciones
			begin
				-----------------------------------------------------------
				--NOTA ORIGINAL DE VB:
				/*
							'Consulta de Costo Sin ir a PROD
                            'Se toma como base la programación original de SIP haciendo ajustes como en de la línea de abjo, donde se omite la condición
                            'de que la línea del producto sea MP (Materi Prima)
                            'strSQL = "SELECT CLV_ART FROM INVE01 WHERE CLV_ART =  '" & adoComponentesRS!COMPONENTE & "' AND LTRIM(RTRIM(LIN_PROD)) = 'MP'"
				*/
				
				SELECT @CVE_ART = CVE_ART FROM aspel_sae50..INVE01 WHERE CVE_ART =  @articulo
				if @@rowcount = 1
					begin
						SELECT TOP 1 @CostoQry = COSTO From aspel_sae50..MINVE01 where convert(date,FECHA_DOCU) <= @fecha AND CVE_ART = @articulo AND CVE_CPTO = 1 ORDER BY FECHA_DOCU DESC,NUM_MOV DESC
						if @@rowcount = 1
							begin
								set @Costo = @Costo + @CostoQry
							end
						else
							begin
								SELECT TOP 1 @CostoQry = COSTO,@fecha_docu = FECHA_DOCU From aspel_sae50..MINVE01 where convert(date,FECHA_DOCU) >= @fecha AND CVE_ART = @articulo AND CVE_CPTO = 1 ORDER BY FECHA_DOCU ASC
								if @@rowcount = 1
									begin
										set @comentario = 'No hay compras ANTERIORES para el producto '  + @articulo + ' se costea con la compra del ' + cast(@fecha_docu as varchar(15))
                                        set @Costo = @Costo + @CostoQry
									end
								else
									begin
										set @comentario = 'No hay compras ni ANTERIORES ni SIGUIENTES para el producto ' + @articulo
									end
							end
					end
				/*else
					begin
						-------------------------------------------------------------------------
						--		El código original en VB no tiene parte falsa para esta condición
						-------------------------------------------------------------------------
					end*/
			end
		else
			begin
				----------------------
				--	Ini Costeo SIP	--
				----------------------
				
				----------------------------------------------------------
				--	se inserta el detalle de componetes por artículo	--
				----------------------------------------------------------				
				insert into #componentes_PT_DET01
				SELECT
					COMPONENTE,
					CANTIDAD,
					TIPOCOMP,
					COSTOU
				FROM
					aspel_prod30..PT_DET01
				where CLAVE = @articulo
				set @iTot2 = @@rowcount	--Obtengo el total de registros de los componente por artículo										
				--Ahora recorro la tabla de los componentes:
				set @iCont2 = 1 --inicializo el contador que recorrerá los componentes x artículo / modelo
				while @iCont2 <= @iTot2
					begin
						SELECT	@COMPONENTE = COMPONENTE,@CANTIDAD = CANTIDAD,@TIPOCOMP = TIPOCOMP,@COSTOU = COSTOU FROM #componentes_PT_DET01 where [UID] = @iCont2
						if @TIPOCOMP = 49
							begin
								--------------------------------------------------------------------------
								--NOTA: DE VB... 'VALIDO QUE SI JUEGUE PARA LA ESTRUCTURA EL COMPONENTE...
								--------------------------------------------------------------------------
								SELECT @CVE_ART = CVE_ART FROM aspel_sae50..INVE01 WHERE CVE_ART =  @componente
								--------------------------------------------------------------------------
								if @@rowcount = 1 --<-----NOTA DE VB: ...'SI ES MP
									begin										
										SELECT TOP 1 @CostoQry = COSTO From aspel_sae50..MINVE01 where convert(date,FECHA_DOCU) <= @fecha AND CVE_ART = @componente AND CVE_CPTO = 1 ORDER BY FECHA_DOCU DESC,NUM_MOV DESC
										if @@rowcount = 1
											begin
												---select '001'
												set @Costo	= @Costo + @CANTIDAD * @CostoQry
											end
										else
											begin
												SELECT TOP 1 @CostoQry = COSTO,@fecha_docu = FECHA_DOCU From aspel_sae50..MINVE01 where convert(date,FECHA_DOCU) >= @fecha AND CVE_ART = @componente AND CVE_CPTO = 1 ORDER BY FECHA_DOCU ASC
												if @@rowcount = 1
													begin
														----select '002'
														set @comentario = 'No hay compras ANTERIORES para el componente ' + @componente + ' se costea con la compra del ' + cast(@fecha_docu as varchar(20))
														set @Costo = @Costo + @CANTIDAD * @CostoQry
													end
												else
													begin														
														set @comentario = 'No hay compras ni ANTERIORES ni SIGUIENTES para el componente ' + @componente
														---select '003',@comentario as comment
													end
											end
									end
								/*else									
									begin
										--El código original en VB no tiene parte falsa para esta condición
									end
								*/
							end
						else
							begin
								---select '004'
								set @Costo	= @Costo + @COSTOU * @CANTIDAD
							end
						set @CostoQry = 0						
						set @iCont2 = @iCont2 + 1
					end
				delete #componentes_PT_DET01
				DBCC CHECKIDENT('#componentes_PT_DET01', RESEED, 0)
			end
		--print 'iCont===>' + cast(@iCont as varchar(100)) + '< Costo:>>>>>>>>' + cast(@Costo as varchar(50))
		--select @comentario as comment
		update #tabla_maestra set COSTO = @Costo,COMENTARIO = @Comentario where [UID] = @iCont
		set @comentario = ''
		set @iCont	= @iCont + 1
	end

select * from #tabla_maestra

drop table #tabla_maestra
drop table #componentes_PT_DET01

GO
