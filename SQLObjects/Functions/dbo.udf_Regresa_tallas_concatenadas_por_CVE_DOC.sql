SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
create FUNCTION [dbo].[udf_Regresa_tallas_concatenadas_por_CVE_DOC]
(
	@CVE_DOC varchar(100)
)
RETURNS varchar(max)
AS
BEGIN
	declare @valor varchar(max)

	SELECT @valor = coalesce(@valor, '') + RIGHT(RTRIM(LTrim(tallas.CVE_ART)), 4) + '/' + CONVERT(varchar(12), tallas.PXR) + ' '
	FROM aspel_sae50.dbo.PAR_COMPO01 tallas 
	where LTRIM(RTRIM(tallas.CVE_DOC)) = @CVE_DOC and PXR <> 0


	return @valor
END
GO
