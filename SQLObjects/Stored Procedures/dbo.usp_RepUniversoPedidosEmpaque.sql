
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/*
09-Mar-2015: Se corrige Bug de Case Sensitive para la BD de Aspel_Sae50 a aspel_sae50.
*/
CREATE procedure [dbo].[usp_RepUniversoPedidosEmpaque]
	as
begin
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
		SELECT
		  'P' + LTRIM(UPPEDIDOS.PEDIDO)	AS 'Pedido',
		  F_CAPT						AS 'Fch. Pedido',
		  COD_CLIENTE					AS 'Cliente',
		  CLIE01.NOMBRE					AS 'Nombre',
		  PRENDAS						AS 'Prendas',
		  F_VENCIMIENTO					AS 'Fch. Venc.',
		  F_EMPAQUE						AS 'Fch. Empaque',
		  ISNULL((
					SELECT TOP 1
						'F-' + CVE_DOC
					FROM
						aspel_sae50.dbo.DOCTOSIGF01
					WHERE
							TIP_DOC = 'F'
						AND
							ANT_SIG = 'A'
						AND
							TIP_DOC_E = 'P'
						AND
							RTRIM(LTRIM(CVE_DOC_E)) = 'P' + RTRIM(LTRIM(UPPEDIDOS.PEDIDO))
					ORDER BY
						CVE_DOC DESC
					), '') AS 'Factura'
		FROM
			aspel_sae50.dbo.CLIE01
		LEFT JOIN
			aspel_sae50.dbo.UPPEDIDOS
			ON LTRIM(RTRIM(CLAVE)) = LTRIM(RTRIM(COD_CLIENTE))
		LEFT JOIN aspel_sae50.dbo.FACTP01
			ON LTRIM(RTRIM(CVE_DOC)) = 'P' + RTRIM(LTRIM(UPPEDIDOS.PEDIDO))
		JOIN aspel_sae50.dbo.PED_MSTR
			ON PED_MSTR.PEDIDO = UPPEDIDOS.PEDIDO
		WHERE
			UPPEDIDOS.ESTATUS <> 'OK'
			AND
			F_EMPAQUE IS NOT NULL
		ORDER BY
			UPPEDIDOS.PEDIDO
end
GO
