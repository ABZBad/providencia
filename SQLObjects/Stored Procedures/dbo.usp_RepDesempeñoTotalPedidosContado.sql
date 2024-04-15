SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_RepDesempeñoTotalPedidosContado]
	(
		@fecha_inicial		as datetime,
		@fecha_final		as datetime
	)
AS
BEGIN
	
	SET NOCOUNT ON

	SELECT
		COUNT(*) AS TotalConta
	FROM
		aspel_sae50.dbo.UPPEDIDOS U
	INNER JOIN
		aspel_sae50.dbo.PED_MSTR P
	ON
		U.PEDIDO = P.PEDIDO
	INNER JOIN
		aspel_sae50.dbo.CLIE01 CL
		ON
			LTRIM(RTRIM(P.CLIENTE)) =LTRIM(RTRIM(CL.CLAVE))
	LEFT JOIN
		aspel_sae50.dbo.ESTDPEDI
		ON
			P.PEDIDO = ESTDPEDI.PEDIDO
		WHERE
			convert(datetime2,convert(varchar(255),F_VENCIMIENTO,102),102)  BETWEEN @fecha_inicial AND @fecha_final AND P.TERMINOS = 0
	
END
GO
