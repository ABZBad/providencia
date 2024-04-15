
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
--Modificación para versión 8.0.0.43 del sistema SIP
/*
	Se agrega Left Join ln. 33
	Se corrige orden en la que los Procesos Ln. 37
*/
CREATE PROCEDURE [dbo].[usp_OrdenTrabajo]
(
	@idPedido int
)
AS
BEGIN
	set ansi_warnings off;
	SET NOCOUNT ON;

SELECT CLAVE AS AD_ADDR,NOMBRE AS AD_NAME, ped.AGENTE, ped.PEDIDO, ped_t.MODELO, 
	ped_t.DESCRIPCION, ped_t.TALLAS, ped_t.PRENDAS, ped_t.AGRUPADOR, 
	CMT_PEDIDO AS CMT_CONTADOR, cmt.CMT_CMMT + '    ' + cmt.CMT_COMO + '    ' + cmt.CMT_DONDE AS CMT_CMMT, cmt.CMT_PROCESO,
	cmt.CMT_AGRUPADOR,
	'[Nombre de Archivo:] ' +img.NAME AS NAME,
	img.COD_CATALOGO,
	'[Color 1:] ' + img.COLOR_1 AS COLOR_1,
	'[Color 2:] ' + img.COLOR_2 AS COLOR_2, 
	'[Color 3:] ' + img.COLOR_3 AS COLOR_3,
	'[Color 4:] ' + img.COLOR_4 AS COLOR_4,
	'[Color 5:] ' + img.COLOR_5 AS COLOR_5,
	'[Color 6:] ' + img.COLOR_6 AS COLOR_6,
	'[Puntadas:] ' + CAST(img.PUNTADAS AS char(10)) AS PUNTADAS,
	(img.COMENTARIOS)
FROM aspel_sae50.dbo.CLIE01 clie JOIN aspel_sae50.dbo.PED_MSTR ped
ON ltrim(rtrim(CLAVE)) = ltrim(rtrim(CLIENTE))
JOIN aspel_sae50.dbo.PED_TEMP ped_t
ON ped.PEDIDO = ped_t.PEDIDO
inner join aspel_sae50.dbo.CMT_DET cmt on ped_t.PEDIDO = cmt.CMT_PEDIDO and ped_t.AGRUPADOR = cmt.CMT_AGRUPADOR
LEFT JOIN aspel_sae50.dbo.IMAGENES img
ON img.COD_CATALOGO = cmt.CMT_COMO
where ped_t.PEDIDO = @idPedido
order by cmt.CMT_INDX

END
GO
