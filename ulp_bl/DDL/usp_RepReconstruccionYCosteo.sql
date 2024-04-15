ALTER procedure [dbo].[usp_RepReconstruccionYCosteo]
(
	@fecha as date
)
as

set transaction isolation level read uncommitted
set ansi_warnings off
set nocount on
declare @iTot	as int
set @iTot = 0;

create table #PP (
	PRODUCTO	[varchar](20),
	SUMA_PP		[float]		default(0)
)
create table #OP (
	PROD		[varchar](20),
	SUM_ENV		[float]		default(0)
)
create table #MI (
	PROD		[varchar](20),
	SUM_REC		[float]		default(0)
)
create table #TOTALES (
	Articulo		[varchar](20),
	Linea			[varchar](255),
	[PP Actual]		[decimal]		default(0),
	Recibidos		[decimal]		default(0),
	Enviados		[decimal]		default(0),
	[PP Recons]		[decimal](10,2)	default(0)
)
create table #componentes_49 (
	[articulo]		[varchar](20),
	[componente]	[varchar](20),
	[cantidad]		[float]	default(0)
)

create table #componentes_49_detalle (
	[articulo]		[varchar](20),
	[componente]	[varchar](20),
	[cantidad]		[float]	default(0)
)
create table #componentes_49_eof
(
	[articulo]		[varchar](20),	
	[observacion]	[varchar](300)
)

alter table #componentes_49_detalle add [UID] int identity(1,1)
alter table #componentes_49_detalle add constraint pk_det_uid primary key([UID])

insert into #PP SELECT	PRODUCTO,	SUM(CANTIDAD) - SUM(CANTTERM) AS SUMA_PP From aspel_prod30.dbo.ORD_FAB01 GROUP BY PRODUCTO
insert into #OP SELECT PRODUCTO AS PROD,SUM(CANTIDAD) AS SUM_ENV From aspel_prod30.dbo.ORD_FAB01 WHERE convert(date,FCAPTURA) >= @fecha AND STATUS <> 0 GROUP BY PRODUCTO
insert into #MI select CVE_ART AS ART,SUM(CANT) AS SUM_REC From aspel_sae50.dbo.MINVE01 WHERE convert(date,FECHA_DOCU) >= @fecha AND CVE_CPTO = '3' GROUP BY CVE_ART

insert into #TOTALES
SELECT
        CVE_ART AS CLV_ART,
        LIN_PROD,
        SUMA_PP,
        isnull(SUM_REC,0)	as	SUM_REC,
        isnull(SUM_ENV,0)	as	SUM_ENV,
        0
FROM
	aspel_sae50.dbo.INVE01
LEFT JOIN #PP
        ON CVE_ART = #PP.PRODUCTO COLLATE Modern_Spanish_CI_AS
LEFT JOIN #MI
        ON CVE_ART = #MI.PROD COLLATE Modern_Spanish_CI_AS
LEFT JOIN #OP
        ON CVE_ART = #OP.PROD COLLATE Modern_Spanish_CI_AS
WHERE TIPO_ELE <> 'S'
AND LIN_PROD <> 'INSU'
AND LIN_PROD <> 'HABI'
AND LIN_PROD <> 'MP'
AND LIN_PROD <> 'NINVE'
AND LIN_PROD <> 'ZABOH' --and CVE_ART in ('PJ12MSXZ3834')
AND (SUMA_PP IS NOT NULL
OR SUM_REC IS NOT NULL
OR SUM_ENV IS NOT NULL)
ORDER BY LIN_PROD



set @iTot = @@ROWCOUNT

delete #TOTALES where [PP Actual] + Recibidos + Enviados = 0


alter table #TOTALES add [UID] int identity(1,1)
alter table #TOTALES add constraint pk_totales_uid primary key([UID])


