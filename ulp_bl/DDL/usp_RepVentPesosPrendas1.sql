/*
09-Abr-2015 :	Se modifica totalmente el reporte de Ventas en pesos y prendas por agente
				Según último código fuente entregado del SIP7
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
  PRENDAS_D
  into #tbl_final
FROM (SELECT
  CVE_VEND,
  NOMBRE,
  ISNULL((SELECT
    SUM(P.TOT_PARTIDA - (((P.TOT_PARTIDA * ISNULL(P.DESC1, 0)) / 100) + ((P.TOT_PARTIDA * ISNULL(P.DESC2, 0)) / 100) + ((P.TOT_PARTIDA * ISNULL(P.DESC3, 0)) / 100)))
  FROM aspel_sae50..FACTF01 F
  INNER JOIN aspel_sae50..PAR_FACTF01 P
    ON F.CVE_DOC = P.CVE_DOC
  INNER JOIN aspel_sae50..INVE01 I
    ON P.CVE_ART = I.CVE_ART
  WHERE F.STATUS <> 'C'
  AND convert(date,F.FECHA_DOC) between @fecha_inicial and @fecha_final
  AND F.CVE_VEND = V.CVE_VEND
  AND P.TIPO_PROD = 'P'
  AND I.LIN_PROD <> 'MP'), 0) AS PESOS_F,
  ISNULL((SELECT
    SUM(CANT)
  FROM aspel_sae50..FACTF01 F
  INNER JOIN aspel_sae50..PAR_FACTF01 P
    ON F.CVE_DOC = P.CVE_DOC
  INNER JOIN aspel_sae50..INVE01 I
    ON P.CVE_ART = I.CVE_ART
  WHERE F.STATUS <> 'C'
  AND convert(date,F.FECHA_DOC) between @fecha_inicial and @fecha_final
  AND F.CVE_VEND = V.CVE_VEND
  AND P.TIPO_PROD = 'P'
  AND I.LIN_PROD <> 'MP'), 0) AS PRENDAS_F,
  ISNULL((SELECT
    SUM(P.TOT_PARTIDA - (((P.TOT_PARTIDA * ISNULL(P.DESC1, 0)) / 100) + ((P.TOT_PARTIDA * ISNULL(P.DESC2, 0)) / 100) + ((P.TOT_PARTIDA * ISNULL(P.DESC3, 0)) / 100)))
  FROM aspel_sae50..FACTD01 D
  INNER JOIN aspel_sae50..PAR_FACTD01 P
    ON D.CVE_DOC = P.CVE_DOC
  INNER JOIN aspel_sae50..INVE01 I
    ON P.CVE_ART = I.CVE_ART
  WHERE D.STATUS <> 'C'
  AND convert(date,D.FECHA_DOC) between @fecha_inicial and @fecha_final
  AND D.CVE_VEND = V.CVE_VEND
  AND P.TIPO_PROD = 'P'
  AND I.LIN_PROD <> 'MP'), 0) AS PESOS_D,
  ISNULL((SELECT
    SUM(CANT)
  FROM aspel_sae50..FACTD01 D
  INNER JOIN aspel_sae50..PAR_FACTD01 P
    ON D.CVE_DOC = P.CVE_DOC
  INNER JOIN aspel_sae50..INVE01 I
    ON P.CVE_ART = I.CVE_ART
  WHERE D.STATUS <> 'C'
  AND convert(date,D.FECHA_DOC) between @fecha_inicial and @fecha_final
  AND D.CVE_VEND = V.CVE_VEND
  AND P.TIPO_PROD = 'P'
  AND I.LIN_PROD <> 'MP'), 0) AS PRENDAS_D
FROM aspel_sae50..VEND01 V
WHERE STATUS = 'A') AS TBL
 
 
 
 if @vtas_en_rango = 1
	begin
		select AGENTE,NOMBRE,Pesos,PRENDAS,Promedio from #tbl_final where PRENDAS_F > 0 OR PRENDAS_D > 0
	end
else
	begin
		select AGENTE,NOMBRE,Pesos,PRENDAS,Promedio from #tbl_final
	end
 
 drop table #tbl_final
 
END