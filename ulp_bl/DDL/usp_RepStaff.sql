/*
03-03-2015
Se corrige error de división entre cero
*/
ALTER procedure [usp_RepStaff]
(
	@fecha_desde	as date,
	@fecha_hasta	as date
)
as

declare @factura_subtotal	float
declare @factura_prendas	float
declare @nc_subtotal		float
declare @nc_iva				float
declare @nc_total			float
declare	@nc_prendas			float

declare @pedidos_cap_periodo			float
declare @prendas_cap_periodo			float
declare @pedidos_en_proceso				float
declare @prendas_en_proceso				float
declare @liberados						float
declare @clientes_atendidos_año_nat		float
declare @clientes_atendidos_en_periodo	float

declare @existencia_alm_Gral_1			float
declare @existencia_alm_Surtido_3		float
declare @existencia_alm_Mostrador_4		float
declare @existencia_alm_Pedidos_Par_40	float
declare @existencia_alm_Fact_Par_5		float
declare @existencia_alm_Esp_32			float
declare @existencia_alm_Hospital_35		float
declare @existencia_prod_Proc			float
declare @existencia_mp_2				float

declare @importe_cuen_mo01_1			float
declare @importe_cuen_mo01_0			float
declare @valor_cartera_total			float
declare @relacion_venta_cartera			float

--set @fecha_desde  =	'01-05-2014'
--set @fecha_hasta  =	'31-05-2014'




--Facturas
SELECT
        @factura_subtotal = ISNULL(SUM(CAN_TOT - (DES_TOT + DES_FIN)), 0) 
FROM aspel_sae50..FACTF01 AS FACT01
WHERE convert(date,FECHA_DOC) between @fecha_desde AND  @fecha_hasta
AND FACT01.STATUS <> 'C'
AND FACT01.TIP_DOC = 'F'

--NOTAS DE CRÉDITO
SELECT
        @nc_subtotal	= ISNULL(SUM(CAN_TOT - (DES_TOT + DES_FIN)), 0)/*,
        @nc_iva			= ISNULL(SUM(IMP_TOT4), 0),
        @nc_total		= ISNULL(SUM(CAN_TOT - DES_TOT + IMP_TOT4), 0)*/
FROM aspel_sae50..FACTD01 AS FACT01
WHERE convert(date,FECHA_DOC) between @fecha_desde AND  @fecha_hasta
AND FACT01.STATUS <> 'C'
AND FACT01.TIP_DOC = 'D'

--PRENDAS FACTURAS
SELECT
        @factura_prendas	=	ISNULL(SUM(FA0TY1.CANT), 0)
FROM aspel_sae50..FACTF01 AS FACT01
JOIN aspel_sae50..PAR_FACTF01 FA0TY1
        ON LTRIM(RTRIM(FA0TY1.CVE_DOC)) = LTRIM(RTRIM(FACT01.CVE_DOC))
JOIN aspel_sae50..INVE01
        ON LTRIM(RTRIM(INVE01.CVE_ART)) = LTRIM(RTRIM(FA0TY1.CVE_ART))
WHERE FA0TY1.TIPO_PROD = 'P'
AND LTRIM(RTRIM(LIN_PROD)) <> 'MP'
AND convert(date,FECHA_DOC) between @fecha_desde AND  @fecha_hasta
AND FACT01.STATUS <> 'C'
AND FACT01.TIP_DOC = 'F'

--PRENDAS NC
SELECT
        @nc_prendas = ISNULL(SUM(FA0TY1.CANT), 0)
FROM aspel_sae50..FACTD01 AS FACT01
JOIN aspel_sae50..PAR_FACTD01 FA0TY1
        ON LTRIM(RTRIM(FA0TY1.CVE_DOC)) = LTRIM(RTRIM(FACT01.CVE_DOC))
JOIN aspel_sae50..INVE01
        ON LTRIM(RTRIM(INVE01.CVE_ART)) = LTRIM(RTRIM(FA0TY1.CVE_ART))
WHERE FA0TY1.TIPO_PROD = 'P'
AND LTRIM(RTRIM(LIN_PROD)) <> 'MP'
AND convert(date,FECHA_DOC) between @fecha_desde AND  @fecha_hasta
AND FACT01.STATUS <> 'C'
AND FACT01.TIP_DOC = 'D'


--PEDIDOs capturados en el periodo
SELECT
        @pedidos_cap_periodo = ISNULL(COUNT(*), 0)
FROM aspel_sae50..FACTP01 AS FACT01
WHERE convert(date,FECHA_DOC) between @fecha_desde AND  @fecha_hasta
AND TIP_DOC = 'P'

--PRENDAS capturadas en el periodo
SELECT
        @prendas_cap_periodo = ISNULL(SUM(FA0TY1.CANT), 0)
