SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE procedure [dbo].[usp_RepReconsYConteoMermas_CMO]
(
	@CVE_CPTO		int,
	@fecha_desde	date,
	@fecha_hasta	date
)
/*
set	@CVE_CPTO		= 60
set @fecha_desde	= '01-05-2014'
set @fecha_hasta	= '31-07-2014'
*/
as

set transaction isolation level read uncommitted
set nocount on

------------------------------------------
create table #tabla_maestra
(	[UID]			[int] IDENTITY (1, 1) not null,
	MODELO			[varchar](20),
	DESCRIPCION		[varchar](300),
	CANTIDAD		[float] default(0),
	COSTO			[float]	default(0),
	COMENTARIO		[varchar](300)
)
create unique clustered index idx on #tabla_maestra([UID])
declare @iCont	int
declare @iTot	int
set @iCont	= 1
set @iTot	= 0
------------------------------------------
------------------------------------------
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
declare @iCont2	int
declare @iTot2	int
set @iCont	= 1
set @iTot	= 0
------------------------------------------
insert into #tabla_maestra (MODELO,CANTIDAD,COSTO)
SELECT CVE_ART AS CLV_ART,
       SUM(CANT) AS CANTIDAD,
       COSTO
FROM aspel_sae50..MINVE01
WHERE CVE_CPTO = @CVE_CPTO
  AND convert(date,FECHA_DOCU) between @fecha_desde and @fecha_hasta
GROUP BY CVE_ART,
         COSTO HAVING SUM(CANT) <> 0
ORDER BY CVE_ART

set @iTot = @@rowcount
--
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
------------------------------------------
while @iCont <= @iTot
	begin
		--
		set @Costo = 0
		set @comentario = ''
		--se extrael el Modelo / Artículo de la table maestra
		select @articulo = MODELO from #tabla_maestra where [UID] = @iCont
		--
		insert into #componentes_PT_DET01 (COMPONENTE,CANTIDAD,COSTOU,TIPOCOMP)
		SELECT COMPONENTE,CANTIDAD,COSTOU,TIPOCOMP FROM aspel_prod30..PT_DET01 where CLAVE = @articulo
		set @iTot2	= @@rowcount
		set @iCont2 = 1
		while @iCont2 <= @iTot2
			begin
				SELECT	@COMPONENTE = COMPONENTE,@CANTIDAD = CANTIDAD,@TIPOCOMP = TIPOCOMP,@COSTOU = COSTOU FROM #componentes_PT_DET01 where [UID] = @iCont2
				if @TIPOCOMP = 49
					begin
						SELECT @CVE_ART = CVE_ART FROM aspel_sae50..INVE01 WHERE CVE_ART = @COMPONENTE AND LTRIM(RTRIM(LIN_PROD)) = 'MP'
						if @@rowcount = 1 --si es MP
							begin
								SELECT TOP 1 @CVE_ART = CVE_ART,@CostoQry = COSTO From aspel_sae50..MINVE01 where convert(date,FECHA_DOCU) <= @fecha_hasta AND CVE_ART = @COMPONENTE AND CVE_CPTO = 1 ORDER BY FECHA_DOCU DESC,NUM_MOV DESC	
								if @@rowcount = 1
									begin
										set @Costo = @Costo + @CANTIDAD * @CostoQry
									end
								else
									begin
										SELECT TOP 1 @CVE_ART = CVE_ART,@CostoQry = COSTO,@fecha_docu = FECHA_DOCU From aspel_sae50..MINVE01 where convert(date,FECHA_DOCU) >= @fecha_hasta AND CVE_ART = @COMPONENTE AND CVE_CPTO = 1 ORDER BY FECHA_DOCU ASC
										if @@rowcount = 1
											begin
												set @comentario = 'No hay compras ANTERIORES para el componente ' + @COMPONENTE + ' se costea con la compra del ' + cast(@fecha_docu as varchar(20))
												set @Costo = @Costo + @CANTIDAD * @CostoQry
											end
										else
											begin
												set @comentario = 'No hay compras ni ANTERIORES ni SIGUIENTES para el componente ' + @COMPONENTE
											end								
									end
							end
					/*else
							begin
								--no hay parte falsa para esta condición
							end*/
					end
				else
					begin
						set @Costo = @Costo + @COSTOU
					end
				set @iCont2 = @iCont2 + 1
			end
		delete #componentes_PT_DET01
		DBCC CHECKIDENT('#componentes_PT_DET01', RESEED, 0)
		--<actualizar aquí tabla principal>
		update #tabla_maestra set COSTO = @Costo,COMENTARIO = @comentario where [UID] = @iCont
		--</actualizar aquí tabla principal>
		set @iCont = @iCont + 1
	end
------------------------------------------

SELECT DESCR FROM aspel_sae50..CONM01 WHERE ltrim(rtrim(CVE_CPTO)) = @CVE_CPTO
select * from #tabla_maestra


drop table #tabla_maestra
drop table #componentes_PT_DET01
GO
