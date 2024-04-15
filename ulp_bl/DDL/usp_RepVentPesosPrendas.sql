ALTER PROCEDURE usp_RepVentPesosPrendas
	(
		@fecha_inicial		as date	= '01-01-1999',
		@fecha_final		as date = '01-01-1999'
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

CREATE TABLE #Tbl_Temp (CVE_VEND varchar(10), nombre varchar(300), VENTASPESOS FLOAT, NCPESOS FLOAT, Prendas float, NCPrendas float)
CREATE TABLE #Tbl_Prendas(CVE_VEND varchar(10), nombre varchar(300), VENTASPESOS FLOAT, NCPESOS FLOAT, Prendas float, NCPrendas FLOAT)


-- DE AQUI SACO LAS VENTAS EN PESOS
INSERT INTO #Tbl_Temp (CVE_VEND, nombre, VENTASPESOS, NCPESOS, Prendas, NCPrendas)
SELECT FACT01.CVE_VEND, VEND01.NOMBRE, SUM(CAN_TOT - (DES_TOT + DES_FIN )), 0,0,0 From  aspel_sae50.dbo.FACTF01 AS FACT01 INNER JOIN aspel_sae50.dbo.VEND01 ON ltrim(rtrim(FACT01.CVE_VEND)) = ltrim(rtrim(VEND01.CVE_VEND))
WHERE FECHA_DOC >= @fecha_inicial AND FECHA_DOC <= @fecha_final AND FACT01.STATUS <> 'C' AND FACT01.TIP_DOC = 'F' 
GROUP BY FACT01.CVE_VEND, NOMBRE

-- RESTO DE AQUÍ LAS NC VENTAS
INSERT INTO #Tbl_Temp (CVE_VEND, nombre, VENTASPESOS, NCPESOS, Prendas, NCPrendas)
SELECT FACT01.CVE_VEND, aspel_sae50.dbo.VEND01.NOMBRE, 0, SUM(CAN_TOT - (DES_TOT + DES_FIN )),0,0 From aspel_sae50.dbo.FACTD01 AS FACT01 JOIN aspel_sae50.dbo.VEND01 ON ltrim(rtrim(FACT01.CVE_VEND)) = ltrim(rtrim(VEND01.CVE_VEND))
WHERE FECHA_DOC >= @fecha_inicial AND FECHA_DOC <= @fecha_final AND FACT01.STATUS <> 'C' AND FACT01.TIP_DOC = 'D'
GROUP BY FACT01.CVE_VEND, NOMBRE

-- OBTENGO LA CANTIDAD DE PRENDAS
INSERT INTO #Tbl_Prendas (CVE_VEND, nombre, VENTASPESOS, NCPESOS, Prendas, NCPrendas)
SELECT FACT01.CVE_VEND, aspel_sae50.dbo.VEND01.NOMBRE, 0, 0, SUM(CANT) as PRENDAS, 0 FROM aspel_sae50.dbo.FACTF01 AS FACT01 JOIN aspel_sae50.dbo.PAR_FACTF01 AS FA0TY1 ON FACT01.CVE_DOC = FA0TY1.CVE_DOC JOIN aspel_sae50.dbo.INVE01 ON ltrim(rtrim(INVE01.CVE_ART)) = ltrim(rtrim(FA0TY1.CVE_ART)) 
INNER JOIN aspel_sae50.dbo.VEND01 ON ltrim(rtrim(FACT01.CVE_VEND)) = ltrim(rtrim(VEND01.CVE_VEND))
WHERE FA0TY1.TIPO_PROD = 'P' AND ltrim(rtrim(LIN_PROD)) <> 'MP' AND FECHA_DOC >= @fecha_inicial AND FECHA_DOC <= @fecha_final AND FACT01.STATUS <> 'C' AND FACT01.TIP_DOC = 'F' 
group by FACT01.CVE_VEND, NOMBRE

-- RESTO LA CANTIDAD DE NC PRENDAS
INSERT INTO #Tbl_Prendas (CVE_VEND, nombre, VENTASPESOS, NCPESOS, Prendas, NCPrendas)
SELECT FACT01.CVE_VEND, NOMBRE, 0, 0, 0, SUM(CANT) as PRENDAS FROM aspel_sae50.dbo.FACTD01 AS FACT01 JOIN aspel_sae50.dbo.PAR_FACTD01 AS FA0TY1 ON FACT01.CVE_DOC = FA0TY1.CVE_DOC JOIN aspel_sae50.dbo.INVE01 ON ltrim(rtrim(INVE01.CVE_ART)) = ltrim(rtrim(FA0TY1.CVE_ART)) 
INNER JOIN aspel_sae50.dbo.VEND01 ON ltrim(rtrim(FACT01.CVE_VEND)) = ltrim(rtrim(VEND01.CVE_VEND))
WHERE FA0TY1.TIPO_PROD = 'P' AND ltrim(rtrim(LIN_PROD)) <> 'MP' AND FECHA_DOC >= @fecha_inicial AND FECHA_DOC <= @fecha_final and FACT01.STATUS <> 'C' AND FACT01.TIP_DOC = 'D'
group by FACT01.CVE_VEND, NOMBRE


-- DEVUELVO LA TABLA FINAL
select con1.CVE_VEND as AGENTE, nombre as NOMBRE, (SUM(VENTASPESOS)-SUM(NCPESOS)) as Pesos, SUM(Prendas)-SUM(NCPrendas)as PRENDAS, Promedio = case when SUM(Prendas)-SUM(NCPrendas) > 0 then (SUM(VENTASPESOS)-SUM(NCPESOS))/(SUM(Prendas)-SUM(NCPrendas)) else 0 end from (
SELECT #Tbl_Temp.CVE_VEND CVE_VEND, nombre,(#Tbl_Temp.VENTASPESOS)VENTASPESOS, (#Tbl_Temp.NCPESOS)NCPESOS, #Tbl_Temp.Prendas Prendas, #Tbl_Temp.NCPrendas NCPrendas FROM #Tbl_Temp
--where Tbl_Temp.CVE_VEND='E18'
union
Select #Tbl_Prendas.CVE_VEND CVE_VEND, nombre, #Tbl_Prendas.VENTASPESOS VENTASPESOS, #Tbl_Prendas.NCPESOS NCPESOS, #Tbl_Prendas.Prendas Prendas, #Tbl_Prendas.NCPrendas NCPrendas from #Tbl_Prendas 
--where Tbl_Prendas.CVE_VEND='E18'
)con1


Group by con1.CVE_VEND, nombre
order by con1.CVE_VEND, nombre

--BORRO LAS TABLAS TEMPORALES
 DROP TABLE #Tbl_Temp
 DROP TABLE #Tbl_Prendas
 
END