FROM aspel_sae50..FACTP01 AS FACT01
JOIN aspel_sae50..PAR_FACTP01 AS FA0TY1
        ON LTRIM(RTRIM(FA0TY1.CVE_DOC)) = LTRIM(RTRIM(FACT01.CVE_DOC))
JOIN aspel_sae50..INVE01
        ON LTRIM(RTRIM(INVE01.CVE_ART)) = LTRIM(RTRIM(FA0TY1.CVE_ART))
WHERE FA0TY1.TIPO_PROD = 'P'
AND LTRIM(RTRIM(LIN_PROD)) <> 'MP'
AND FACT01.STATUS <> 'C'
AND FACT01.TIP_DOC = 'P'
AND convert(date,FECHA_DOC) between @fecha_desde AND  @fecha_hasta

--PEDIDOS vivos (Pedidos en proceso)
SELECT
        @pedidos_en_proceso = ISNULL(COUNT(FACT01.CVE_DOC), 0)
FROM aspel_sae50..FACTP01 AS FACT01
WHERE FACT01.STATUS <> 'C'
AND FACT01.TIP_DOC = 'P'
AND ENLAZADO = 'O'

--PRENDAS vIVAs (Prendas en prOCeso)
SELECT
        @prendas_en_proceso = ISNULL(SUM(FA0TY1.PXS), 0)
FROM aspel_sae50..FACTP01 AS FACT01
JOIN aspel_sae50..PAR_FACTP01 AS FA0TY1
        ON LTRIM(RTRIM(FA0TY1.CVE_DOC)) = LTRIM(RTRIM(FACT01.CVE_DOC))
JOIN aspel_sae50..INVE01
        ON LTRIM(RTRIM(INVE01.CVE_ART)) = LTRIM(RTRIM(FA0TY1.CVE_ART))
WHERE FA0TY1.TIPO_PROD = 'P'
AND LTRIM(RTRIM(LIN_PROD)) <> 'MP'
AND FACT01.STATUS <> 'C'
AND FACT01.TIP_DOC = 'P'
AND ENLAZADO = 'O'

--PEDIDOS LIBERADOS
SELECT
        @liberados = ISNULL(COUNT(*), 0)
FROM aspel_sae50..FACTP01 AS FACT01
JOIN aspel_sae50..UPPEDIDOS
        ON LTRIM(RTRIM(FACT01.CVE_DOC)) = 'P' + LTRIM(RTRIM(UPPEDIDOS.PEDIDO))
WHERE F_LIBERADO > 0
AND FACT01.STATUS <> 'C'
AND FACT01.TIP_DOC = 'P'
AND ENLAZADO = 'O'


--CLIENTEs atendidos en año natural
SELECT
        @clientes_atendidos_año_nat = ISNULL(COUNT(DISTINCT (CVE_CLPV)), 0)
FROM aspel_sae50..FACTF01 AS FACT01
WHERE FECHA_DOC >= GETDATE() - 365
AND FACT01.STATUS <> 'C'
AND FACT01.TIP_DOC = 'F'


--CLIENTES ANTENDIDOS EN EL RANGO
SELECT
        @clientes_atendidos_en_periodo = ISNULL(COUNT(DISTINCT (CVE_CLPV)), 0)
FROM aspel_sae50..FACTF01 AS FACT01
WHERE convert(date,FECHA_DOC) between @fecha_desde AND  @fecha_hasta
AND FACT01.STATUS <> 'C'
AND FACT01.TIP_DOC = 'F'

--EXISTencia ALMACEN general
SELECT
        @existencia_alm_Gral_1 = ISNULL(SUM(MULT01.EXIST), 0)
FROM aspel_sae50..INVE01
JOIN aspel_sae50..INVE_CLIB01
        ON INVE01.CVE_ART = INVE_CLIB01.CVE_PROD
JOIN aspel_sae50..MULT01
        ON INVE01.CVE_ART = MULT01.CVE_ART
        AND MULT01.CVE_ALM = 1
WHERE CVE_ALM = '1'
AND TIPO_ELE = 'P'
AND INVE01.STATUS = 'A'

--EXISTencia ALMACEN SURTIDO
SELECT
        @existencia_alm_Surtido_3 = ISNULL(SUM(MULT01.EXIST), 0)
FROM aspel_sae50..INVE01
JOIN aspel_sae50..INVE_CLIB01
        ON INVE01.CVE_ART = INVE_CLIB01.CVE_PROD
JOIN aspel_sae50..MULT01
        ON INVE01.CVE_ART = MULT01.CVE_ART
        AND MULT01.CVE_ALM = 3
WHERE CVE_ALM = '3'
AND TIPO_ELE = 'P'
AND INVE01.STATUS = 'A'

