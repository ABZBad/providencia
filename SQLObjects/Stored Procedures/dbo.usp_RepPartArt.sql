SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_RepPartArt]
	@fechaInicial DateTime,
	@fechaFinal DateTime,
	@tipoReporte int
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL	READ UNCOMMITTED
	SET NOCOUNT ON;
	DECLARE @Rango int
	
	
	if @tipoReporte = 1 -- POR LINEA
	set @Rango=6
	
	else if @tipoReporte = 2 -- POR MODELO
	set @Rango=8
	    
	else if @tipoReporte = 3  -- POR TALLA
	set @Rango=16
	

	
	--set @Rango=6
/*	
select INVENTARIO.MODELO ,DESCRIPCION = (select top 1 DESCR from aspel_sae50.dbo.INVE01 where SUBSTRING(CVE_ART,1,@Rango) = INVENTARIO.MODELO), ISNULL(FACTURAS.PRENDAS,0) FACTURA, ISNULL(NOTASCREDITO.PRENDAS,0) NC, (ISNULL(FACTURAS.PRENDAS,0)-ISNULL(NOTASCREDITO.PRENDAS,0))SUMA from 

(
SELECT SUBSTRING(CVE_ART,1,@Rango) AS MODELO FROM aspel_sae50.dbo.INVE01

WHERE TIPO_ELE='P' AND LIN_PROD<>'MP'
--AND SUBSTRING(CVE_ART,1,@Rango) in('BA65MO','BA80BL','BA80MO')
GROUP BY SUBSTRING(CVE_ART,1,@Rango)
) as INVENTARIO

INNER JOIN 

(
SELECT SUBSTRING(FY.CVE_ART,1,@Rango) AS MODELO, SUM(CANT) AS PRENDAS 
FROM aspel_sae50.dbo.FACTF01 F 
JOIN aspel_sae50.dbo.PAR_FACTF01 FY ON F.CVE_DOC=FY.CVE_DOC 
JOIN aspel_sae50.dbo.INVE01 I ON LTRIM(RTRIM(I.CVE_ART))=LTRIM(RTRIM(FY.CVE_ART))
WHERE FY.TIPO_PROD='P' AND LTRIM(RTRIM(LIN_PROD))<>'MP' AND convert(datetime2,convert(varchar(255),FECHA_DOC,102),102) between @FechaInicial AND @FechaFinal AND F.STATUS<>'C'
--AND SUBSTRING(FY.CVE_ART,1,@Rango) in('BA65MO','BA80BL','BA80MO')
GROUP BY SUBSTRING(FY.CVE_ART,1,@Rango)
HAVING SUM(CANT) <> 0
) as FACTURAS

ON SUBSTRING(INVENTARIO.MODELO,1,@Rango) = SUBSTRING(FACTURAS.MODELO,1,@Rango)

left JOIN 

(
SELECT SUBSTRING(FY.CVE_ART,1,@Rango) AS MODELO, SUM(CANT) AS PRENDAS
FROM aspel_sae50.dbo.FACTD01 F 
JOIN aspel_sae50.dbo.PAR_FACTD01 FY ON F.CVE_DOC=FY.CVE_DOC 
JOIN aspel_sae50.dbo.INVE01 I ON LTRIM(RTRIM(I.CVE_ART))=LTRIM(RTRIM(FY.CVE_ART))
WHERE FY.TIPO_PROD='P' AND LTRIM(RTRIM(LIN_PROD))<>'MP' AND convert(datetime2,convert(varchar(255),FECHA_DOC,102),102) between @FechaInicial AND @FechaFinal AND F.STATUS<>'C'
--AND SUBSTRING(FY.CVE_ART,1,@Rango) in('BA65MO','BA80BL','BA80MO')
GROUP BY SUBSTRING(FY.CVE_ART,1,@Rango)
HAVING SUM(CANT) <> 0
) as NOTASCREDITO
ON SUBSTRING(INVENTARIO.MODELO,1,@Rango) = SUBSTRING(NOTASCREDITO.MODELO,1,@Rango)

GROUP BY INVENTARIO.MODELO, FACTURAS.PRENDAS, NOTASCREDITO.PRENDAS


ORDER BY INVENTARIO.MODELO
*/



create table #modelos (
	[id]		[int] identity(1,2) not null,
	[modelo]	[varchar](50) not null
)

create table #facturas (
	[id]		[int] identity(1,2) not null,
	[modelo]	[varchar](50) not null,
	[prendas]	[int] default(0)
)

create table #notas_credito (
	[id]		[int] identity(1,2) not null,
	[modelo]	[varchar](50) not null,
	[prendas]	[int] default(0)
)
create table #participacion_por_articulo (
	--[id]			[int] identity(1,2) not null,
	[modelo]		[varchar](50) not null,
	[descripcion]	[varchar](50) not null,
	[factura]		[int] default(0),
	[nc]			[int] default(0),
	[suma]			[int] default(0),
	[porcentaje]	[decimal](9,7) default(0) 
)


