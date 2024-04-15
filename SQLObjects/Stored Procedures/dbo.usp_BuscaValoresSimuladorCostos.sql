SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

create procedure [dbo].[usp_BuscaValoresSimuladorCostos]
	(
		@modelo as varchar(12)
	)
as

SELECT 
	aspel_prod30.dbo.PT_DET01.NUM_REG,
	aspel_prod30.dbo.PT_DET01.TIPOCOMP,
	aspel_prod30.dbo.PT_DET01.COMPONENTE,
	aspel_prod30.dbo.PT_DET01.CANTIDAD,	
	aspel_prod30.dbo.PT_DET01.COSTOU,
	INVE.DESCR,
	INVE.PRECIO1,
	INVE.PRECIO2,
	INVE.ULT_COSTO
FROM
	aspel_prod30.dbo.PT_DET01
LEFT JOIN
	(SELECT
	CVE_ART,
	DESCR,
		(
			SELECT
				P.PRECIO
			FROM
				aspel_sae50.dbo.PRECIO_X_PROD01 P
			WHERE
				P.CVE_ART = I.CVE_ART
				AND
				P.CVE_PRECIO = 1
		) AS PRECIO1,
	(
		SELECT
			P.PRECIO
		FROM
			aspel_sae50.dbo.PRECIO_X_PROD01 P
		WHERE
			P.CVE_ART = I.CVE_ART AND P.CVE_PRECIO = 2
	)  AS PRECIO2,
	ULT_COSTO
FROM aspel_sae50.dbo.INVE01 I) AS INVE
	ON 
	aspel_prod30.dbo.PT_DET01.COMPONENTE = INVE.CVE_ART
WHERE aspel_prod30.dbo.PT_DET01.CLAVE = @modelo order by aspel_prod30.dbo.PT_DET01.NUM_REG

GO
