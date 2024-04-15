/*
18-Mar-2015 : Se crea función necesaria para el módulo

"Captura de facturas de proveedores de Fletes" solicitado por Rubén Alamán

*/
CREATE procedure usp_DocumentosPorPedidoCostura
	(
		@numero_pedido		as	int
	)
as
begin
	SELECT 
		PEDIDO,
		(SELECT ISNULL(SUM(CANTIDAD), 0)	FROM	aspel_sae50.dbo.PED_DET WHERE PEDIDO=@numero_pedido AND PROCESOS LIKE '%C%') PRENDAS_COSTURA,
		dbo.udf_RegresaFacturasPorPedidoCostura(@numero_pedido)	as	FACTURAS,
		(SELECT TOP 1 ISNULL(CMT_COS_PROCESO, 0)	COSTO FROM aspel_sae50.dbo.CMT_DET	WHERE CMT_PEDIDO=@numero_pedido AND CMT_PROCESO='C' ORDER BY COSTO DESC) COSTO_PREVIO
	FROM
		aspel_sae50.dbo.PED_MSTR
	WHERE
		ESTATUS='I' AND TIPO='OV' AND PEDIDO=@numero_pedido
end

