ALTER PROCEDURE [dbo].[usp_frmTransferenciaXPedidoProcesar]
(
	@pedido int,
	@origen int,
	@destino int
)
AS
BEGIN
	set ansi_warnings off;
	SET NOCOUNT ON;

	declare @cliente varchar(500)
	declare @agrupador int
	declare @ULT_CVE_REG int
	declare @num_registros int
	
	IF (SELECT count(*) FROM aspel_sae50.dbo.UPPEDIDOS WHERE PEDIDO = @pedido AND F_LIBERADO IS NULL)>0
		BEGIN
			
			SELECT PY.NUM_PAR, PY.CVE_ART, PY.CANT, PY.PXS, PY.TIPO_PROD, PY.NUM_ALM, ISNULL(I.LIN_PROD, '') LINEA, 
				EXIST = ISNULL((SELECT EXIST FROM aspel_sae50.dbo.MULT01 WHERE CVE_ALM= case when I.LIN_PROD <> 'ESPE' then @origen else 32 end AND CVE_ART=PY.CVE_ART), 0),
				ORIGEN = (case when I.LIN_PROD <> 'ESPE' then @origen else 32 end)
			into #tmpP
			FROM aspel_sae50.dbo.PAR_FACTP01 PY LEFT JOIN aspel_sae50.dbo.INVE01 I ON PY.CVE_ART=I.CVE_ART 
			WHERE PY.TIPO_PROD='P' AND PY.CVE_DOC='P' + convert(varchar(10), @pedido) ORDER BY PY.NUM_PAR
			
			

			select @num_registros = count(*) from #tmpP

			if	(SELECT count(*) FROM #tmpP where EXIST<PXS) = 0
				BEGIN
					update aspel_sae50.dbo.TBLCONTROL01 set ULT_CVE = ULT_CVE + 1 WHERE ID_TABLA = 32
					SELECT @agrupador = ULT_CVE FROM aspel_sae50.dbo.TBLCONTROL01 WHERE ID_TABLA = 32


					SELECT @ULT_CVE_REG = ULT_CVE FROM aspel_sae50.dbo.TBLCONTROL01 WHERE ID_TABLA = 44

					insert into aspel_sae50.dbo.MINVE01(CVE_ART, ALMACEN, NUM_MOV, CVE_CPTO, FECHA_DOCU, TIPO_DOC, REFER, CLAVE_CLPV, 
						VEND, CANT, CANT_COST, PRECIO, COSTO, AFEC_COI, CVE_OBS, REG_SERIE, UNI_VENTA, E_LTPD, EXISTENCIA, TIPO_PROD, 
						FACTOR_CON, FECHAELAB, CTLPOL, CVE_FOLIO, SIGNO, COSTEADO, COSTO_PROM_INI, COSTO_PROM_FIN, DESDE_INVE)
					select t.CVE_ART, t.ORIGEN, @ULT_CVE_REG + ((ROW_NUMBER() OVER(ORDER BY t.NUM_PAR) * 2)-1), 58, 
						convert(varchar(103), getdate(), 103), 'M', 'P' + convert(varchar(10), @pedido), '', '', t.PXS, 0, 0, 0, '', 0, 0, 
						i.UNI_MED, 0, t.EXIST - t.PXS, 'P', 1, getdate(), 0, @agrupador, -1, 'S', 0 , 0, 'S'
					from #tmpP t
					left outer join aspel_sae50.dbo.INVE01 i on t.CVE_ART = i.CVE_ART

					update aspel_sae50.dbo.TBLCONTROL01 set ULT_CVE = ULT_CVE + (@num_registros * 2) WHERE ID_TABLA = 44


					UPDATE aspel_sae50.dbo.MULT01 SET EXIST = t.EXIST - t.PXS
					from aspel_sae50.dbo.MULT01 m inner join #tmpP t on m.CVE_ART = t.CVE_ART
					and LTRIM(RTRIM(m.CVE_ALM)) = convert(varchar(2), t.ORIGEN)

					insert into aspel_sae50.dbo.MINVE01(CVE_ART, ALMACEN, NUM_MOV, CVE_CPTO, FECHA_DOCU, TIPO_DOC, REFER, CLAVE_CLPV, 
						VEND, CANT, CANT_COST, PRECIO, COSTO, AFEC_COI, CVE_OBS, REG_SERIE, UNI_VENTA, E_LTPD, EXISTENCIA, TIPO_PROD, 
						FACTOR_CON, FECHAELAB, CTLPOL, CVE_FOLIO, SIGNO, COSTEADO, COSTO_PROM_INI, COSTO_PROM_FIN, DESDE_INVE)
					select t.CVE_ART, @destino, @ULT_CVE_REG + (ROW_NUMBER() OVER(ORDER BY t.NUM_PAR)*2), 
						7, convert(varchar(103), getdate(), 103), 'M', 'P' + convert(varchar(10), @pedido), '', '', t.PXS, 0, 0, 0, 
						'', 0, 0, i.UNI_MED, 0, m.EXIST + t.PXS, 'P', 1, getdate(), 0, @agrupador, 1, 'S', 0 , 0, 'S'
					from #tmpP t
					left outer join aspel_sae50.dbo.INVE01 i on t.CVE_ART = i.CVE_ART
					left outer join aspel_sae50.dbo.MULT01 m on t.CVE_ART = m.CVE_ART and m.CVE_ALM = @destino


					UPDATE aspel_sae50.dbo.MULT01 SET EXIST = m.EXIST + t.PXS
					from aspel_sae50.dbo.MULT01 m inner join #tmpP t on m.CVE_ART = t.CVE_ART
					WHERE LTRIM(RTRIM(m.CVE_ALM)) = @destino


					UPDATE aspel_sae50.dbo.UPPEDIDOS SET F_LIBERADO = getdate() WHERE PEDIDO = @pedido
					UPDATE aspel_sae50.dbo.CMT_DET SET CMT_ESTATUS = 'R' WHERE CMT_PEDIDO = @pedido

					select resultado='OK', agrupador=@agrupador
				END
			ELSE
				BEGIN
					select CVE_ART, ORIGEN, EXIST, PXS from #tmpP
				END

		END
	ELSE
		BEGIN
			select resultado='El Pedido tiene Fecha de Liberado en UPPEDIDOS, NO se Puede Transferir en Modo Matricial.'
		END



END