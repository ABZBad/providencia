SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Israel Aragón
-- Create date: 18/08/2014
-- Description:	Consulta que devuelve registros para poblar información en el módulo frmPrioridaddeMaquila
-- =============================================
CREATE PROCEDURE [dbo].[usp_PrioridaddeMaquila]
(
	@proveedor varchar(20), 
	@Prefijo varchar(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	set ansi_warnings off;
	SET NOCOUNT ON;

	SELECT comp.CVE_CLPV, par.CVE_DOC,SUBSTRING(CVE_ART,1,9) AS MODELO,SUM(PXR) AS SUMA 
	into #tmp
	FROM aspel_sae50.dbo.COMPO01 comp JOIN aspel_sae50.dbo.PAR_COMPO01 par ON LTRIM(RTRIM(comp.CVE_DOC)) = LTRIM(RTRIM(par.CVE_DOC)) 
	WHERE comp.STATUS <> 'C' AND PXR <> 0 AND comp.TIP_DOC = 'o' AND 
	LTRIM(RTRIM(CVE_CLPV)) = @proveedor AND 
	SUBSTRING(CVE_ART,1,1) = @Prefijo GROUP BY par.CVE_DOC,CVE_CLPV,SUBSTRING(CVE_ART,1,9) ORDER BY par.CVE_DOC

	update aspel_sae50.dbo.PRIORIDAD_MAQUILA set OC = tmp.CVE_DOC, PRIORIDAD = 1000
	from aspel_sae50.dbo.PRIORIDAD_MAQUILA pm inner join #tmp tmp on LTRIM(RTRIM(pm.OC)) = tmp.CVE_DOC

	SELECT comp.CVE_CLPV, FECHA_DOC, CVE_DOC = ltrim(rtrim(par.CVE_DOC)),SUBSTRING(CVE_ART,1,9) AS MODELO,SUM(PXR) AS SUMA,
		PRIORIDAD,comp.CVE_OBS AS OBS_COMP 
	FROM aspel_sae50.dbo.COMPO01 comp JOIN aspel_sae50.dbo.PAR_COMPO01 par ON LTRIM(RTRIM(comp.CVE_DOC)) = LTRIM(RTRIM(par.CVE_DOC)) 
	JOIN aspel_sae50.dbo.PRIORIDAD_MAQUILA pm ON LTRIM(RTRIM(par.CVE_DOC)) = LTRIM(RTRIM(OC)) 
	WHERE comp.STATUS <> 'C' and PXR <> 0 AND comp.TIP_DOC = 'o' AND LTRIM(RTRIM(CVE_CLPV)) = @proveedor AND SUBSTRING(CVE_ART,1,1) = @Prefijo 
	GROUP BY FECHA_DOC,par.CVE_DOC,CVE_CLPV,SUBSTRING(CVE_ART,1,9),PRIORIDAD,comp.CVE_OBS 
	ORDER BY PRIORIDAD,FECHA_DOC,MODELO


END
GO