insert into #modelos SELECT rtrim(ltrim(SUBSTRING(CVE_ART,1,@Rango))) AS MODELO FROM aspel_sae50.dbo.INVE01 WHERE TIPO_ELE='P' AND LIN_PROD<>'MP' GROUP BY SUBSTRING(CVE_ART,1,@Rango) ORDER BY SUBSTRING(CVE_ART,1,@Rango)



insert into #facturas
SELECT 
	rtrim(ltrim(SUBSTRING(FY.CVE_ART,1,@Rango))) AS MODELO,
	SUM(CANT) AS PRENDAS
FROM
		aspel_sae50.dbo.FACTF01 F
	JOIN
		aspel_sae50.dbo.PAR_FACTF01 FY
	ON 
		F.CVE_DOC=FY.CVE_DOC
	JOIN
		aspel_sae50.dbo.INVE01 I
	ON LTRIM(RTRIM(I.CVE_ART))=LTRIM(RTRIM(FY.CVE_ART))
WHERE
		FY.TIPO_PROD='P'
	AND
		LTRIM(RTRIM(LIN_PROD))<>'MP'
	AND
		convert(datetime2,convert(varchar(255),FECHA_DOC,102),102) between @fechaInicial AND @fechaFinal
	AND 
		F.STATUS<>'C'
GROUP BY
	SUBSTRING(FY.CVE_ART,1,@Rango)


insert into #notas_credito
SELECT rtrim(ltrim(SUBSTRING(FY.CVE_ART,1,@Rango))) AS MODELO, SUM(CANT) AS PRENDAS FROM aspel_sae50.dbo.FACTD01 F JOIN aspel_sae50.dbo.PAR_FACTD01 FY ON F.CVE_DOC=FY.CVE_DOC JOIN aspel_sae50.dbo.INVE01 I ON LTRIM(RTRIM(I.CVE_ART))=LTRIM(RTRIM(FY.CVE_ART)) WHERE FY.TIPO_PROD='P' AND LTRIM(RTRIM(LIN_PROD))<>'MP' AND convert(datetime2,convert(varchar(255),FECHA_DOC,102),102) between @fechaInicial AND @fechaFinal AND F.STATUS<>'C' GROUP BY SUBSTRING(FY.CVE_ART,1,@Rango)


declare @cursor				as cursor
declare @countOfModelos		as int
declare @modelo				as varchar(50)
declare @prendas_facturas	as int
declare @prendas_nc			as int
declare @suma_total			as int


set @prendas_facturas = 0
set @prendas_nc = 0
set @suma_total = 0


	set @cursor = cursor for select modelo from #modelos

	open @cursor
	fetch next from @cursor into @modelo
	
	while @@fetch_status = 0
		begin
			select
				@prendas_facturas = #facturas.prendas
			from
				#facturas
			where
				#facturas.modelo = @modelo
				
			select
				@prendas_nc = #notas_credito.prendas
			from
				#notas_credito
			where
				#notas_credito.modelo = @modelo
			
			
			insert into #participacion_por_articulo (
														modelo,
														descripcion,
														factura,
														nc,
														suma
													)
													values
													(
														@modelo,
														isnull((select top 1 DESCR from aspel_sae50.dbo.INVE01 where rtrim(ltrim(SUBSTRING(CVE_ART,1,@Rango))) = @modelo),''),
														@prendas_facturas,
														@prendas_nc * -1,
														@prendas_facturas + (@prendas_nc * -1)
													)
				set @prendas_facturas = 0
				set @prendas_nc = 0
			
			fetch next from @cursor into @modelo
		end		
	close		@cursor
	deallocate	@cursor


--select * from #participacion_por_articulo where factura <> 0 or nc <> 0

delete #participacion_por_articulo where not (factura <> 0 or nc <> 0)

select @suma_total = isnull(sum([suma]),0) from #participacion_por_articulo

update #participacion_por_articulo set porcentaje = convert(decimal(9,7),cast(suma as float) / cast(@suma_total as float))


select 
	modelo,
	descripcion,
	factura,
	nc,
	suma
from
	#participacion_por_articulo order by [porcentaje] asc


--select #facturas.* from #facturas inner join #modelos on #facturas.modelo = #modelos.modelo
--select #notas_credito.* from #notas_credito inner join #modelos on #notas_credito.modelo = #modelos.modelo
--select * from #modelos
/*
select * from #modelos
select * from #facturas
select * from #notas_credito
*/


drop table #modelos
drop table #facturas
drop table #notas_credito
drop table #participacion_por_articulo

END
GO
