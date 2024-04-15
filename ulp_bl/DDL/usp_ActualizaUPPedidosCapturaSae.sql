-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
/*
1-Abr-2015 : Se agrega DELETE antes del INSERT a ESTDPEDI, marcaba error de llave duplicada
*/
ALTER PROCEDURE usp_ActualizaUPPedidosCapturaSae
	(
		@NumeroPedido as	 int	= 0
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	update aspel_sae50.dbo.UPPEDIDOS SET 
		F_CAPT_ASPEL = getdate(),
		F_GESTION = getdate()
	WHERE PEDIDO = @NumeroPedido
	
	DELETE aspel_sae50.dbo.ESTDPEDI WHERE PEDIDO = convert(varchar(20),@NumeroPedido)
	
	INSERT INTO aspel_sae50.dbo.ESTDPEDI 
						(
						PEDIDO,
						ADVO,
						LIB,
						SUR,
						COR,
						EST,
						BOR,
						INI,
						[COS],
						EMP,
						EMB,
						FCH
						)
					SELECT 
						convert(varchar(20),@NumeroPedido),
						ADVO,
						LIB,
						SUR,
						COR,
						EST,
						BOR,
						INI,
						[COS],
						EMP,
						EMB,
						GETDATE()
					FROM
						aspel_sae50.dbo.CSTDPEDI
	
	
END