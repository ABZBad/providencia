SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE view [dbo].[vw_UpPedidos]
as
(
SELECT
	UPPEDIDOS.*,
	isnull(CONTEO_PRENDAS.SUMA,0) AS NUMERO_PRENDAS,
	NOMBRE,
	CVE_VEND AS VEND 
FROM 
	aspel_sae50.dbo.UPPEDIDOS 
	JOIN 
		aspel_sae50.dbo.CLIE01 ON ltrim(rtrim(UPPEDIDOS.COD_CLIENTE)) = ltrim(rtrim(CLIE01.CLAVE)) 
	INNER JOIN 
		aspel_sae50.dbo.PED_MSTR P ON UPPEDIDOS.PEDIDO = P.PEDIDO 
	LEFT JOIN
		(select ISNULL(sum(CANTIDAD),0) AS SUMA,PEDIDO from aspel_sae50.dbo.PED_DET GROUP BY PEDIDO) AS CONTEO_PRENDAS
		ON UPPEDIDOS.PEDIDO = CONTEO_PRENDAS.PEDIDO
WHERE
	P.ESTATUS <> 'C'
	AND
		P.TIPO = 'OV'
	AND
		(
			SELECT TOP 1
				CVE_DOC_E
			FROM
				aspel_sae50.dbo.DOCTOSIGF01
			WHERE
				CVE_DOC IN  (
								SELECT TOP 1 
									CVE_DOC_E
								FROM 
									aspel_sae50.dbo.DOCTOSIGF01 
								WHERE
									CVE_DOC = CONVERT(VARCHAR,UPPEDIDOS.PEDIDO)
									AND
									TIP_DOC_E = 'F'
							)
				AND
				TIP_DOC_E = 'D'
		) IS NULL
)
GO
