SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE PROCEDURE [dbo].[usp_OrdenTrabajoPorcliente]
	(
		@clave_cliente varchar(20)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	set ansi_warnings off;
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select 
		AGENTE,	
	    PEDIDO,
        CLIENTE,
        FECHA, 
        ESTATUS
		/*
        DESCUENTO,
        TERMINOS,
        COMISION,        
        ESTATUS,
        REMITIDO,
        CONSIGNADO,
        OBSERVACIONES,
        IMPORTE,
        PRENDAS,
        DESC_DADO,
        LISTA,
        OC,
        FECHA_IMPRESION,
        TIPO
		*/
	from aspel_sae50.dbo.PED_MSTR where ltrim(rtrim(CLIENTE)) = @clave_cliente AND ESTATUS <> 'C' AND TIPO = 'OT'

END
GO
