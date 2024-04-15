SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
/*
declare @res varchar(500)
EXEC usp_EliminarHabilitarPedidoAspelSaeSip 28080, @resultado = @res OUTPUT
PRINT @res
*/
CREATE PROCEDURE [dbo].[usp_EliminarHabilitarPedidoAspelSaeSip]
(
	@pedido int, 
	@resultado varchar(500) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	declare @F_CAPT varchar(12)
	declare @cliente varchar(10)
	declare @sPedido varchar(10)
	set @sPedido = 'P' + convert(varchar(10), @pedido)

	if	(SELECT COUNT(*) FROM aspel_sae50.dbo.FACTP01 WHERE ltrim(rtrim(CVE_DOC)) = @sPedido AND ENLAZADO <> 'O')=0
		BEGIN
			
			--PARTe 1: Aqui se cambia el ESTATUS en PED_MSTR de I a P
			update aspel_sae50.dbo.PED_MSTR set ESTATUS = 'P' WHERE PEDIDO = @pedido
			
			--Parte 2: Aqui se elimina el PEDIDO de SAE, no es necesario modificar el CONTADOR de DOCumentos
			DELETE FROM aspel_sae50.dbo.FACTP01 WHERE CVE_DOC = @sPedido
			--Campos Libres de Encabezado
			DELETE FROM aspel_sae50.dbo.FACTP_CLIB01 WHERE CLAVE_DOC = @sPedido
			--Borrado de Partidas
			DELETE FROM aspel_sae50.dbo.PAR_FACTP01 WHERE CVE_DOC = @sPedido
			--Campos Libres de Partidas
			DELETE FROM aspel_sae50.dbo.PAR_FACTP_CLIB01 WHERE CLAVE_DOC = @sPedido

			
			--SELECT COD_CLIENTE, F_CAPT FROM aspel_sae50.dbo.UPPEDIDOS WHERE PEDIDO = 30461
			if (SELECT count(COD_CLIENTE) FROM aspel_sae50.dbo.UPPEDIDOS WHERE PEDIDO = @pedido)>0
				BEGIN
					SELECT @cliente = COD_CLIENTE, @F_CAPT = F_CAPT FROM aspel_sae50.dbo.UPPEDIDOS WHERE PEDIDO = @pedido

					delete FROM aspel_sae50.dbo.UPPEDIDOS WHERE PEDIDO = @pedido

					insert into aspel_sae50.dbo.UPPEDIDOS(COD_CLIENTE, F_CAPT, PEDIDO)
					select @cliente, @F_CAPT, @pedido
				END


			SET @resultado=''
		END
	else
		BEGIN
			SET @resultado='Este pedido ya ha sido facturado anteriormente y no puede ser cancelado por este proceso.'	
		END



END
GO
