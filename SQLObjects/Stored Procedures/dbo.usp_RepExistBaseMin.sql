SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
--usp_RepExistBaseMin 1
CREATE PROCEDURE [dbo].[usp_RepExistBaseMin]
(
	@CVE_ALM int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT ic.CAMPLIB4 AS CAMPOINTU,m.CVE_ALM AS ALMACEN,m.CVE_ART AS CLV_ART,i.DESCR,m.EXIST,
		m.COMP_X_REC,m.STOCK_MIN,m.STOCK_MAX,PEND_SURT, LTRIM(RTRIM(ic.CAMPLIB1)) AS CAMPOSTRU1 
	FROM aspel_sae50.dbo.INVE01 i JOIN aspel_sae50.dbo.INVE_CLIB01 ic ON i.CVE_ART = ic.CVE_PROD JOIN aspel_sae50.dbo.MULT01 m ON i.CVE_ART = m.CVE_ART 
	WHERE m.CVE_ALM = @CVE_ALM AND LTRIM(RTRIM(ic.CAMPLIB1)) <> '' AND TIPO_ELE = 'P' AND i.STATUS = 'A' 
	ORDER BY ic.CAMPLIB4 ,substring(i.CVE_ART,1,8),substring(i.CVE_ART,9,4)
END
GO
