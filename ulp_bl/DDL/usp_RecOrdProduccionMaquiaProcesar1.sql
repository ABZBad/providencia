-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	Este sp se ejecuta una sóla vez por cada que presionan el botón procesar en el módulo "Recepción de fabricación OP + OM"
--		guarda en info en tablas base
-- Actualización : 06/03/2015
-- =============================================

--usp_RecOrdProduccionMaquiaProcesar1 40, '42CA', '1564', 1, 0, 0, 9
ALTER PROCEDURE usp_RecOrdProduccionMaquiaProcesar1
(
	@almacen int, 
	@clave_proveedor varchar(12), --enviar lblClave
	@orden_maquila varchar(20),  --enviar txtOMaquila
	@CostoConfeccion numeric(18, 2), --enviar txtCostoConfeccion
	@TotalPrendasOK int,  --enviar sumatoria de @tallaOK
	@TotalPrendas int,  --enviar sumatoria de @tallaOK + sumatoria de @tallaDef
	@EsquemaImp varchar(20) -- enviar txtEsquemaImp
)
AS
BEGIN
	set ansi_warnings off;

	declare @FOLIOSC_ULT_DOC varchar(20)
	--'Actualizacion de folio en COM001 ---------------------1---------------------------- Apartado de folios en COM001 PARA LA RECEPCION
		--SELECT ULT_DOC AS ULT_DOCR, FECH_ULT_DOC AS ULTFECHF FROM aspel_sae50.dbo.FOLIOSC01 WHERE TIP_DOC = 'c' AND SERIE = 'STAND.'
		update aspel_sae50.dbo.FOLIOSC01 set ULT_DOC = ULT_DOC + 1, FECH_ULT_DOC = CAST(getdate() AS DATE) 
		WHERE TIP_DOC = 'c' AND SERIE = 'STAND.'
		SELECT @FOLIOSC_ULT_DOC = right('                    '+convert(varchar(8), ULT_DOC), 20) 
		FROM aspel_sae50.dbo.FOLIOSC01 WHERE TIP_DOC = 'c' AND SERIE = 'STAND.'

		----búsqueda para determinar el impuesto
		declare @impuesto numeric(18, 2)

		SELECT @impuesto = @TotalPrendas * @CostoConfeccion * IMPUESTO4 / 100
		FROM aspel_sae50.dbo.IMPU01 WHERE LTRIM(RTRIM(CVE_ESQIMPU)) = @EsquemaImp
				
		insert into aspel_sae50.dbo.COMPC01(TIP_DOC, CVE_DOC, CVE_CLPV, STATUS, SU_REFER, FECHA_DOC, FECHA_REC, FECHA_PAG, 
			CAN_TOT, IMP_TOT1, IMP_TOT2, IMP_TOT3, DES_TOT, IMP_TOT4, DES_FIN, TOT_IND, OBS_COND, CVE_OBS, NUM_ALMA, ACT_CXP, 
			ACT_COI, NUM_MONED, TIPCAMB, ENLAZADO, TIP_DOC_E, NUM_PAGOS, FECHAELAB, CTLPOL, ESCFD, BLOQ, DES_FIN_PORC, DES_TOT_PORC, IMPORTE)
		values('c', @FOLIOSC_ULT_DOC, right('          '+@clave_proveedor, 10), 'E', @orden_maquila, CAST(getdate() AS DATE), 
		CAST(getdate() AS DATE), CAST(getdate() AS DATE), (@TotalPrendasOK*@CostoConfeccion),
		0, 0, 0, 0, @impuesto, 0, 0, '', 0, @almacen, '', '', 1, 1, 'O', 'O', 0, getdate(), 0, 'N', 'N', 0, 0,
		((@TotalPrendas*@CostoConfeccion)+@impuesto))

		--esta actualización sí existe en su sistema per al parecer se les olvidó darle el método update a su objeto que actualiza
		--update aspel_sae50.dbo.COMPO01 set ENLAZADO = 'T', TIP_DOC_E = 'c' WHERE LTRIM(RTRIM(CVE_DOC)) = @orden_maquila	

END
