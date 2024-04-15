SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Israel Aragó>
-- Create date: <13/01/2015>
-- Description:	<Description,,>
-- =============================================
--usp_DupCodProdEstr_Devuelve_UltimoCostoUnitario 'BA80BGSM4001', 1
CREATE PROCEDURE [dbo].[usp_DupCodProdEstr_Devuelve_UltimoCostoUnitario]
(	
	@tipo varchar(2), 
	@Componente varchar(100), 
	@almacen int 
)
AS
BEGIN
	set ansi_warnings off;
	SET NOCOUNT ON;
	
	if @tipo='PT' 
	begin
		SELECT PT.CLAVE, I.ULT_COSTO, M.CVE_ALM 
		FROM aspel_prod30.dbo.PRO_TERM01 PT 
		LEFT JOIN aspel_sae50.dbo.INVE01 I ON PT.CLAVE=I.CVE_ART 
		LEFT JOIN aspel_sae50.dbo.MULT01 M ON PT.CLAVE=M.CVE_ART 
		WHERE CLAVE=@Componente AND M.CVE_ALM=@almacen
	end
	else
	begin
		SELECT I.CVE_ART, I.ULT_COSTO, M.CVE_ALM 
		FROM aspel_sae50.dbo.INVE01 I 
		LEFT JOIN aspel_sae50.dbo.MULT01 M ON I.CVE_ART=M.CVE_ART 
        WHERE I.CVE_ART=@Componente AND M.CVE_ALM=@almacen
	end
END
GO
