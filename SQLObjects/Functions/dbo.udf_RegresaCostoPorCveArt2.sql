SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[udf_RegresaCostoPorCveArt2]
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
		SELECT 			
			@costo = COSTO
		FROM
			aspel_sae50.dbo.MINVE01
		WHERE
			CVE_CPTO=1
			AND
			LTRIM(RTRIM(CVE_ART)) = @cve_art AND convert(date,FECHA_DOCU) <= @fecha
		ORDER BY
			FECHA_DOCU DESC, NUM_MOV DESC
	end
else
	begin		
		SELECT 			
			@costo = COSTO
		FROM
			aspel_sae50.dbo.MINVE01
		WHERE
			CVE_CPTO=1
			AND
			LTRIM(RTRIM(CVE_ART)) = @cve_art AND convert(date,FECHA_DOCU) > @fecha
		ORDER BY
			FECHA_DOCU DESC, NUM_MOV DESC	
	end
	
	
	
	RETURN @costo

END
GO
