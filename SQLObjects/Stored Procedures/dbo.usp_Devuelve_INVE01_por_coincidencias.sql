SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Israel Aragó>
-- Create date: <13/01/2015>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_Devuelve_INVE01_por_coincidencias]
(
	@descr varchar(100), 
	@almacen int
)
AS
BEGIN
	set ansi_warnings off;
	SET NOCOUNT ON;

	SELECT CLAVE = I.CVE_ART, NOMBRE=I.DESCR, I.EXIST, I.LIN_PROD 
	FROM aspel_sae50.dbo.INVE01 I LEFT JOIN aspel_sae50.dbo.MULT01 M ON I.CVE_ART=M.CVE_ART 
	WHERE (UPPER(I.CVE_ART) LIKE UPPER('%' + @descr + '%') OR 
		UPPER(I.DESCR) LIKE UPPER('%' + @descr + '%')) AND I.TIPO_ELE='P' AND I.STATUS='A' AND M.CVE_ALM= @almacen
	ORDER BY I.CVE_ART
END
GO
