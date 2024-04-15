SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Israel Aragón>
-- Create date: <20/08/2014>
-- Description:	<Consulta que devuelve datos que se exportarán derivados del módulo RecOrdProduccionMaquia>
-- =============================================
create PROCEDURE [dbo].[usp_PrioridaddeMaquilaActualizaPrioridad]
(
	@OC varchar(20), 
	@PRIORIDAD int	
)

AS
BEGIN
	set ansi_warnings off;
	SET NOCOUNT ON;

	UPDATE aspel_sae50.dbo.PRIORIDAD_MAQUILA SET PRIORIDAD = @PRIORIDAD WHERE ltrim(rtrim(OC)) = @OC

END
GO
