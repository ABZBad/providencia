SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_EstadoCuenta]
(
	@clave_cliente varchar(20)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	set ansi_warnings off;
	SET NOCOUNT ON;

	SELECT        CVE_CLIE = rtrim(ltrim(CXC.CVE_CLIE)), CONVERT(VARCHAR, CXC.NUM_CPTO) + ' - (' + CO.DESCR + ')' AS Concepto, rtrim(ltrim(CXC.DOCTO)) AS Documento, 
							 rtrim(ltrim(CXC.REFER)) AS Referencia, ISNULL(CONVERT(VARCHAR, CXC.FECHA_APLI, 106), 'No Dispo.') AS Aplicado, ISNULL(CONVERT(VARCHAR, CXC.FECHA_VENC, 106), 
							 'No Dispo.') AS Vencido, ISNULL(CONVERT(VARCHAR, CXC.FECHAELAB, 106), 'No Dispo.') AS Elaborado, 
							 CASE WHEN CO.TIPO = 'C' THEN ROUND(ISNULL(CXC.IMPORTE, 0), 2) ELSE ROUND(0, 2) END AS Cargo, 
							 CASE WHEN CO.TIPO = 'A' THEN ROUND(ISNULL(CXC.IMPORTE, 0), 2) ELSE ROUND(0, 2) END AS Abono, DATEDIFF(Day, CXC.FECHA_APLI, CXC.FECHA_VENC) 
							 AS Dias
	FROM            aspel_sae50.dbo.CUEN_M01 CXC LEFT JOIN
							 aspel_sae50.dbo.CONC01 CO ON CXC.NUM_CPTO = CO.NUM_CPTO
	where rtrim(ltrim(CXC.CVE_CLIE)) = @clave_cliente
	UNION ALL
	SELECT        CVE_CLIE = rtrim(ltrim(CXC_H.CVE_CLIE)), CONVERT(VARCHAR, CXC_H.NUM_CPTO) + ' - (' + CO.DESCR + ')' AS Concepto, rtrim(ltrim(CXC_H.DOCTO)) AS Documento, 
							 rtrim(ltrim(CXC_H.REFER)) AS Referencia, ISNULL(CONVERT(VARCHAR, CXC_H.FECHA_APLI, 106), 'No Dispo.') AS Aplicado, ISNULL(CONVERT(VARCHAR, CXC_H.FECHA_VENC, 
							 106), 'No Dispo.') AS Vencido, ISNULL(CONVERT(VARCHAR, CXC_H.FECHAELAB, 106), 'No Dispo.') AS Elaborado, 
							 CASE WHEN CO.TIPO = 'C' THEN ROUND(ISNULL(CXC_H.IMPORTE, 0), 2) ELSE ROUND(0, 2) END AS Cargo, 
							 CASE WHEN CO.TIPO = 'A' THEN ROUND(ISNULL(CXC_H.IMPORTE, 0), 2) ELSE ROUND(0, 2) END AS Abono, DATEDIFF(Day, CXC_H.FECHA_APLI, 
							 CXC_H.FECHA_VENC) AS Dias
	FROM            aspel_sae50.dbo.CUEN_DET01 CXC_H LEFT JOIN
							 aspel_sae50.dbo.CONC01 CO ON CXC_H.NUM_CPTO = CO.NUM_CPTO
	where rtrim(ltrim(CXC_H.CVE_CLIE)) = @clave_cliente
	order by rtrim(ltrim(CXC.REFER)), ISNULL(CONVERT(VARCHAR, CXC.FECHAELAB, 106), 'No Dispo.')

	/*
	SELECT CONVERT(VARCHAR,CXC.NUM_CPTO) + ' - (' + CO.DESCR + ')' AS Concepto, CXC.DOCTO AS Documento, CXC.REFER AS Referencia, 
	ISNULL(CONVERT(VARCHAR, CXC.FECHA_APLI, 106),'No Dispo.') AS Aplicado, ISNULL(CONVERT(VARCHAR, CXC.FECHA_VENC, 106),'No Dispo.') AS Vencido, 
	ISNULL(CONVERT(VARCHAR, CXC.FECHAELAB, 106),'No Dispo.') AS Elaborado, 
	CASE WHEN CO.TIPO = 'C' THEN ROUND(ISNULL(CXC.IMPORTE,0),2) ELSE ROUND(0,2) END as Cargo, CASE WHEN CO.TIPO = 'A' THEN ROUND(ISNULL(CXC.IMPORTE,0),2) ELSE ROUND(0,2) END As Abono, 
	DATEDIFF(Day, CXC.FECHA_APLI, CXC.FECHA_VENC) As Dias 
	FROM aspel_sae50.dbo.CUEN_M01 CXC LEFT JOIN aspel_sae50.dbo.CONC01 CO ON CXC.NUM_CPTO = CO.NUM_CPTO WHERE CXC.CVE_CLIE = '       469' 
	UNION ALL 
	SELECT CONVERT(VARCHAR,CXC_H.NUM_CPTO) + ' - (' + CO.DESCR + ')' AS Concepto, CXC_H.DOCTO AS Documento, CXC_H.REFER AS Referencia, 
	ISNULL(CONVERT(VARCHAR, CXC_H.FECHA_APLI, 106),'No Dispo.') AS Aplicado, ISNULL(CONVERT(VARCHAR, CXC_H.FECHA_VENC, 106),'No Dispo.') AS Vencido, 
	ISNULL(CONVERT(VARCHAR, CXC_H.FECHAELAB, 106),'No Dispo.') AS Elaborado, 
	CASE WHEN CO.TIPO = 'C' THEN ROUND(ISNULL(CXC_H.IMPORTE,0),2) ELSE ROUND(0,2) END as Cargo,
	CASE WHEN CO.TIPO = 'A' THEN ROUND(ISNULL(CXC_H.IMPORTE,0),2) ELSE ROUND(0,2) END As Abono, 
	DATEDIFF(Day, CXC_H.FECHA_APLI, CXC_H.FECHA_VENC) As Dias 
	FROM aspel_sae50.dbo.CUEN_DET01 CXC_H LEFT JOIN aspel_sae50.dbo.CONC01 CO ON CXC_H.NUM_CPTO = CO.NUM_CPTO 
	WHERE CXC_H.CVE_CLIE = '       469' ORDER BY Referencia, Elaborado
	*/
END
GO
