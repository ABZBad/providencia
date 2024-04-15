SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Israel Aragón
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[sp_EstadoCuenta]
	@id varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

SELECT CONVERT(VARCHAR,CXC.NUM_CPTO) + ' - (' + CO.DESCR + ')' AS Concepto, 
	CXC.DOCTO AS Documento, 
		CXC.REFER AS Referencia, 
		ISNULL(CONVERT(VARCHAR, CXC.FECHA_APLI, 106),'No Dispo.') AS Aplicado, 
		ISNULL(CONVERT(VARCHAR, CXC.FECHA_VENC, 106),'No Dispo.') AS Vencido, 
		ISNULL(CONVERT(VARCHAR, CXC.FECHAELAB, 106),'No Dispo.') AS Elaborado, 
		CASE WHEN CO.TIPO = 'C' THEN ROUND(ISNULL(CXC.IMPORTE,0),2) ELSE ROUND(0,2) END as Cargo, 
		CASE WHEN CO.TIPO = 'A' THEN ROUND(ISNULL(CXC.IMPORTE,0),2) ELSE ROUND(0,2) END As Abono, 
		DATEDIFF(Day, CXC.FECHA_APLI, CXC.FECHA_VENC) As Dias 
FROM aspel_sae50.dbo.CUEN_M01 CXC 
LEFT JOIN aspel_sae50.dbo.CONC01 CO ON CXC.NUM_CPTO = CO.NUM_CPTO 
WHERE CXC.CVE_CLIE = @id
UNION ALL 
SELECT CONVERT(VARCHAR,CXC_H.NUM_CPTO) + ' - (' + CO.DESCR + ')' AS Concepto, 
	CXC_H.DOCTO AS Documento, CXC_H.REFER AS Referencia, 
	ISNULL(CONVERT(VARCHAR, CXC_H.FECHA_APLI, 106),'No Dispo.') AS Aplicado, 
	ISNULL(CONVERT(VARCHAR, CXC_H.FECHA_VENC, 106),'No Dispo.') AS Vencido, 
	ISNULL(CONVERT(VARCHAR, CXC_H.FECHAELAB, 106),'No Dispo.') AS Elaborado, 
	CASE WHEN CO.TIPO = 'C' THEN ROUND(ISNULL(CXC_H.IMPORTE,0),2) ELSE ROUND(0,2) END as Cargo,
	CASE WHEN CO.TIPO = 'A' THEN ROUND(ISNULL(CXC_H.IMPORTE,0),2) ELSE ROUND(0,2) END As Abono, 
	DATEDIFF(Day, CXC_H.FECHA_APLI, CXC_H.FECHA_VENC) As Dias 
FROM aspel_sae50.dbo.CUEN_DET01 CXC_H 
LEFT JOIN aspel_sae50.dbo.CONC01 CO ON CXC_H.NUM_CPTO = CO.NUM_CPTO 
WHERE CXC_H.CVE_CLIE = @id ORDER BY Referencia, Elaborado


END
GO
