SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
create procedure [dbo].[usp_RepReconsYConteoMermas]
(
	@CVE_CPTO		as int,
	@fecha_desde	as date,
	@fecha_hasta	as date
)
as

/*
set @CVE_CPTO		=	60
set @fecha_desde	=	'01-07-2014'
set @fecha_hasta	=	'31-07-2014'
*/

SELECT DESCR FROM aspel_sae50..CONM01 WHERE CVE_CPTO = @CVE_CPTO 


select
	Consulta_Agrupada.CLV_ART,
	Consulta_Agrupada.CANTIDAD,
	INVE01.DESCR,
	Consulta_Agrupada.COSTO
from
	(
	SELECT
			CVE_ART AS CLV_ART,
			SUM(CANT) AS CANTIDAD,
			COSTO
	FROM aspel_sae50..MINVE01
	WHERE CVE_CPTO = @CVE_CPTO
	AND convert(date,FECHA_DOCU) >= @fecha_desde
	AND convert(date,FECHA_DOCU) <= @fecha_hasta
	GROUP BY
		CVE_ART,
		COSTO
) as Consulta_Agrupada
	inner join
		aspel_sae50..INVE01
	on
		Consulta_Agrupada.CLV_ART = LTRIM(RTRIM(INVE01.CVE_ART))
order by 
	Consulta_Agrupada.CLV_ART
GO
