/*
	Actualización del SP 06/03/2015
	Correcciones varias
*/
ALTER PROCEDURE usp_RecOrdProduccionMaquiaProcesar
(
	@almacen int, 
	@NUM_REG int, 
	@REFERENCIA VARCHAR(20), 
	@PRODUCTO VARCHAR(20),
	@tallaOK int, 
	@tallaDef int, 	
	@defectuosos varchar(20), --poner el valor de la caja de texto
	@orden_maquila varchar(20),  --enviar txtOMaquila
	@CostoConfeccion numeric(18, 2), --enviar txtCostoConfeccion
	@TotalPrendas numeric(18, 2),  --enviar sumatoria de @tallaOK + sumatoria de @tallaDef
	@EsquemaImp varchar(20), -- enviar txtEsquemaImp
	@ConsecutivoReg int,	--id que recibirá derivado del ciclo en vs.net
	@prefijo varchar(1)		--se envía el txtPrefijo
)
AS
BEGIN
	SET NOCOUNT ON;
	set ansi_warnings off;

	--usp_RecOrdProduccionMaquia '36944', '1564', '40'

	--Paso 1. Con el recordset generado al buscar la orden se procede a dar de baja los articulos

	--Paso 1.1 Aumenta producto terminado en INVE01 y en MULT01 en el ALMACEN 1, 'Paso 3.1   Disminuye PendXRecib en INVE01, en MULT01 se decrementa ComprasXRec

	--actualiza valores de las prendas, primer renglón del grid, esto se hará columna por columna del grid
	UPDATE aspel_sae50.dbo.INVE01 SET EXIST = EXIST + @tallaOK, COMP_X_REC = COMP_X_REC - (@tallaDef+@tallaOK) 
	WHERE ltrim(rtrim(CVE_ART)) = @PRODUCTO
	UPDATE aspel_sae50.dbo.MULT01 SET EXIST = EXIST + @tallaOK, COMP_X_REC = COMP_X_REC - (@tallaDef+@tallaOK) 
	WHERE CVE_ALM = @almacen AND rtrim(ltrim(CVE_ART)) = @PRODUCTO

	--actualiza valores de las prendas, segundo renglón del grid, esto se hará columna por columna del grid
	UPDATE aspel_sae50.dbo.INVE01 SET EXIST = EXIST + @tallaDef WHERE ltrim(rtrim(CVE_ART)) = 'PRENDASA' --valores fijos de la condición
	UPDATE aspel_sae50.dbo.MULT01 SET EXIST = EXIST + @tallaDef WHERE CVE_ALM = '1' AND rtrim(ltrim(CVE_ART)) = 'PRENDASA' --valores fijos de la condición

	--Paso 2 Aumenta CANTTERM en ord_fab01, si se terminaron todas entonces el ESTATUS = 3 (terminado), el num_reg es el valor contenido en la consulta inicial
	--el valor a asignar es la sumatoria del registro 1 y el registro 2 del la columna en curso
	UPDATE aspel_prod30.dbo.ORD_FAB01 SET CANTTERM = CANTTERM + (@tallaDef+@tallaOK) WHERE NUM_REG = @NUM_REG

	--'Paso 3 Si la orden esta completa le cambia el STATUS(sólo se condiciona el num_reg, lo demás es fijo)
	UPDATE aspel_prod30.dbo.ORD_FAB01 SET STATUS = 3 WHERE NUM_REG = @NUM_REG AND CANTIDAD - CANTTERM = 0 

	--    'Paso 4 Se genera un registro en SEG_FAB de acuerdo a la CANTIDAD recibida
	--        '4.1    Se actualiza el folio de SEG_FAB
	--SELECT * From aspel_prod30.dbo.SEG_F0B01--consulta para actualizar registro
	update aspel_prod30.dbo.SEG_F0B01 set NUM_REGS = NUM_REGS +1, ULT_CLV = ULT_CLV +1
	
	--prepara valores a insertar en SEG_FAB01
	declare @chNUM_REG varchar(16)
	set @chNUM_REG=  right('                '+convert(varchar(16), @NUM_REG), 16)

	declare @num_SEG_F0B int
	SELECT @num_SEG_F0B = NUM_REGS From aspel_prod30.dbo.SEG_F0B01
	
	--se inserta en SEG_FAB01
	insert into aspel_prod30.dbo.SEG_FAB01(NUM_REG, ORDEN, PROCESO, FECHMOV, ESTATUS, TIPOMOV, COMPONENTE, ALMACEN, TIPOCMP, REFERENCIA, 
		CANTIDAD, CANTDEV, CSTOUNI, NUMUSU, NUMSERIE, CVETRAB, ACTSAE, NINDUNIT, CSTOUNIEST, RESTO)
	values(@num_SEG_F0B, @chNUM_REG, '', getdate(), 0, '', @PRODUCTO, @almacen, 0, @REFERENCIA, (@tallaDef+@tallaOK), 
		0, 0, 0, 0, '', 1, 0, 0, '')
	
	--Paso 5.A Se genera un registro en MINV01 3 ENTRADA DE FABRICA --------- PARA LOS ARTICULOS OK
	---prepara valores para inserción
	declare @num_TBLCONTROL int
	update aspel_sae50.dbo.TBLCONTROL01 set ULT_CVE = ULT_CVE + 1 WHERE ID_TABLA = 32 	
	SELECT @num_TBLCONTROL = ULT_CVE FROM aspel_sae50.dbo.TBLCONTROL01 WHERE ID_TABLA = 32 

	declare @num_TBLCONTROL44 int
	update aspel_sae50.dbo.TBLCONTROL01 set ULT_CVE = ULT_CVE + 1 WHERE ID_TABLA = 44	
	SELECT @num_TBLCONTROL44 = ULT_CVE FROM aspel_sae50.dbo.TBLCONTROL01 WHERE ID_TABLA = 44

	--'Busco los datos del articulo en INVE01
	declare @INVE_UNI_MED varchar(10)	
	--SELECT UNI_MED,UNI_EMP,EXIST,ULT_COSTO,TIPO_ELE,FAC_CONV FROM aspel_sae50.dbo.INVE01 WHERE LTRIM(RTRIM(CVE_ART)) = @PRODUCTO
	SELECT @INVE_UNI_MED = UNI_MED FROM aspel_sae50.dbo.INVE01 WHERE LTRIM(RTRIM(CVE_ART)) = @PRODUCTO

	--'Busco informacion de la existencia en MULT01
	declare @MULT_EXIST float	
	--SELECT CVE_ART AS CLV_ART,EXIST FROM aspel_sae50.dbo.MULT01 WHERE LTRIM(RTRIM(CVE_ART)) = @PRODUCTO
	SELECT top 1 @MULT_EXIST = EXIST FROM aspel_sae50.dbo.MULT01 WHERE LTRIM(RTRIM(CVE_ART)) = @PRODUCTO

	--'Se reconstruye el costo del articulo sin considerar mano de obra
	declare @COSTOMINVE numeric(18, 4)
	SELECT @COSTOMINVE = SUM(CANTIDAD * ULT_COSTO) FROM aspel_prod30.dbo.PT_DET01 
	JOIN aspel_sae50.dbo.INVE01 ON aspel_prod30.dbo.PT_DET01.COMPONENTE = INVE01.CVE_ART WHERE CLAVE = @PRODUCTO AND TIPOCOMP = '49'

	--inserta en MINVE01
	insert into aspel_sae50.dbo.MINVE01(CVE_ART, ALMACEN, NUM_MOV, CVE_CPTO, FECHA_DOCU, TIPO_DOC, REFER, CLAVE_CLPV, VEND, 
		CANT, CANT_COST, PRECIO, COSTO, COSTO_PROM_INI, COSTO_PROM_FIN, AFEC_COI, CVE_OBS, REG_SERIE, UNI_VENTA, 
		E_LTPD, EXISTENCIA, TIPO_PROD, FACTOR_CON, FECHAELAB, CTLPOL, CVE_FOLIO, SIGNO, COSTEADO, DESDE_INVE)
	values(@PRODUCTO, @almacen, @num_TBLCONTROL44, 3, cast(getdate() as date), 'M', @REFERENCIA, '', '', @tallaOK, 
	0, 0, @COSTOMINVE, @COSTOMINVE, @COSTOMINVE, '', 0, 0, @INVE_UNI_MED, 0, @MULT_EXIST, 'P', 1, getdate(), 0, 
	@num_TBLCONTROL, 1, 'S', 'S')
	
	IF @tallaDef>0
	BEGIN		
		update  aspel_sae50.dbo.TBLCONTROL01 set ULT_CVE = ULT_CVE + 1 WHERE ID_TABLA = 32 
		SELECT @num_TBLCONTROL = ULT_CVE FROM aspel_sae50.dbo.TBLCONTROL01 WHERE ID_TABLA = 32 

		update aspel_sae50.dbo.TBLCONTROL01 set ULT_CVE = ULT_CVE + 1 WHERE ID_TABLA = 44	
		SELECT @num_TBLCONTROL44 = ULT_CVE FROM aspel_sae50.dbo.TBLCONTROL01 WHERE ID_TABLA = 44

		--SELECT CVE_ART AS CLV_ART,UNI_MED,UNI_EMP,EXIST,ULT_COSTO,TIPO_ELE,FAC_CONV FROM aspel_sae50.dbo.INVE01 WHERE LTRIM(RTRIM(CVE_ART)) = @PRODUCTO
		--SELECT CVE_ART AS CLV_ART,EXIST FROM aspel_sae50.dbo.MULT01 WHERE LTRIM(RTRIM(CVE_ART)) = @PRODUCTO

		--inserta en MINVE01(revisar insert)
		insert into aspel_sae50.dbo.MINVE01(CVE_ART, ALMACEN, NUM_MOV, CVE_CPTO, FECHA_DOCU, TIPO_DOC, REFER, CLAVE_CLPV, VEND, 
			CANT, CANT_COST, PRECIO, COSTO, COSTO_PROM_INI, COSTO_PROM_FIN, AFEC_COI, CVE_OBS, REG_SERIE, UNI_VENTA, 
			E_LTPD, EXISTENCIA, TIPO_PROD, FACTOR_CON, FECHAELAB, CTLPOL, CVE_FOLIO, SIGNO, COSTEADO, DESDE_INVE)
		values(@PRODUCTO, @almacen, @num_TBLCONTROL44, 3, cast(getdate() as date), 'M', @REFERENCIA, '', '', @tallaDef, 
		0, 0, @COSTOMINVE, @COSTOMINVE, @COSTOMINVE, '', 0, 0, @INVE_UNI_MED, 0, @MULT_EXIST, 'P', 1, getdate(), 0, 
		@num_TBLCONTROL, 1, 'S', 'S')

		--'Paso 5.C Se genera un registro en MINV01 60 SALIDA POR DEFECTUOSO ----------------- PARA LOS ARTICULOS DEF
		update  aspel_sae50.dbo.TBLCONTROL01 set ULT_CVE = ULT_CVE + 1 WHERE ID_TABLA = 32 
		SELECT @num_TBLCONTROL = ULT_CVE FROM aspel_sae50.dbo.TBLCONTROL01 WHERE ID_TABLA = 32 

		update aspel_sae50.dbo.TBLCONTROL01 set ULT_CVE = ULT_CVE + 1 WHERE ID_TABLA = 44	
		SELECT @num_TBLCONTROL44 = ULT_CVE FROM aspel_sae50.dbo.TBLCONTROL01 WHERE ID_TABLA = 44

		--inserta en MINVE01(revisar insert)
		insert into aspel_sae50.dbo.MINVE01(CVE_ART, ALMACEN, NUM_MOV, CVE_CPTO, FECHA_DOCU, TIPO_DOC, REFER, CLAVE_CLPV, VEND, 
			CANT, CANT_COST, PRECIO, COSTO, COSTO_PROM_INI, COSTO_PROM_FIN, AFEC_COI, CVE_OBS, REG_SERIE, UNI_VENTA, 
			E_LTPD, EXISTENCIA, TIPO_PROD, FACTOR_CON, FECHAELAB, CTLPOL, CVE_FOLIO, SIGNO, COSTEADO, DESDE_INVE)
		values(@PRODUCTO, @almacen, @num_TBLCONTROL44, 60, cast(getdate() as date), 'M', @REFERENCIA, '', '', @tallaDef, 
		0, 0, @COSTOMINVE, @COSTOMINVE, @COSTOMINVE, '', 0, 0, @INVE_UNI_MED, 0, @MULT_EXIST, 'P', 1, getdate(), 0, 
		@num_TBLCONTROL, -1, 'S', 'S')


		--'Paso 5.D Se genera un registro en MINV01 6 ENTRADA POR AJUSTE DE PRENDASA -----------------
		update  aspel_sae50.dbo.TBLCONTROL01 set ULT_CVE = ULT_CVE + 1 WHERE ID_TABLA = 32 
		SELECT @num_TBLCONTROL = ULT_CVE FROM aspel_sae50.dbo.TBLCONTROL01 WHERE ID_TABLA = 32 

		update aspel_sae50.dbo.TBLCONTROL01 set ULT_CVE = ULT_CVE + 1 WHERE ID_TABLA = 44	
		SELECT @num_TBLCONTROL44 = ULT_CVE FROM aspel_sae50.dbo.TBLCONTROL01 WHERE ID_TABLA = 44


		--'Busco informacion de la existencia en MULT01 de defectuosos
		declare @MULT_EXIST_DEFEC float	
		--SELECT CVE_ART AS CLV_ART,EXIST FROM aspel_sae50.dbo.MULT01 WHERE LTRIM(RTRIM(CVE_ART)) = @PRODUCTO
		SELECT top 1 @MULT_EXIST_DEFEC = EXIST FROM aspel_sae50.dbo.MULT01 WHERE LTRIM(RTRIM(CVE_ART)) = @defectuosos


		--inserta en MINVE01(revisar insert)
		insert into aspel_sae50.dbo.MINVE01(CVE_ART, ALMACEN, NUM_MOV, CVE_CPTO, FECHA_DOCU, TIPO_DOC, REFER, CLAVE_CLPV, VEND, 
			CANT, CANT_COST, PRECIO, COSTO, COSTO_PROM_INI, COSTO_PROM_FIN, AFEC_COI, CVE_OBS, REG_SERIE, UNI_VENTA, 
			E_LTPD, EXISTENCIA, TIPO_PROD, FACTOR_CON, FECHAELAB, CTLPOL, CVE_FOLIO, SIGNO, COSTEADO, DESDE_INVE)
		values(@defectuosos, @almacen, @num_TBLCONTROL44, 6, cast(getdate() as date), 'M', @REFERENCIA, '', '', @tallaDef, 
		0, 0, 0, 0, 0, '', 0, 0, @INVE_UNI_MED, 0, @MULT_EXIST_DEFEC, 'P', 1, getdate(), 0, 
		@num_TBLCONTROL, 1, 'S', 'S')
	END


