SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Israel Aragón
-- Create date: 09/09/2014
-- Description:	Devuelve información para llenar el módulo frmTransferenciaXModelo
-- =============================================
--usp_frmTransferenciaXModelo 'PAGAMOSH', '1', '3'
CREATE PROCEDURE [dbo].[usp_frmTransferenciaXModelo]
(
	@modelo varchar(12), 
	@Origen varchar(2),
	@Destino varchar(2)
)
AS
BEGIN
	set ansi_warnings off;
	SET NOCOUNT ON;

	SELECT inv.CVE_ART AS CLV_ART,DESCR,TIPO_ELE, mult.EXIST 
		, multDest.CVE_ART AS CLV_ART_DEST
	FROM aspel_sae50.dbo.INVE01 inv 
	INNER JOIN aspel_sae50.dbo.MULT01 mult ON inv.CVE_ART = mult.CVE_ART 
	INNER JOIN aspel_sae50.dbo.MULT01 multDest ON inv.CVE_ART = multDest.CVE_ART AND multDest.CVE_ALM = @Destino 
	WHERE ltrim(rtrim(SUBSTRING(inv.CVE_ART,1,8))) = @modelo  AND mult.CVE_ALM = @Origen
	ORDER BY SUBSTRING(inv.CVE_ART,11,2), SUBSTRING(inv.CVE_ART,9,2)

END
GO
