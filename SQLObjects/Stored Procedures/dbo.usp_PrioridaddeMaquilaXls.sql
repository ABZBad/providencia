SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Israel Aragón
-- Create date: 18/08/2014
-- Description:	Consulta que devuelve registros para poblar información en el módulo frmPrioridaddeMaquila cuando se exporta a excel
-- =============================================

--usp_PrioridaddeMaquilaXls '42PA', 'X'
CREATE PROCEDURE [dbo].[usp_PrioridaddeMaquilaXls]
(
	@proveedor varchar(20), 
	@Prefijo varchar(10)
)
AS
BEGIN

	set ansi_warnings off;
	SET NOCOUNT ON;

/*	
	declare @proveedor varchar(20)
	declare @Prefijo varchar(10)

	set @proveedor = '42PA'
	set @Prefijo='X'
	*/

	SELECT comp.CVE_CLPV,FECHA_DOC,par.CVE_DOC,SUBSTRING(CVE_ART,1,9) AS MODELO,SUM(PXR) AS SUMA,
		PRIORIDAD,comp.CVE_OBS AS OBS_COMP 
	into #tmp
	FROM aspel_sae50.dbo.COMPO01 comp JOIN aspel_sae50.dbo.PAR_COMPO01 par ON LTRIM(RTRIM(comp.CVE_DOC)) = LTRIM(RTRIM(par.CVE_DOC)) 
	JOIN aspel_sae50.dbo.PRIORIDAD_MAQUILA pm ON LTRIM(RTRIM(par.CVE_DOC)) = LTRIM(RTRIM(OC)) 
	WHERE comp.STATUS <> 'C' and PXR <> 0 AND comp.TIP_DOC = 'o' AND LTRIM(RTRIM(CVE_CLPV)) = @proveedor AND SUBSTRING(CVE_ART,1,1) = @Prefijo 
		and PRIORIDAD <> 1000
	GROUP BY FECHA_DOC,par.CVE_DOC,CVE_CLPV,SUBSTRING(CVE_ART,1,9),PRIORIDAD,comp.CVE_OBS 
	ORDER BY PRIORIDAD,FECHA_DOC,MODELO


	select CVE_ART = LTRIM(RTRIM(SUBSTRING(CVE_ART,1,9))), DESCR = min(DESCR) 
	into #inve_grup
	from aspel_sae50.dbo.INVE01 	
	--where LTRIM(RTRIM(SUBSTRING(CVE_ART,1,9))) = 'XCA50PZZZ'
	group by LTRIM(RTRIM(SUBSTRING(CVE_ART,1,9)))

	
	SELECT distinct CANT=tmp.SUMA, tmp.MODELO, DESCRIPCION=inve.DESCR, OC=LTRIM(RTRIM(tmp.CVE_DOC)), tmp.PRIORIDAD, 
		prov.NOMBRE, 		
		X_OBSER = obs.STR_OBS, 
		--obs.CVE_OBS AS NUM_REG, 
		tallas = dbo.udf_Regresa_tallas_concatenadas_por_CVE_DOC(LTRIM(RTRIM(tmp.CVE_DOC)))
		--tallas = replicate(' ', 400)
	--into #final
	FROM #tmp tmp left outer join aspel_sae50.dbo.PROV01 prov on tmp.CVE_CLPV = LTRIM(RTRIM(CLAVE))
	left outer join #inve_grup inve on CVE_ART = tmp.MODELO	
	--left outer join aspel_sae50.dbo.INVE01 inve on LTRIM(RTRIM(SUBSTRING(CVE_ART,1,9))) = tmp.MODELO
	left outer join aspel_sae50.dbo.OBS_DOCF01 obs on tmp.OBS_COMP = obs.CVE_OBS
	order by PRIORIDAD
	
	--select * from aspel_sae50.dbo.INVE01 where LTRIM(RTRIM(SUBSTRING(CVE_ART,1,9)))='XPA6MMOSH'

--select * from #tmp 	
--
/*
		-------------------actualiza tallas		
		declare @CVE_DOC_com  char(10)
		declare @Tallas_a_asignar varchar(1000)		
		set @Tallas_a_asignar = ''
		declare @CVE_DOC int, @CVE_ART varchar(12), @PXR varchar(12)
		declare cursor_tallas CURSOR FOR 
			SELECT tmp.OC, tallas.CVE_ART, tallas.PXR
			FROM #final tmp left outer join aspel_sae50.dbo.PAR_COMPO01 tallas on tmp.OC = LTRIM(RTRIM(tallas.CVE_DOC)) 
			where PXR <> 0

		OPEN cursor_tallas
		FETCH NEXT FROM cursor_tallas INTO @CVE_DOC, @CVE_ART, @PXR
		set @CVE_DOC_com = @CVE_DOC
		WHILE @@FETCH_STATUS = 0
		BEGIN			
			if @CVE_DOC_com <> @CVE_DOC
			begin
				--SELECT @Tallas_a_asignar, @CVE_DOC_com
				update #final set TALLAS = @Tallas_a_asignar where OC = @CVE_DOC
				set @Tallas_a_asignar = ''
				set @CVE_DOC_com = @CVE_DOC
			end
			set @Tallas_a_asignar = @Tallas_a_asignar + RIGHT(RTRIM(LTrim(@CVE_ART)), 4) + '/' + CONVERT(varchar(12), @PXR) + ' '
			--print @Tallas_a_asignar 
			--print @CVE_DOC_com
			FETCH NEXT FROM cursor_tallas INTO @CVE_DOC, @CVE_ART, @PXR
		END
		update #final set TALLAS = @Tallas_a_asignar where OC = @CVE_DOC
		CLOSE cursor_tallas
		DEALLOCATE cursor_tallas


	select * from #final order by OC
*/
	--drop table #final
	drop table #tmp

END
GO
