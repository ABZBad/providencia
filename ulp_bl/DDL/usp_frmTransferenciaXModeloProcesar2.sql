/*
Se cambia parámetro de Numeric(n) a int, marcaba desbordamiento en la cantidad y agrupador
Se metió Isnull al determinar el costo ya que estaba guardando valores null en vez de cero en MINVE01
*/
ALTER PROCEDURE usp_frmTransferenciaXModeloProcesar
(
	@CVE_ART varchar(12), 
	@Origen varchar(2),
	@Destino varchar(2),
	@cantidad int, 
	@usuario varchar(20), 
	@agrupador int, --id que se incrementa en el load de la forma 
	@resultado varchar(500) output
)
AS
BEGIN
	set ansi_warnings off;
	SET NOCOUNT ON;

	declare @EXIST numeric(10)
	declare @ULT_CVE numeric(10)
	declare @COSTO numeric(18, 2)
	declare @UNI_MED varchar(18)

	--se debe incrementar el valor que traiga ULT_CVE más 2 por cada vuelta del ciclo	
	SELECT @ULT_CVE = ULT_CVE FROM aspel_sae50.dbo.TBLCONTROL01 WHERE ID_TABLA = 44
	update aspel_sae50.dbo.TBLCONTROL01 set ULT_CVE = ULT_CVE + 2 WHERE ID_TABLA = 44

	--consulta para obtener la EXIST de cada producto en cada vuelta del ciclo
	SELECT @EXIST = EXIST FROM aspel_sae50.dbo.MULT01 WHERE CVE_ART = @CVE_ART AND CVE_ALM = CONVERT(INT, @Origen)

	IF (@EXIST>=@cantidad)
		BEGIN
			-------------------------------------movimientos de origen
			SELECT TOP 1 @COSTO = isnull(COSTO,0) FROM aspel_sae50.dbo.MINVE01 WHERE CVE_ART = @CVE_ART AND CVE_CPTO = 1 AND COSTO > 0
			if @@rowcount = 0
				begin
					set @COSTO = 0
				end
			
			SELECT @UNI_MED = UNI_MED FROM aspel_sae50.dbo.INVE01 where CVE_ART = @CVE_ART
			
			--en caso de que la cantidad sea menor a la existencia se inserta en MINVE01
			insert into aspel_sae50.dbo.MINVE01(CVE_ART, ALMACEN, NUM_MOV, CVE_CPTO, FECHA_DOCU, TIPO_DOC, REFER, CLAVE_CLPV, VEND, CANT, 
				CANT_COST, PRECIO, COSTO, AFEC_COI, CVE_OBS, REG_SERIE, UNI_VENTA, E_LTPD, EXISTENCIA, TIPO_PROD, FACTOR_CON, FECHAELAB, 
				CTLPOL, CVE_FOLIO, SIGNO, COSTEADO, COSTO_PROM_INI, COSTO_PROM_FIN, DESDE_INVE)
			values(@CVE_ART, convert(int, @Origen), @ULT_CVE + 1, 58, cast(getdate() as date), 'M', @usuario, '', '', @cantidad, 
				0, 0, @COSTO, '', 0, 0, @UNI_MED, 0, (@EXIST - @cantidad), 'P', 1, getdate(), 0, @agrupador, -1, 'S', @COSTO, @COSTO, 'S')

	
			update aspel_sae50.dbo.MULT01 set EXIST = @EXIST - @cantidad WHERE CVE_ART = @CVE_ART AND CVE_ALM = CONVERT(INT, @Origen)
	
			-------------------------------------movimientos de destino

			SELECT @EXIST = EXIST FROM aspel_sae50.dbo.MULT01 WHERE CVE_ART = @CVE_ART AND CVE_ALM = CONVERT(INT, @Destino)


			insert into aspel_sae50.dbo.MINVE01(CVE_ART, ALMACEN, NUM_MOV, CVE_CPTO, FECHA_DOCU, TIPO_DOC, REFER, CLAVE_CLPV, VEND, CANT, 
				CANT_COST, PRECIO, COSTO, AFEC_COI, CVE_OBS, REG_SERIE, UNI_VENTA, E_LTPD, EXISTENCIA, TIPO_PROD, FACTOR_CON, FECHAELAB, 
				CTLPOL, CVE_FOLIO, SIGNO, COSTEADO, COSTO_PROM_INI, COSTO_PROM_FIN, DESDE_INVE)
			values(@CVE_ART, convert(int, @Destino), @ULT_CVE + 2, 7, cast(getdate() as date), 'M', @usuario, '', '', @cantidad, 
				0, 0, @COSTO, '', 0, 0, @UNI_MED, 0, (@EXIST + @cantidad), 'P', 1, getdate(), 0, @agrupador, 1, 'S', @COSTO, @COSTO, 'S')

			update aspel_sae50.dbo.MULT01 set EXIST = @EXIST + @cantidad WHERE CVE_ART = @CVE_ART AND CVE_ALM = CONVERT(INT, @Destino)


			

			set @resultado=''
		END
	ELSE
		BEGIN
			set @resultado='La talla ' + right(@CVE_ART, 4) + ' no se transfirio debido a que no hay existencia suficiente o esta ha cambiado'
		END



END