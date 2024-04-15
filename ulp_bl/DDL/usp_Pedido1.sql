/*
Se agregó:

"ORDER BY ped_t.MODELO, cmt.CMT_INDX" ya que el orden de los procesos no correspond a la captura

usp_Pedido '130'

11-Mar-2015
Se agrega isnull a los campos: COMISION y DESCUENTO

*/
ALTER PROCEDURE usp_Pedido
(
	@idPedido int
)
AS
BEGIN
	set ansi_warnings off;
	SET NOCOUNT ON;

SELECT        CLIE.CLAVE AS CCLIE, CLIE.NOMBRE, 'DIRECCION: ' + ISNULL(CLIE.CALLE, '') + ' ' + ISNULL(CLIE.NUMEXT, '') + ' ' + ISNULL(CLIE.NUMINT, '') 
                         + ' ' + ISNULL(CLIE.COLONIA, '') + ' ' + ' CIUDAD: ' + ISNULL(CLIE.MUNICIPIO, 'N/A') + ' ' + '   C.P. ' + ISNULL(CLIE.CODIGO, 'N/A') 
                         + ' ' + ' TEL:  ' + ISNULL(CLIE.TELEFONO, 'N/A') AS DIRECCION, CLIE.RFC, CT.NOMBRE AS ATENCION, ped.PEDIDO, ped.CLIENTE, ped.FECHA, DESCUENTO = isnull(ped.DESCUENTO, 0), 
                         ped.TERMINOS, COMISION=isnull(ped.COMISION, 0), ped.AGENTE, ped.ESTATUS, ped.REMITIDO, ped.CONSIGNADO, ped.OBSERVACIONES, ped.IMPORTE, ped.PRENDAS, 
                         ped.DESC_DADO, ped.OC, ped_t.MODELO, ped_t.DESCRIPCION, ped_t.PRENDAS AS t_PRENDAS, ped_t.TALLAS, ped_t.PEDIDO AS t_PEDIDO, ped_t.PRECIO, 
                         ped_t.IMPORTE AS t_IMPORTE, ped_t.AGRUPADOR, ped_t.PRE_PROCESOS, cmt.CMT_PEDIDO, 
                         cmt.CMT_CMMT + ' ' + cmt.CMT_COMO + ' ' + cmt.CMT_DONDE AS CMT_CMMT, cmt.CMT_PROCESO, cmt.CMT_PRE_PROCESO, cmt.CMT_AGRUPADOR, 
						v.CLASIFIC, v.NOMBRE as nombre_vendedor
FROM            aspel_sae50.dbo.CLIE01 AS CLIE 
						INNER JOIN aspel_sae50.dbo.PED_MSTR AS ped ON LTRIM(RTRIM(CLIE.CLAVE)) = LTRIM(RTRIM(ped.CLIENTE)) 
						LEFT OUTER JOIN aspel_sae50.dbo.PED_TEMP AS ped_t ON ped_t.PEDIDO = ped.PEDIDO 
						LEFT OUTER JOIN aspel_sae50.dbo.CMT_DET AS cmt ON cmt.CMT_PEDIDO = ped_t.PEDIDO AND cmt.CMT_AGRUPADOR = ped_t.AGRUPADOR 
						LEFT OUTER JOIN aspel_sae50.dbo.CONTAC01 AS CT ON CLIE.CLAVE = CT.CVE_CLIE AND CT.TIPOCONTAC = 'V' AND CT.STATUS = 'A'
						left outer join aspel_sae50.dbo.VEND01 v on v.CVE_VEND = ped.AGENTE
where ped.PEDIDO = @idPedido
ORDER BY ped_t.MODELO, cmt.CMT_INDX


END