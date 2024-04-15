SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Israel Aragón>
-- Create date: <18/08/2014>
-- Description:	<Consulta que devuelve datos que se exportarán derivados del módulo RecOrdProduccionMaquia>
-- =============================================
CREATE PROCEDURE [dbo].[usp_RecOrdProduccionMaquiaExportarXls]
(
	@REFERENCIA VARCHAR(5000)
)

AS
BEGIN
	set ansi_warnings off;
	SET NOCOUNT ON;

	SELECT NUM_MOV AS NUM_REG,CVE_ART AS CLV_ART,CVE_CPTO AS TIPO_MOV,FECHA_DOCU,REFER,CANT,ALMACEN 
	FROM aspel_sae50.dbo.MINVE01 
	WHERE  charindex('('+LTRIM(RTRIM(REFER))+')', @REFERENCIA)>0 and 
	YEAR(FECHA_DOCU) = YEAR(getdate()) AND MONTH(FECHA_DOCU) = MONTH(getdate()) AND DAY(FECHA_DOCU) = DAY(getdate())

END
GO
