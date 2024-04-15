SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Victor L
-- Create date: 15-05-2014
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_EmisionCartera]
	(
		@cve_vend		as nvarchar(100),
		@anio			as int
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select 
			CLIE01.CLAVE AS CLAVE_CLIENTE,
			CLIE01.NOMBRE AS NOMBRE_CLIENTE,
			CARTERA.*
	from
			aspel_sae50.dbo.CLIE01
		inner join
			aspel_sae50.dbo.CARTERA
		on
			ltrim(rtrim(CLIE01.CLAVE)) = ltrim(rtrim(CARTERA.CLIENTE))
	where
			rtrim(ltrim(CLIE01.CVE_VEND)) = upper(@cve_vend)
		and
			ANNIO >= @anio
    
END
GO
