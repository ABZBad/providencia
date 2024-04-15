SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [usp_RepFechasYDesempeñoPorAreaFechaStd]
	(
		@pedido			as int,
		@dias			as int
	)
AS
BEGIN	
	SET NOCOUNT ON

		SELECT 
				ISNULL(CONVERT(VARCHAR, aspel_sae50.dbo.AddWeekdays(F_GESTION,@dias), 6), '') AS 'Fch Stnd'
		FROM
				aspel_sae50.dbo.UPPEDIDOS WHERE PEDIDO=@pedido
	
END
GO
