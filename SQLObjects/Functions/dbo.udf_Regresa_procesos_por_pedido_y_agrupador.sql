SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[udf_Regresa_procesos_por_pedido_y_agrupador]
(
	@pedido int, 
	@AGRUPADOR varchar(100)
)
RETURNS varchar(max)
AS
BEGIN
	declare @valor varchar(max)

	SELECT @valor = coalesce(@valor, '') + CMT_PROCESO
	From aspel_sae50.dbo.CMT_DET Where CMT_PEDIDO = @pedido and CMT_AGRUPADOR = @AGRUPADOR

	return @valor
END
GO
