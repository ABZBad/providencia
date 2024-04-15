ALTER PROCEDURE usp_EntradaAFabricacionOPManual
(
	@idPedido varchar(12),
	@resultado varchar(500) output
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

declare @NUM_MOV int
declare @total_reg_principal NUMERIC(10)
declare @CVE_FOLIO NUMERIC(6,2)
declare @total_reg2 NUMERIC(8)
--declare @idPedido varchar(12)
--set @idPedido = 'BAGAVOXH'


--1. busca la existencia de la órden que se introdujo
SELECT REFERENCIA = LTRIM(RTRIM(REFERENCIA)), COMPONENTE, SUM((ord.CANTIDAD - CANTTERM) * pt.CANTIDAD) AS REQUERIMIENTO	
into #principal
FROM aspel_prod30.dbo.ORD_FAB01 ord JOIN aspel_prod30.dbo.PT_DET01 pt ON ord.PRODUCTO = pt.CLAVE 
WHERE LTRIM(RTRIM(REFERENCIA)) = @idPedido AND TIPOCOMP = 49 AND ord.STATUS = 0 GROUP BY COMPONENTE, LTRIM(RTRIM(REFERENCIA))

set @resultado='ok 1, '

select @total_reg_principal =  count(COMPONENTE) from #principal
--print @total_reg_principal
set @resultado= @resultado + 'ok 1.1, '

IF	(@total_reg_principal)>0
	BEGIN

		--2. Revisa en la tabla MULT01 si existe el producto 
		SELECT m.CVE_ART, m.EXIST 
		into #tblVerificacion
		FROM #principal p left outer join aspel_sae50.dbo.MULT01 m on p.COMPONENTE = LTRIM(RTRIM(m.CVE_ART))
		where m.CVE_ALM = 2 and m.CVE_ART is null

		set @resultado= @resultado + 'ok 2, '

		IF (select count(CVE_ART) from #tblVerificacion)=0
			BEGIN
				declare @CVE_ART varchar(50)
				declare @REQUERIMIENTO int
				declare @EXIST int

				--2.1 Revisa en la tabla MULT01 que la cantidad de productos sea suficiente
				SELECT m.CVE_ART, m.EXIST, p.REQUERIMIENTO
				into #tblExist
				FROM #principal p left outer join aspel_sae50.dbo.MULT01 m on p.COMPONENTE = LTRIM(RTRIM(m.CVE_ART))
				where m.CVE_ALM = 2 and REQUERIMIENTO > m.EXIST

				set @resultado= @resultado + 'ok 2.1, '

				IF (select count(EXIST) from #tblExist)=0
					BEGIN
						BEGIN TRY
							BEGIN TRAN
					
										--3. alctualiza la existencia en la tabla INVE01
										UPDATE aspel_sae50.dbo.INVE01 SET PEND_SURT = ROUND(PEND_SURT - p.REQUERIMIENTO,3), EXIST = ROUND(EXIST - p.REQUERIMIENTO,3) 
										from #principal p inner join aspel_sae50.dbo.INVE01 i on p.COMPONENTE = i.CVE_ART
										set @resultado= @resultado + 'ok 3, '

										--4. alctualiza la existencia de la tabla MULT01
										UPDATE aspel_sae50.dbo.MULT01 SET EXIST = ROUND(EXIST - p.REQUERIMIENTO,3) 
										from #principal p inner join aspel_sae50.dbo.MULT01 m on p.COMPONENTE = m.CVE_ART
										WHERE m.CVE_ALM = 2
										set @resultado= @resultado + 'ok 4, '
	
										--5. consulta en donde se actualiza el valor de ULT_CVE incrementándole su valor a sí mismo más uno 
										update aspel_sae50.dbo.TBLCONTROL01 set ULT_CVE = ULT_CVE + 1 WHERE ID_TABLA = 32		
										set @resultado= @resultado + 'ok 5, '

										--6. Actualiza ULT_CVE más la cantidad de registros que contenga #principal
										UPDATE aspel_sae50.dbo.TBLCONTROL01 SET ULT_CVE = ULT_CVE + (SELECT count(REFERENCIA) from #principal)
										WHERE ID_TABLA = 44 
										set @resultado= @resultado + 'ok 6, '
										--@total_reg_principal

										--7 Inserta en MINVE01 valores derivados de #principal, INVE01 y MULT01
										insert into aspel_sae50.dbo.MINVE01(CVE_ART, ALMACEN, NUM_MOV, CVE_CPTO, FECHA_DOCU, TIPO_DOC, REFER, CLAVE_CLPV, 
											VEND, CANT, CANT_COST, PRECIO, COSTO, AFEC_COI, CVE_OBS, REG_SERIE, UNI_VENTA, E_LTPD, EXISTENCIA, TIPO_PROD, 
											FACTOR_CON, FECHAELAB, CTLPOL, CVE_FOLIO, SIGNO, COSTEADO, DESDE_INVE, MOV_ENLAZADO)
										SELECT p.COMPONENTE, 2, @total_reg_principal, 53, cast(getdate() as date), 'N', p.REFERENCIA, '', '', p.REQUERIMIENTO, 
											0, 0, i.ULT_COSTO, '', 0, 0, i.UNI_EMP, 0, m.EXIST, i.TIPO_ELE, i.FAC_CONV, cast(getdate() as date), 0, 
											(select ULT_CVE from aspel_sae50.dbo.TBLCONTROL01 WHERE ID_TABLA = 32), -1, 'S', 'S', 0
										FROM #principal p
										left outer join aspel_sae50.dbo.INVE01 i on p.COMPONENTE = LTRIM(RTRIM(i.CVE_ART))
										left outer join aspel_sae50.dbo.MULT01 m on p.COMPONENTE = LTRIM(RTRIM(m.CVE_ART))
										set @resultado= @resultado + 'ok 7, '

										--10. Obtiene el listado de productos de la tabla ORD_FAB01 condicionando por referencia (número de órden)
										SELECT * into #principal2 
										From aspel_prod30.dbo.ORD_FAB01 WHERE REFERENCIA in(select REFERENCIA from #principal group by REFERENCIA)
										set @resultado= @resultado + 'ok 10, '

										--12. Obtiene de SEG_F0B01 el valor de NUM_REGS más la cantidad de registros que contenga #principal
										SELECT @total_reg2 = NUM_REGS FROM aspel_prod30.dbo.SEG_F0B01 group by NUM_REGS
										--146917
										set @resultado= @resultado + 'ok 12, '

										--11 y 13. Inserta en SEG_FAB01 el resultado de #principal2 y PT_DET01 
										insert into aspel_prod30.dbo.SEG_FAB01(NUM_REG, ORDEN, PROCESO, FECHMOV, ESTATUS, TIPOMOV, COMPONENTE, ALMACEN, 
											TIPOCMP, REFERENCIA, CANTIDAD, CANTDEV, CSTOUNI, NUMUSU, NUMSERIE, CVETRAB, ACTSAE, NINDUNIT, CSTOUNIEST, RESTO)
										SELECT @total_reg2 + row_number() OVER(ORDER BY p2.CLAVE, pd.PROCESO), 
										p2.CLAVE, pd.PROCESO, cast(getdate() as date), 0, 5, pd.COMPONENTE, 2, 
											case pd.TIPOCOMP when 49 then 1 when 50 then 2 else 0 end, p2.REFERENCIA, p2.CANTIDAD, 0, Round(pd.COSTOU, 3), 
											50, 0, '', 1, 0, Round(pd.COSTOU, 3), ''
										FROM aspel_prod30.dbo.PT_DET01 pd
										inner join #principal2 p2 on pd.CLAVE = p2.PRODUCTO
										set @resultado= @resultado + 'ok 11 y 13, '

										--actualiza el id de SEG_F0B01
										select @total_reg2 = max(NUM_REG) from aspel_prod30.dbo.SEG_FAB01 
										set @resultado= @resultado + 'ok 14, '

										update aspel_prod30.dbo.SEG_F0B01 set NUM_REGS= @total_reg2 
										set @resultado= @resultado + 'ok 15, '

										--14.Actualiza status a 1 en ORD_FAB01 donde la referencia sea igual a el parámetro inicial
										UPDATE aspel_prod30.dbo.ORD_FAB01 SET STATUS = 1 WHERE REFERENCIA in(select REFERENCIA from #principal group by REFERENCIA)
										set @resultado= @resultado + 'ok 16, '

										drop table #principal2
										set @resultado= @resultado + 'ok 17 '
					
										set @resultado=''
							COMMIT TRAN
						END TRY
						BEGIN CATCH
							ROLLBACK TRAN
							SELECT @@ERROR
						END CATCH
					END
				ELSE
					BEGIN
						select top 1 @CVE_ART = CVE_ART, @REQUERIMIENTO = REQUERIMIENTO, @EXIST = EXIST from #tblExist	
						set @resultado= 'No hay existencia suficiente de '+ @CVE_ART + ' se requieren ' + convert(varchar(5), @REQUERIMIENTO) + ' y en el almacen 2 sólo hay ' + convert(varchar(5), @EXIST)
					END
			END
		ELSE
			BEGIN
				select top 1 @CVE_ART = CVE_ART from #tblVerificacion
				set @resultado= 'El componente '+ @CVE_ART +' no existe en el catalogo de artículos'
			END

		drop table #principal

	END
ELSE
	BEGIN
		set @resultado= 'Orden inexistente, sin estructura o ya liberada.'
	END

END
