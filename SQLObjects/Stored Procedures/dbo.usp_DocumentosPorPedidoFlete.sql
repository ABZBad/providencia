SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE procedure [dbo].[usp_DocumentosPorPedidoFlete]
	(
		@numero_pedido		as	int
	)
as
begin
	SELECT 
		PEDIDO,
		(SELECT ISNULL(SUM(CANTIDAD), 0)	FROM	aspel_sae50.dbo.PED_DET WHERE PEDIDO=@numero_pedido AND PROCESOS LIKE '%F%') PRENDAS_FLETE,
		dbo.udf_RegresaFacturasPorPedido(@numero_pedido)	as	FACTURAS,
		(SELECT TOP 1 ISNULL(CMT_COS_PROCESO, 0)	COSTO FROM aspel_sae50.dbo.CMT_DET	WHERE CMT_PEDIDO=@numero_pedido AND CMT_PROCESO='F' ORDER BY COSTO DESC) COSTO_PREVIO
	FROM
		aspel_sae50.dbo.PED_MSTR
	WHERE
		ESTATUS='I' AND TIPO='OV' AND PEDIDO=@numero_pedido
end
GO
