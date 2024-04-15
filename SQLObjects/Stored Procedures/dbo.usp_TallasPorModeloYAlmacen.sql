SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE procedure [dbo].[usp_TallasPorModeloYAlmacen]
	(
		@modelo		varchar(40),
		@almacen	varchar(2)
	)
as

--SELECT * FROM aspel_prod30.dbo.ORD_FAB01 WHERE LTRIM(RTRIM(REFERENCIA)) = '1'

SELECT 
	CLAVE,
	COSTOE,
	DESCR,
	CVE_ALM AS ALMACEN
FROM
	aspel_sae50.dbo.MULT01 MULT
JOIN
	aspel_sae50.dbo.INVE01 INVE
	ON
	INVE.CVE_ART = MULT.CVE_ART
JOIN
	aspel_prod30.dbo.PRO_TERM01 PRO_TERM
	ON CLAVE = INVE.CVE_ART
WHERE
	substring(INVE.CVE_ART,1,8) = @modelo
	AND
	TIPO_ELE = 'P'
	and
	CVE_ALM = @almacen
order by substring(INVE.CVE_ART,9,2),substring(INVE.CVE_ART,11,2)
GO
