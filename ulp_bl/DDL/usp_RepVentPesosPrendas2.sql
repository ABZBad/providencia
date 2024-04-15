/*
09-Abr-2015 :	Se modifica totalmente el reporte de Ventas en pesos y prendas por agente
				Según último código fuente entregado del SIP7
04-May-2015 :	Se vuelve a modificar según código fuente de SIP 7.15.10 (hubo modificaciones)
*/
ALTER PROCEDURE usp_RepVentPesosPrendas
	(
		@fecha_inicial		as date	= '01-01-1999',
		@fecha_final		as date = '01-01-1999',
		@vtas_en_rango		as bit = 0
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT
  CVE_VEND AS AGENTE,
  NOMBRE,
  (PESOS_F - PESOS_D) AS Pesos,
  (PRENDAS_F - PRENDAS_D) AS PRENDAS,  
  CASE
    WHEN (PESOS_F - PESOS_D) = 0 THEN 0
    WHEN (PRENDAS_F - PRENDAS_D) = 0 THEN 0
    ELSE (PESOS_F - PESOS_D) / (PRENDAS_F - PRENDAS_D)
  END AS Promedio,
  PRENDAS_F,
  PRENDAS_D,
  PESOS_F,
  PESOS_D
  into #tbl_final
FROM (SELECT
  CVE_VEND,
  NOMBRE,
  ISNULL((SELECT
    ISNULL(SUM(CAN_TOT - (DES_TOT + DES_FIN)), 0)
  FROM aspel_sae50..FACTF01 AS FACT01
  WHERE FECHA_DOC between @fecha_inicial and @fecha_final
  AND FACT01.STATUS <> 'C'
  AND FACT01.CVE_VEND = V.CVE_VEND), 0) AS PESOS_F,
  ISNULL((SELECT
    ISNULL(SUM(FA0TY1.CANT), 0)
  FROM aspel_sae50..FACTF01 AS FACT01
  JOIN aspel_sae50..PAR_FACTF01 FA0TY1
    ON LTRIM(RTRIM(FA0TY1.CVE_DOC)) = LTRIM(RTRIM(FACT01.CVE_DOC))
  JOIN aspel_sae50..INVE01
    ON LTRIM(RTRIM(INVE01.CVE_ART)) = LTRIM(RTRIM(FA0TY1.CVE_ART))
  WHERE FA0TY1.TIPO_PROD = 'P'
  AND LTRIM(RTRIM(LIN_PROD)) <> 'MP'
  AND FECHA_DOC between @fecha_inicial and @fecha_final
  AND FACT01.STATUS <> 'C'
  AND FACT01.CVE_VEND = V.CVE_VEND), 0) AS PRENDAS_F,
  ISNULL((SELECT
    ISNULL(SUM(CAN_TOT - (DES_TOT + DES_FIN)), 0)
  FROM aspel_sae50..FACTD01 AS FACT01
  WHERE FECHA_DOC between @fecha_inicial and @fecha_final
  AND FACT01.STATUS <> 'C'
  AND FACT01.CVE_VEND = V.CVE_VEND), 0) AS PESOS_D,
  ISNULL((SELECT
    ISNULL(SUM(FA0TY1.CANT), 0)
  FROM aspel_sae50..FACTD01 AS FACT01
  JOIN aspel_sae50..PAR_FACTD01 FA0TY1
    ON LTRIM(RTRIM(FA0TY1.CVE_DOC)) = LTRIM(RTRIM(FACT01.CVE_DOC))
  JOIN aspel_sae50..INVE01
    ON LTRIM(RTRIM(INVE01.CVE_ART)) = LTRIM(RTRIM(FA0TY1.CVE_ART))
  WHERE FA0TY1.TIPO_PROD = 'P'
  AND LTRIM(RTRIM(LIN_PROD)) <> 'MP'
  AND FECHA_DOC between @fecha_inicial and @fecha_final
  AND FACT01.STATUS <> 'C'
  AND FACT01.CVE_VEND = V.CVE_VEND), 0) AS PRENDAS_D
FROM aspel_sae50..VEND01 V
WHERE STATUS = 'A') AS TBL
 
 
 
 if @vtas_en_rango = 1
	begin
		select AGENTE,NOMBRE,Pesos,PRENDAS,Promedio from #tbl_final where PRENDAS_F <> 0 OR PRENDAS_D <> 0 OR PESOS_F <> 0 OR PESOS_D <> 0
	end
else
	begin
		select AGENTE,NOMBRE,Pesos,PRENDAS,Promedio from #tbl_final
	end
 
 drop table #tbl_final
 
END