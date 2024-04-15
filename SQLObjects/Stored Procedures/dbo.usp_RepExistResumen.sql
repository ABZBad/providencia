
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
--Modificación para versión 8.0.0.43 del sistema SIP
/*
	Se agrega Isnull(,0) ya que en el reporte de xistencias mostraba vacío en vez de Cero (0)
	7-may-2015: se modifica consulta para que devuelva totalizados igual al SIP7 7.15.1
*/
CREATE PROCEDURE [dbo].[usp_RepExistResumen]
(
	@tipo int, --1. SMAX		2. SMIN			3.SMIN SIN PP
	@CVE_ALM int, 
	@resumen int --1.resumen existencia Total		2.resumen modelos
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT ic.CAMPLIB4 AS CAMPOINTU, m.CVE_ALM AS ALMACEN,m.CVE_ART AS CLV_ART,i.DESCR,m.EXIST,
		m.COMP_X_REC,m.STOCK_MIN,m.STOCK_MAX,PEND_SURT, LTRIM(RTRIM(ic.CAMPLIB1)) AS CAMPOSTRU1 
	into #tot
	FROM aspel_sae50.dbo.INVE01 i JOIN aspel_sae50.dbo.INVE_CLIB01 ic ON i.CVE_ART = ic.CVE_PROD JOIN aspel_sae50.dbo.MULT01 m ON i.CVE_ART = m.CVE_ART 
	WHERE m.CVE_ALM = @CVE_ALM AND LTRIM(RTRIM(ic.CAMPLIB1)) <> '' AND TIPO_ELE = 'P' AND i.STATUS = 'A' 
	ORDER BY ic.CAMPLIB4 ,substring(i.CVE_ART,1,8),substring(i.CVE_ART,9,4)

	--final
	if (@resumen=1)--1.resumen existencia Total		
		begin
			if (@tipo in(1,2))
			BEGIN
				select [Existencia TOTAL] = sum(EXIST), 
					[PP TOTAL]=sum(COMP_X_REC), 
					[S Min TOTAL]=sum(STOCK_MIN), 
					[S Max TOTAL]=sum(STOCK_MAX), 
					[Pend x Surtir TOTAL] = '', 
					[MH TOTAL2]= case @tipo
								when 1 then--SMAX
										 sum(
											case when ((STOCK_MAX-(EXIST+COMP_X_REC))*-1)>0 then 0 else ((STOCK_MAX-(EXIST+COMP_X_REC))*-1) end
											)
								when 2 then--SMIN
										 sum(
											case when ((STOCK_MIN-(EXIST+COMP_X_REC))*-1)>0 then 0 else ((STOCK_MIN-(EXIST+COMP_X_REC))*-1) end
											)
							  end
				from #tot
			END
			ELSE
			BEGIN
				SELECT 
					TOTAL_ALM_GEN = SUM(case when CVE_ALM =1 then m.EXIST else 0 end), 
					RECOMENDADO_ALM_GEN = SUM(case when CVE_ALM =1 then m.STOCK_MIN else 0 end), 

					TOTAL_ALM_SURT = SUM(case when CVE_ALM =3 then m.EXIST else 0 end), 
					RECOMENDADO_ALM_SURT = SUM(case when CVE_ALM =3 then m.STOCK_MIN else 0 end), 

					TOTAL_ALM_MOST = SUM(case when CVE_ALM =4 then m.EXIST else 0 end), 
					RECOMENDADO_ALM_MOST = SUM(case when CVE_ALM =4 then m.STOCK_MIN else 0 end)
				
				FROM aspel_sae50.dbo.INVE01 i JOIN aspel_sae50.dbo.INVE_CLIB01 ic ON i.CVE_ART = ic.CVE_PROD 
					JOIN aspel_sae50.dbo.MULT01 m ON i.CVE_ART = m.CVE_ART 
				WHERE CVE_ALM in (1,3,4) AND TIPO_ELE = 'P' AND i.STATUS = 'A'
			END
			
		end
	else --2.resumen modelos
		begin
			SELECT LTRIM(RTRIM(CAMPLIB1)) AS CAMPOSTRU1 into #cat
			FROM aspel_sae50.dbo.INVE_CLIB01 	
			WHERE CAMPLIB1 IS NOT NULL AND LTRIM(RTRIM(CAMPLIB1)) <> '' GROUP BY LTRIM(RTRIM(CAMPLIB1))
				   
			select c.CAMPOSTRU1, existencia= isnull(sum(t.EXIST),0)
			from #cat c 
			left outer join #tot t on c.CAMPOSTRU1 = t.CAMPOSTRU1
			group by c.CAMPOSTRU1
			order by c.CAMPOSTRU1

		end

END
GO