insert into #componentes_49
SELECT
	[CLAVE], 
    [COMPONENTE],
    [CANTIDAD]
    FROM aspel_prod30.dbo.PT_DET01 
 WHERE [CLAVE] in (select Articulo collate Modern_Spanish_CI_AS from #TOTALES) and [TIPOCOMP] = 49



declare @iCont int
set @iCont = 1

declare @componente	varchar(20)
declare @articulo	varchar(20)
declare @cantidad	float
set @componente = ''
set @articulo	= ''
set @cantidad = 0

create table #articulo_costo
(
	articulo	[varchar](20),
	costo		[float],
	fecha_docu	[date]
)




declare @costo	float
declare @iTotDetalle	int
declare @iContDetalle	int
declare @fecha_docu date

set @iContDetalle = 1
set @iTotDetalle = 0

while  @iCont <= @iTot
	begin		
		insert into #componentes_49_detalle
		select
			articulo, 
			componente,
			cantidad
		from
			#componentes_49
		where
			articulo in (
							select
								Articulo
							from
								#TOTALES
							where [UID] = @iCont
						)					
		select @iTotDetalle = count(#componentes_49_detalle.[UID]) from #componentes_49_detalle		
		if (@iTotDetalle > 0)
			begin
				set @iContDetalle = 1
				set @fecha_docu = null
				
				while @iContDetalle <= @iTotDetalle
					begin				
								select
									@articulo = articulo, 
									@componente = componente,
									@cantidad = cantidad
								from
									#componentes_49_detalle
								where [UID] = @iContDetalle						
								insert into #articulo_costo
								SELECT TOP 1
									@articulo,
									@cantidad * COSTO,
									null
								From
									aspel_sae50.dbo.MINVE01
								where
									convert(date,FECHA_DOCU) <= @fecha
									AND
									CVE_ART = @componente
									AND CVE_CPTO = 1
								ORDER BY
									FECHA_DOCU DESC,NUM_MOV DESC
								
								--select @componente as componente,@@rowcount as [rowcount]								
								if (@@rowcount = 0)
									begin										
										insert into #articulo_costo
											SELECT TOP 1
												@articulo,
												@cantidad * COSTO,
												convert(date,FECHA_DOCU)												
											From
												aspel_sae50.dbo.MINVE01
											where
												convert(date,FECHA_DOCU) >= @fecha AND CVE_ART = @componente AND CVE_CPTO = 1
											ORDER BY FECHA_DOCU ASC
											if (@@rowcount = 0)
												begin
													insert into #componentes_49_eof select @articulo,'No hay compras ni ANTERIORES ni SIGUIENTES para el componente '+ @componente
												end
											else
												begin
													SELECT TOP 1													
														@fecha_docu = convert(date,FECHA_DOCU)												
													From
														aspel_sae50.dbo.MINVE01
													where
														convert(date,FECHA_DOCU) >= @fecha AND CVE_ART = @componente AND CVE_CPTO = 1
													ORDER BY FECHA_DOCU ASC
													insert into #componentes_49_eof select @articulo,'No hay compras ANTERIORES para el componente ' + @componente + ' se costea con la compra del ' + cast(@fecha_docu as varchar(20))
												end
									end
									
								set @iContDetalle = @iContDetalle + 1						
					end					
					delete #componentes_49_detalle
					DBCC CHECKIDENT('#componentes_49_detalle', RESEED, 0)
			end
			set @iCont = @iCont + 1
	end

SELECT 
	#TOTALES.*,
	Costos.Costo,
	#componentes_49_eof.Observacion
FROM
	#TOTALES
inner join 
			(select 
					articulo,
					sum(costo) as costo
				from
					#articulo_costo
				group by
					articulo					
			) as Costos
			on
				#TOTALES.Articulo = Costos.articulo
left join
	#componentes_49_eof
	on 
	#componentes_49_eof.articulo = #TOTALES.Articulo
			order by
				#TOTALES.Articulo


drop table #PP
drop table #OP
drop table #MI
drop table #TOTALES
drop table #componentes_49
drop table #componentes_49_detalle
drop table #componentes_49_eof
drop table #articulo_costo