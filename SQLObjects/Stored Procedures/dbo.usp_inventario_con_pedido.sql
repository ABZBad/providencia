SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
create PROCEDURE [dbo].[usp_inventario_con_pedido]
(
	@idPedido int,
	@modelo varchar(20), 
	@agrupador varchar(30)
)
AS
BEGIN
	SET NOCOUNT ON;

	--'P5876'

SELECT        I.CVE_ART AS CLV_ART, I.DESCR, isnull(pd.PRECIO_PROD, P.PRECIO) AS PRECIO1, I.TIPO_ELE, CANTIDAD = isnull(pd.CANTIDAD, 0)
FROM            aspel_sae50.dbo.INVE01 AS I LEFT OUTER JOIN
                         aspel_sae50.dbo.PRECIO_X_PROD01 AS P ON I.CVE_ART = P.CVE_ART AND P.CVE_PRECIO = 1
				LEFT OUTER JOIN aspel_sae50.dbo.PED_DET pd on pd.CODIGO = P.CVE_ART and PEDIDO = @idPedido AND AGRUPADOR = @agrupador
				--LEFT OUTER JOIN aspel_sae50.dbo.PED_DET pd on pd.CODIGO = P.CVE_ART and PEDIDO = 30463 AND AGRUPADOR = -549390401
				where SUBSTRING(I.CVE_ART,1,8) =  @modelo				
				--where SUBSTRING(I.CVE_ART,1,8) =  'PAGAMOSH'


END
GO