--EXISTencia ALMACEN mostrador
SELECT
        @existencia_alm_Mostrador_4 = ISNULL(SUM(MULT01.EXIST), 0)
FROM aspel_sae50..INVE01
JOIN aspel_sae50..INVE_CLIB01
        ON INVE01.CVE_ART = INVE_CLIB01.CVE_PROD
JOIN aspel_sae50..MULT01
        ON INVE01.CVE_ART = MULT01.CVE_ART
        AND MULT01.CVE_ALM = 4
WHERE CVE_ALM = '4'
AND TIPO_ELE = 'P'
AND INVE01.STATUS = 'A'

--EXISTencia ALMACEN Pedidos Parciales (40)
SELECT
        @existencia_alm_Pedidos_Par_40 = ISNULL(SUM(MULT01.EXIST), 0)
FROM aspel_sae50..INVE01
JOIN aspel_sae50..INVE_CLIB01
        ON INVE01.CVE_ART = INVE_CLIB01.CVE_PROD
JOIN aspel_sae50..MULT01
        ON INVE01.CVE_ART = MULT01.CVE_ART
        AND MULT01.CVE_ALM = 40
WHERE CVE_ALM = '40'
AND TIPO_ELE = 'P'
AND INVE01.STATUS = 'A'


--EXISTencia ALMACEN Facturación
SELECT
        @existencia_alm_Fact_Par_5 = ISNULL(SUM(MULT01.EXIST), 0)
FROM aspel_sae50..INVE01
JOIN aspel_sae50..INVE_CLIB01
        ON INVE01.CVE_ART = INVE_CLIB01.CVE_PROD
JOIN aspel_sae50..MULT01
        ON INVE01.CVE_ART = MULT01.CVE_ART
        AND MULT01.CVE_ALM = 5
WHERE CVE_ALM = '5'
AND TIPO_ELE = 'P'
AND INVE01.STATUS = 'A'



--EXISTencia ALMACEN Especiales
SELECT
        @existencia_alm_Esp_32 = ISNULL(SUM(MULT01.EXIST), 0)
FROM aspel_sae50..INVE01
JOIN aspel_sae50..INVE_CLIB01
        ON INVE01.CVE_ART = INVE_CLIB01.CVE_PROD
JOIN aspel_sae50..MULT01
        ON INVE01.CVE_ART = MULT01.CVE_ART
        AND MULT01.CVE_ALM = 32
WHERE CVE_ALM = '32'
AND TIPO_ELE = 'P'
AND INVE01.STATUS = 'A'


--'EXISTencia ALMACEN Especiales ?
SELECT
        @existencia_alm_Hospital_35 = ISNULL(SUM(MULT01.EXIST), 0)
FROM aspel_sae50..INVE01
JOIN aspel_sae50..INVE_CLIB01
        ON INVE01.CVE_ART = INVE_CLIB01.CVE_PROD
JOIN aspel_sae50..MULT01
        ON INVE01.CVE_ART = MULT01.CVE_ART
        AND MULT01.CVE_ALM = 35
WHERE CVE_ALM = '35'
AND TIPO_ELE = 'P'
AND INVE01.STATUS = 'A'

--'EXISTencia ALMACEN MP
SELECT
        @existencia_mp_2 = ISNULL(SUM(MULT01.EXIST), 0)
FROM aspel_sae50..INVE01
JOIN aspel_sae50..MULT01
        ON INVE01.CVE_ART = MULT01.CVE_ART
        AND MULT01.CVE_ALM = 2
WHERE CVE_ALM = '2'
AND TIPO_ELE = 'P'
AND LIN_PROD = 'MP'
AND INVE01.STATUS = 'A'

--'EXISTencia ALMACEN PP
SELECT
        @existencia_prod_Proc = ISNULL(SUM(MULT01.COMP_X_REC), 0)
FROM aspel_sae50..INVE01
JOIN aspel_sae50..INVE_CLIB01
        ON INVE01.CVE_ART = INVE_CLIB01.CVE_PROD
JOIN aspel_sae50..MULT01
        ON INVE01.CVE_ART = MULT01.CVE_ART
        AND MULT01.CVE_ALM = 1
WHERE CVE_ALM = '1'
AND TIPO_ELE = 'P'
AND INVE01.STATUS = 'A'

