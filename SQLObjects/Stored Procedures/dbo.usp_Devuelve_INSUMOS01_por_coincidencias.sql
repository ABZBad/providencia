SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Israel Aragó>
-- Create date: <20/01/2015>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_Devuelve_INSUMOS01_por_coincidencias]
(
	@descr varchar(100)
)
AS
BEGIN
	set ansi_warnings off;
	SET NOCOUNT ON;

	SELECT CLAVE, NOMBRE FROM aspel_prod30.dbo.INSUMOS01  
	WHERE UPPER(CLAVE) LIKE UPPER('%' + @descr + '%') OR UPPER(NOMBRE) LIKE UPPER('%' + @descr + '%') ORDER BY CLAVE

END
GO
