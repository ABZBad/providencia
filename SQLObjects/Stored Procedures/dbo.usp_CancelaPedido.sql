SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
create procedure [dbo].[usp_CancelaPedido]
(
	@NumeroPedido	as	int
)
as
	UPDATE aspel_sae50.dbo.PED_MSTR SET ESTATUS = 'C' WHERE PEDIDO = @NumeroPedido
	UPDATE aspel_sae50.dbo.UPPEDIDOS SET ESTATUS_UPPEDIDOS = 'C' WHERE PEDIDO = @NumeroPedido
	
	
GO
