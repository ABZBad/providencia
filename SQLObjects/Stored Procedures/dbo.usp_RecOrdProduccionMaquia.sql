
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Israel Aragón
-- Create date: 08/08/2014
-- Description:	Devuelve info para módulo RecOrdProduccionMaquia

--10-Mar-2015 : se agrega order by ordf.NUM_REG
-- =============================================
CREATE PROCEDURE [dbo].[usp_RecOrdProduccionMaquia]
(
	@referencia varchar(20), 
	@CVE_DOC varchar(20), 
	@CVE_ALM varchar(20)
)
AS
BEGIN
	SET NOCOUNT ON;
	set ansi_warnings off;

	SELECT ordf.NUM_REG,REFERENCIA, PAR.CVE_DOC,PAR.CVE_ART,PRODUCTO,PXR,COST, inv.DESCR, 
		prov.CLAVE, prov.NOMBRE --, ordf.STATUS, CVE_ALM
	FROM aspel_sae50.dbo.PAR_COMPO01 PAR 
	JOIN aspel_prod30.dbo.ORD_FAB01 ordf ON SUBSTRING(PRODUCTO,1,12) = SUBSTRING(CVE_ART,2,12) 
	JOIN aspel_sae50.dbo.MULT01 M ON PRODUCTO = M.CVE_ART 
	left outer join aspel_sae50.dbo.INVE01 inv on PRODUCTO = inv.CVE_ART
	left outer join aspel_sae50.dbo.COMPO01 comp on ltrim(rtrim(PAR.CVE_DOC)) = ltrim(rtrim(comp.CVE_DOC))
	left outer join aspel_sae50.dbo.PROV01 prov on comp.CVE_CLPV = prov.CLAVE 
	WHERE CANTIDAD - CANTTERM >= PXR AND LTRIM(RTRIM(REFERENCIA)) = @referencia AND ltrim(rtrim(PAR.CVE_DOC)) = @CVE_DOC AND 
		ordf.STATUS <> 3 AND ordf.STATUS <> 0 AND LTRIM(RTRIM(CVE_ALM)) = @CVE_ALM
	order by ordf.NUM_REG
END
GO
