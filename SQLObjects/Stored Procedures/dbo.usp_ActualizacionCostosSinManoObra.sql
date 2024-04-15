SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Israel Aragón
-- Create date: 25/09/2014
-- Description:	sp para procesar info del menú Avanzad\Contabilidad\Procesos\"Actualización de costos SIN mano de obra"
-- =============================================
--usp_ActualizacionCostosSinManoObra 1
--usp_ActualizacionCostosSinManoObra 2
CREATE PROCEDURE [dbo].[usp_ActualizacionCostosSinManoObra]
(
	@paso int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	set ansi_warnings off;

    if	(@paso=1)
		BEGIN
			UPDATE aspel_prod30.dbo.PT_DET01 Set aspel_prod30.dbo.PT_DET01.COSTOU = ROUND(i.ULT_COSTO,3) 
			FROM aspel_prod30.dbo.PT_DET01 pt JOIN aspel_sae50.dbo.INVE01 i ON COMPONENTE = CVE_ART 
			WHERE pt.TIPOCOMP = '49' AND LIN_PROD <> 'INSU'
		END
	if (@paso=2)
		BEGIN 
			SELECT CLAVE,SUM(CANTIDAD * COSTOU) AS SUMA 
			into #tmp
			FROM aspel_sae50.dbo.INVE01 i
			JOIN aspel_prod30.dbo.PT_DET01 pt ON CVE_ART = CLAVE AND TIPOCOMP = '49' GROUP BY CLAVE

			update aspel_sae50.dbo.INVE01 set ULT_COSTO = t.SUMA
			from #tmp t
			inner join aspel_sae50.dbo.INVE01 i on t.CLAVE = LTRIM(RTRIM(i.CVE_ART))
			
		END
	

END
GO