-------------------------------------------------------------------------
select
	@factura_subtotal							as	factura_subtotal,
	@factura_subtotal * .16						as	factura_iva,
	@factura_subtotal * 1.16					as	factura_total,
	@nc_subtotal								as	nc_subtotal,
	@nc_subtotal * .16							as	nc_iva,
	@nc_subtotal * 1.16							as	nc_total,
	@factura_subtotal - @nc_subtotal			as	subtotal_total,
	(@factura_subtotal - @nc_subtotal) * .16	as	subtotal_iva,
	(@factura_subtotal - @nc_subtotal) * 1.16	as	subtotal_total_total,
	@factura_prendas							as	factura_prendas,
	@nc_prendas									as	nc_prendas,
	@factura_prendas - @nc_prendas				as	prendas_total,
	case when (@factura_prendas - @nc_prendas) <> 0 then
		(@factura_subtotal - @nc_subtotal) / (@factura_prendas - @nc_prendas)
	else
		0
	end
		as	promedio_x_prenda
	
-------------------------------------------------------------------------
select
	@pedidos_cap_periodo						as	pedidos_cap_periodo,
	@prendas_cap_periodo						as	prendas_cap_periodo,
	case when @pedidos_cap_periodo <> 0  then @prendas_cap_periodo / @pedidos_cap_periodo else 0 end	as	promedio_prendas_cap_periodo,
	@pedidos_en_proceso							as	pedidos_en_proceso,
	@prendas_en_proceso							as	prendas_en_proceso,
	case when @pedidos_en_proceso <> 0 then @prendas_en_proceso / @pedidos_en_proceso else 0 end	as	promedio_prendas_x_proceso,
	@liberados									as	liberados,
	@pedidos_en_proceso - @liberados			as	no_liberados,
	@clientes_atendidos_año_nat					as	clientes_atendidos_año_nat,
	@clientes_atendidos_en_periodo				as	clientes_atendidos_en_periodo
-------------------------------------------------------------------------
select
	@existencia_alm_Gral_1						as	existencia_alm_Gral_1,
	@existencia_alm_Surtido_3					as	existencia_alm_Surtido_3,
	@existencia_alm_Mostrador_4					as	existencia_alm_Mostrador_4,
	@existencia_alm_Pedidos_Par_40				as	existencia_alm_Pedidos_Par_40,
	@existencia_alm_Fact_Par_5					as	existencia_alm_Fact_Par_5,
	@existencia_alm_Esp_32						as	existencia_alm_Esp_32,
	@existencia_alm_Hospital_35					as	existencia_alm_Hospital_35,
	@existencia_prod_Proc						as	existencia_prod_Proc,
	@existencia_mp_2							as	existencia_mp_2
-------------------------------------------------------------------------
SELECT @importe_cuen_mo01_1 = SUM(TBL.IMPORTE) FROM (SELECT IMPORTE FROM aspel_sae50..CUEN_M01 WHERE SIGNO = 1 UNION ALL SELECT IMPORTE FROM aspel_sae50..CUEN_DET01  WHERE SIGNO = 1) AS TBL
SELECT @importe_cuen_mo01_0 = SUM(TBL.IMPORTE) FROM (SELECT IMPORTE FROM aspel_sae50..CUEN_M01 WHERE SIGNO = -1 UNION ALL SELECT IMPORTE FROM aspel_sae50..CUEN_DET01  WHERE SIGNO = -1) AS TBL

set @valor_cartera_total	= @importe_cuen_mo01_1 - @importe_cuen_mo01_0
set @relacion_venta_cartera	= case when (@factura_subtotal - @nc_subtotal) <> 0 then @valor_cartera_total / (@factura_subtotal - @nc_subtotal) else 0 end
select 
	@valor_cartera_total										as	valor_cartera_total,
	@relacion_venta_cartera										as	relacion_venta_cartera,
	@relacion_venta_cartera * 30								as	relacion_venta_cartera_dias
-------------------------------------------------------------------------


/*'Recepciones de maquila.  Este debe ser el ultimo prOCeso a calcular debido a que se utilizara un ciclo para determinar el
                              'numero de PROVeedores y que este sea variable
                              */
SELECT
        rtrim(ltrim(ISNULL(CLAVE_CLPV, '')))		as	CLAVE_CLPV,
        rtrim(ltrim(ISNULL(NOMBRE, '')))			as	NOMBRE,
        ISNULL(SUM(CANT), 0) AS TOTAL
FROM aspel_sae50..MINVE01 AS MINV01
JOIN aspel_sae50..PROV01
        ON PROV01.CLAVE = MINV01.CLAVE_CLPV
JOIN aspel_sae50..INVE_CLIB01
        ON MINV01.CVE_ART = INVE_CLIB01.CVE_PROD
WHERE MINV01.CVE_CPTO = 1
AND INVE_CLIB01.CAMPLIB1 = 'MAQUILA'
AND convert(date,FECHA_DOCU) between @fecha_desde AND  @fecha_hasta
GROUP BY CLAVE_CLPV,
         NOMBRE
ORDER BY CLAVE_CLPV
