SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[udf_RegresaCostoPorCveArt]
(
	@cve_art	as	varchar(30),
	@operador	as	varchar(2),
	@fecha		as	date
)
RETURNS decimal(10,2)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @costo	decimal(10,2)


if @operador = '<='
	begin
		SELECT top 1			
			--CY.CANT,
			@costo = isnull(CY.COST,0)
		FROM
			aspel_sae50.dbo.COMPC01 C
		INNER JOIN
			aspel_sae50.dbo.PAR_COMPC01 CY
		ON
			C.CVE_DOC=CY.CVE_DOC
		WHERE
			C.TIP_DOC='c'
			AND
			C.STATUS<>'C'
			AND
			LTRIM(RTRIM(CVE_ART)) = @cve_art
			AND
			convert(date,C.FECHA_DOC) <= @fecha 
		ORDER BY
			C.FECHA_DOC DESC, C.CVE_DOC DESC
	end
else
	begin		
		SELECT top 1			
			--CY.CANT,
			@costo = isnull(CY.COST,0)
		FROM
			aspel_sae50.dbo.COMPC01 C
		INNER JOIN
			aspel_sae50.dbo.PAR_COMPC01 CY
		ON
			C.CVE_DOC=CY.CVE_DOC
		WHERE
			C.TIP_DOC='c'
			AND
			C.STATUS<>'C'
			AND
			LTRIM(RTRIM(CVE_ART)) = @cve_art
			AND
			convert(date,C.FECHA_DOC) > @fecha 
		ORDER BY
			C.FECHA_DOC DESC, C.CVE_DOC DESC	
	end
	
	
	
	RETURN @costo

END
GO
