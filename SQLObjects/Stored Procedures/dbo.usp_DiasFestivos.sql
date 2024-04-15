SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_DiasFestivos]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT DATENAME(dw, FECHA_FESTIVO) + ', ' + 
		CONVERT(VARCHAR,DAY(FECHA_FESTIVO)) + ' de ' + 
		DATENAME(M, FECHA_FESTIVO) + ' de ' + 
		CONVERT(VARCHAR,YEAR(FECHA_FESTIVO)) AS Fecha 
	FROM aspel_sae50.dbo.DIASFESTIVOS ORDER BY FECHA_FESTIVO DESC

END
GO
