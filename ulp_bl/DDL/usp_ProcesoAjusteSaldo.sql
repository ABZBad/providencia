create procedure usp_ProcesoAjusteSaldo (
	@CVE_CLIE		as varchar(30),
	@REFER			as varchar(30),
	@NO_FACTURA		as varchar(30),
	@MONTO_AJUSTE	as numeric(18,2)
)
as

begin

--procedimiento para ajuste de saldos en CxC

begin transaction

			declare @STRCVEVEND		as varchar(20)
				
			--aplicación automática de ajuste

			--generar nuevo id de movimiento

			select @CVE_CLIE = CLAVE,@STRCVEVEND = CVE_VEND from aspel_sae50..CLIE01 where RTRIM(LTRIM(CLAVE)) = @CVE_CLIE

			declare @CVE_BITA	as int

			select @CVE_BITA = ULT_CVE + 1 from aspel_sae50..TBLCONTROL01 where ID_TABLA = 62

			update aspel_sae50..TBLCONTROL01 SET ULT_CVE = @CVE_BITA where ID_TABLA = 62

			INSERT INTO aspel_sae50..BITA01
						(
						   [CVE_BITA]
						   ,[CVE_CLIE]
						   ,[CVE_CAMPANIA]
						   ,[CVE_ACTIVIDAD]
						   ,[FECHAHORA]
						   ,[CVE_USUARIO]
						   ,[OBSERVACIONES]
						   ,[STATUS]
						   ,[NOM_USUARIO]
					   )
				 VALUES
						(
						   @CVE_BITA
						   ,@CVE_CLIE
						   ,'_SAE_'
						   ,'12'
						   ,convert(date,getdate())
						   ,0
						   ,'No. [ ' + @REFER + ' ] $ ' + cast(@MONTO_AJUSTE as varchar(18))
						   ,'F'
						   ,'SYS'
					   )
			           
			           
			INSERT INTO aspel_sae50..CUEN_M01
					   ([CVE_CLIE]
					   ,[REFER]
					   ,[NUM_CPTO]
					   ,[NUM_CARGO]
					   ,[CVE_OBS]
					   ,[NO_FACTURA]
					   ,[DOCTO]
					   ,[IMPORTE]
					   ,[FECHA_APLI]
					   ,[FECHA_VENC]
					   ,[AFEC_COI]
					   ,[STRCVEVEND]
					   ,[NUM_MONED]
					   ,[TCAMBIO]
					   ,[IMPMON_EXT]
					   ,[FECHAELAB]
					   ,[CTLPOL]
					   ,[CVE_FOLIO]
					   ,[TIPO_MOV]
					   ,[CVE_BITA]
					   ,[SIGNO]
					   ,[CVE_AUT]
					   ,[USUARIO]
					   ,[ENTREGADA]
					   ,[FECHA_ENTREGA]
					   ,[STATUS]
					   ,[REF_SIST])
				 VALUES
					   (
					   @CVE_CLIE
					   ,@REFER
					   ,45
					   ,1
					   ,0
					   ,@NO_FACTURA
					   ,@REFER
					   ,@MONTO_AJUSTE
					   ,convert(date,getdate())
					   ,convert(date,getdate())
					   ,null
					   ,@STRCVEVEND
					   ,1
					   ,1
					   ,@MONTO_AJUSTE
					   ,convert(date,getdate())
					   ,null
					   ,null
					   ,'A'
					   ,@CVE_BITA
					   ,-1
					   ,null
					   ,0
					   ,'N'
					   ,null
					   ,null
					   ,null
					   )


			update aspel_sae50..CLIE01 set SALDO = SALDO - @MONTO_AJUSTE  where CLAVE = @CVE_CLIE


commit transaction

end