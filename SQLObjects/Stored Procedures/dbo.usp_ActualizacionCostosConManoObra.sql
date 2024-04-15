
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Israel Aragón
-- Create date: 25/09/2014
-- Description:	sp para procesar info del menú Avanzad\Contabilidad\Procesos\"Actualización de costos SIN mano de obra"
-- =============================================
-- 09/03/2015 Se agrega Round() en la suma
--usp_ActualizacionCostosConManoObra 1
--usp_ActualizacionCostosConManoObra 2
--usp_ActualizacionCostosConManoObra 3
CREATE PROCEDURE [dbo].[usp_ActualizacionCostosConManoObra]
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
			WHERE pt.TIPOCOMP = '49'
		END
	if (@paso=2)
		BEGIN
			
			SELECT CLAVE, round(SUM(CANTIDAD * COSTOU), 2) AS SUMA 
			into #tmp
			FROM aspel_sae50.dbo.INVE01 i
			JOIN aspel_prod30.dbo.PT_DET01 pt ON CVE_ART = CLAVE GROUP BY CLAVE

			update aspel_sae50.dbo.INVE01 set ULT_COSTO = t.SUMA
			from #tmp t
			inner join aspel_sae50.dbo.INVE01 i on t.CLAVE = LTRIM(RTRIM(i.CVE_ART))
			
		END
	if (@paso=3)
		BEGIN			
			UPDATE aspel_sae50.dbo.MINVE01 Set COSTO = ROUND(ULT_COSTO,3) 
			FROM aspel_sae50.dbo.INVE01 i JOIN aspel_sae50.dbo.MINVE01 m ON i.CVE_ART = m.CVE_ART 
			Where CVE_CPTO in(2, 4, 51, 56)
			--Where CVE_CPTO = 2 Or CVE_CPTO = 4 Or CVE_CPTO = 51 Or CVE_CPTO = 56
		END
	
END
GO