----------------------------------------------------------------------
----------------
-----------------------------------------------------------------------

		declare @IMPUESTO4 numeric(18, 2)
		declare @IMP1APLICA numeric(18, 2)
		declare @IMP2APLICA numeric(18, 2)
		declare @IMP3APLICA numeric(18, 2)
		declare @IMP4APLICA numeric(18, 2)

		SELECT @IMPUESTO4= IMPUESTO4, @IMP1APLICA= IMP1APLICA, @IMP2APLICA= IMP2APLICA, @IMP3APLICA= IMP3APLICA, @IMP4APLICA= IMP4APLICA
		FROM aspel_sae50.dbo.IMPU01 WHERE LTRIM(RTRIM(CVE_ESQIMPU)) = @EsquemaImp

	declare @FOLIOSC_ULT_DOC varchar(20)

	SELECT @FOLIOSC_ULT_DOC = right('                    '+convert(varchar(8), ULT_DOC), 20) 
	FROM aspel_sae50.dbo.FOLIOSC01 WHERE TIP_DOC = 'c' AND SERIE = 'STAND.'

	--inserta en PAR_COMPC01	
	insert into aspel_sae50.dbo.PAR_COMPC01(CVE_DOC, NUM_PAR, CVE_ART, CANT, PXR, PREC, COST, IMPU1, IMPU2, IMPU3, IMPU4, 
		IMP1APLA, IMP2APLA, IMP3APLA, IMP4APLA, TOTIMP1, TOTIMP2, TOTIMP3, TOTIMP4, DESCU, ACT_INV, TIP_CAM, UNI_VENTA, 
		TIPO_ELEM, TIPO_PROD, CVE_OBS, E_LTPD, REG_SERIE, FACTCONV, NUM_ALM, NUM_MOV, TOT_PARTIDA)
	values(@FOLIOSC_ULT_DOC, @ConsecutivoReg, 'X' + @PRODUCTO, @tallaOK, @tallaOK, 0, @CostoConfeccion, 0, 0, 0, @IMPUESTO4, 
		@IMP1APLICA, @IMP2APLICA, @IMP3APLICA, @IMP4APLICA, 0, 0, 0, (@tallaOK * @CostoConfeccion * @IMPUESTO4/100), 
		0, '', 1, 'pz', 'N', 'P', 0, 0, 0, 1, @almacen, 0, (@tallaOK * @CostoConfeccion))

	--'Paso 4.1 Realizar los cambios en las partidas de la OC original para reflejar lo recibido ------------------------------ 4.1 modificaciones en COM0Y1
	declare @PAR_COMPO_NUM_PAR int
	update aspel_sae50.dbo.PAR_COMPO01 set PXR = PXR -(@tallaOK + @tallaDef) 
	WHERE LTRIM(RTRIM(CVE_DOC)) = @orden_maquila AND LTRIM(RTRIM(CVE_ART)) = @prefijo + @PRODUCTO

	select @PAR_COMPO_NUM_PAR = NUM_PAR from aspel_sae50.dbo.PAR_COMPO01
	WHERE LTRIM(RTRIM(CVE_DOC)) = @orden_maquila AND LTRIM(RTRIM(CVE_ART)) = @prefijo + @PRODUCTO
	
	--inserta en DOCTOSIGC01
	insert into aspel_sae50.dbo.DOCTOSIGC01(TIP_DOC, CVE_DOC, ANT_SIG, TIP_DOC_E, CVE_DOC_E, PARTIDA, PART_E, CANT_E)
	values('o', right('                    '+@orden_maquila, 20), 'S', 'c',   right('                    '+@FOLIOSC_ULT_DOC, 20), 
		@ConsecutivoReg, @PAR_COMPO_NUM_PAR, (@tallaOK + @tallaDef))

	--vuelve a insertar en DOCTOSIGC01
	insert into aspel_sae50.dbo.DOCTOSIGC01(TIP_DOC, CVE_DOC, ANT_SIG, TIP_DOC_E, CVE_DOC_E, PARTIDA, PART_E, CANT_E)
	values('c', right('                    '+@FOLIOSC_ULT_DOC, 20), 'A', 'o',  right('                    '+@orden_maquila, 20), 
		@ConsecutivoReg, @PAR_COMPO_NUM_PAR, (@tallaOK + @tallaDef))

	
END