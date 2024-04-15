SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_Imagenes] 
(
	@COD_CATALOGO varchar(50)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

select * from aspel_sae50.dbo.IMAGENES img where rtrim(ltrim(img.COD_CATALOGO))= rtrim(ltrim(@COD_CATALOGO))

END
GO
