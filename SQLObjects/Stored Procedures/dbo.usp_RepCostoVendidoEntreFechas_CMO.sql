SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
create procedure [usp_RepCostoVendidoEntreFechas_CMO]
(
	@fecha_desde	date,
	@fecha_hasta	date
)
as


--set @fecha_desde	= '01-07-2014'
--set @fecha_hasta	= '31-07-2014'


set transaction isolation level read uncommitted
set nocount on
/*
creación de la tabla maestra
*/
---
create table #tabla_maestra
(	[UID]			[int] IDENTITY (1, 1) not null,
	MODELO			[varchar](20),
	DESCRIPCION		[varchar](300),
	IsException		[varchar](50),
	FACTURA			[float] default(0),
	NC				[float]	default(0),
	COSTO			[float]	default(0),
	COMENTARIO		[varchar](300)
)
----
--------------------
/*sirve para almacenar los componentes según la clave del artículo / modelo*/
create table #componentes_PT_DET01
(
	COMPONENTE	[varchar](30),
	CANTIDAD	[float],
	TIPOCOMP	[int],
	COSTOU		[float]	
)
alter table #componentes_PT_DET01 add [UID] int identity(1,1)
create unique clustered index idx on #componentes_PT_DET01([UID])
--------------------

----variable con el total de registros a iterar, también se declara el contador del While
declare	@iTot	int
declare @iCont	int

declare @iTot2  int
declare @iCont2 int
----

--alter table #tabla_maestra add constraint pk_uid primary key([UID])
create unique clustered index idx on #tabla_maestra([UID])

/* se insertan los registros a la tabla maestra, tabla que finalmente se devolverá cuando se llame el SP */
insert into #tabla_maestra (MODELO,DESCRIPCION,IsException,FACTURA,NC)
SELECT
	CVE_ART AS MODELO,
	DESCR.DESCR,
	isnull(DESCR.IsExcepcion,'')	as IsExcepcion,
	isnull(Facturas.FACTURAS,0) as FACTURAS,
	isnull(NC.NC,0) as NC
From
	aspel_sae50..INVE01
left
	join (SELECT RTRIM(LTRIM(PAR_FACTF01.CVE_ART)) as MODELO,sum(CANT) AS FACTURAS FROM aspel_sae50..FACTF01 JOIN aspel_sae50..PAR_FACTF01 ON FACTF01.CVE_DOC = PAR_FACTF01.CVE_DOC JOIN aspel_sae50..INVE01 ON ltrim(rtrim(INVE01.CVE_ART)) = ltrim(rtrim(PAR_FACTF01.CVE_ART)) WHERE PAR_FACTF01.TIPO_PROD = 'P' AND ltrim(rtrim(LIN_PROD)) <> 'MP' AND convert(date,FECHA_DOC) between @fecha_desde AND @fecha_hasta  and FACTF01.STATUS <> 'C' GROUP BY PAR_FACTF01.CVE_ART) as Facturas on INVE01.CVE_ART = Facturas.MODELO
left
	join (SELECT RTRIM(LTRIM(PAR_FACTD01.CVE_ART)) as MODELO,sum(CANT) AS NC       FROM aspel_sae50..FACTD01 JOIN aspel_sae50..PAR_FACTD01 ON FACTD01.CVE_DOC = PAR_FACTD01.CVE_DOC JOIN aspel_sae50..INVE01 ON ltrim(rtrim(INVE01.CVE_ART)) = ltrim(rtrim(PAR_FACTD01.CVE_ART)) WHERE PAR_FACTD01.TIPO_PROD = 'P' AND ltrim(rtrim(LIN_PROD)) <> 'MP' AND convert(date,FECHA_DOC) between @fecha_desde AND @fecha_hasta  and FACTD01.STATUS <> 'C' GROUP BY PAR_FACTD01.CVE_ART) as NC on INVE01.CVE_ART = NC.MODELO
inner
	join (SELECT CVE_PROD as MODELO,DESCR, LOWER(RTrim(LTrim(CAMPLIB1))) as IsExcepcion FROM aspel_sae50..INVE_CLIB01 CL LEFT JOIN aspel_sae50..INVE01 I ON CL.CVE_PROD = I.CVE_ART) as DESCR on INVE01.CVE_ART = DESCR.MODELO
	
WHERE
	TIPO_ELE = 'P' AND LIN_PROD <> 'MP' and /*CVE_ART = 'PJ14MSSH2830' and*/ (FACTURAS > 0 or NC > 0)  GROUP BY CVE_ART,Facturas.FACTURAS,NC.NC,DESCR.DESCR,DESCR.IsExcepcion order by CVE_ART	

set @iTot = @@rowcount
---
set @iCont = 1
---
declare @MODELO			varchar(20)
declare @CVE_ART    varchar(20)
declare @IsException	varchar(20)
declare @Costo			float
declare @CostoQry		float
declare	@Comentario   varchar(300)
declare @fecha_docu		date
---
--------------------
declare @COMPONENTE	varchar(30)
declare @CANTIDAD	float
declare @TIPOCOMP	int
declare @COSTOU		float
--------------------
---
--		se recorre la tabla maestra
---
while @iCont <= @iTot
	begin
		set	@Costo		= 0
		set @Comentario	= ''
		set	@CostoQry	= 0
		set @fecha_docu	= null
		---
		/*se extrae el modelo del registro actual...*/
		select @MODELO = MODELO,@IsException = IsException from #tabla_maestra where [UID] = @iCont
    --NOTA ORIGINAL VB: 'Se deben de contemplar las exepciones antes de ir a consultar los Componentes
		if @IsException = 'zapato' or @IsException = 'playera'
      begin
        /*
            NOTA ORIGINAL SIP VB:
            ' Consulta de Costo Sin ir a PROD
            'Se toma como base la programación original de SIP haciendo ajustes como en de la línea de abjo, donde se omite la condición
            'de que la línea del producto sea MP (Materi Prima)
            'strSQL = "SELECT CLV_ART FROM INVE01 WHERE CLV_ART =  '" & adoComponentesRS!COMPONENTE & "' AND LTRIM(RTRIM(LIN_PROD)) = 'MP'"
        */
        SELECT @CVE_ART = CVE_ART FROM aspel_sae50..INVE01 WHERE CVE_ART =  @MODELO
        if @@ROWCOUNT = 1
			begin
				SELECT TOP 1 @CVE_ART = CVE_ART ,@CostoQry = COSTO From aspel_sae50..MINVE01 where convert(date,FECHA_DOCU) <= @fecha_hasta AND CVE_ART = @MODELO AND CVE_CPTO = 1 ORDER BY FECHA_DOCU DESC,NUM_MOV DESC
				if @@ROWCOUNT = 1
					begin
						set @Costo = @Costo + @CostoQry
					end
				else  
					begin
						SELECT TOP 1 @CVE_ART = CVE_ART,@CostoQry = COSTO,@fecha_docu = FECHA_DOCU From aspel_sae50..MINVE01 where convert(date,FECHA_DOCU) >= @fecha_hasta AND CVE_ART = @MODELO AND CVE_CPTO = 1 ORDER BY FECHA_DOCU ASC
						if @@ROWCOUNT = 1
							begin
								set @Comentario = 'No hay compras ANTERIORES para el producto ' + @MODELO + ' se costea con la compra del ' + cast(@fecha_docu as varchar(20))
								set @Costo = @Costo + @CostoQry
							end
						else
							begin
								set @Comentario = 'No hay compras ni ANTERIORES ni SIGUIENTES para el producto ' + @MODELO
							end 
					end
			end
				/*else
				  begin
					--no hay parte falsa para esta condición
				  end*/
      end
    else
      begin
        --NOTA ORIGINAL SIP EN VB : ****** Ini Costeo SIP *******
        insert into #componentes_PT_DET01
        SELECT COMPONENTE,CANTIDAD,TIPOCOMP,COSTOU FROM aspel_prod30..PT_DET01 where CLAVE = @MODELO AND COMPONENTE NOT LIKE ('BOT%') AND COMPONENTE NOT LIKE ('ESC%')
        set @iTot2 = @@ROWCOUNT
        set @iCont2 = 1
        while @iCont2 <= @iTot2
          begin			
            select
              @COMPONENTE	=	COMPONENTE,
              @CANTIDAD	=	CANTIDAD,
              @TIPOCOMP	=	TIPOCOMP,
              @COSTOU		=	isnull(COSTOU,0)
            from
              #componentes_PT_DET01
            where
              [UID] = @iCont2       
             --select @iCont
             --select * from #componentes_PT_DET01
             --select @COMPONENTE
            if @TIPOCOMP = 49
              begin				
				--PRINT 'ES 49----------------------------------------'
                --NOTA ORIGINAL VB: 'VALIDO QUE SI JUEGUE PARA LA ESTRUCTURA EL COMPONENTE
                --print @COMPONENTE
                SELECT @CVE_ART = CVE_ART FROM aspel_sae50..INVE01 WHERE CVE_ART =  @COMPONENTE
                if @@ROWCOUNT = 1   --NOTA ORIGINAL VB :  ' si es MP
					begin
						SELECT TOP 1 @CVE_ART = CVE_ART,@CostoQry = COSTO From aspel_sae50..MINVE01 where convert(date,FECHA_DOCU) <= @fecha_hasta AND CVE_ART = @COMPONENTE AND CVE_CPTO = 1 ORDER BY FECHA_DOCU DESC,NUM_MOV DESC
						if @@ROWCOUNT = 1
							begin
								--print '001.-Costo antes:  > ' + cast(@Costo as varchar(20))
								set @Costo = @Costo + (@CANTIDAD * @CostoQry)
								--print '002.-Costo después:> ' + cast(@Costo as varchar(20))
							end
						else
							begin								
								--print 'akí segúnda vuelta'
								SELECT TOP 1 @CVE_ART = CVE_ART,@CostoQry = COSTO,@fecha_docu = FECHA_DOCU From aspel_sae50..MINVE01 where convert(date,FECHA_DOCU) >= @fecha_hasta AND CVE_ART = @COMPONENTE AND CVE_CPTO = 1 ORDER BY FECHA_DOCU ASC
								if @@ROWCOUNT = 1
									begin
										--print '003.-Costo antes:  > ' + cast(@Costo as varchar(20))
										set @Comentario = 'No hay compras ANTERIORES para el componente ' + @COMPONENTE + ' se costea con la compra del ' + cast(@fecha_docu as varchar(20))
										set @Costo = @Costo + (@CANTIDAD * @CostoQry)
										--print '003.-Costo después:> ' + cast(@Costo as varchar(20))
									end
								else
									begin
										--print '004.-Costo antes:> ' + cast(@Costo as varchar(20))								
										set @Comentario = 'No hay compras ni ANTERIORES ni SIGUIENTES para el componente ' + @COMPONENTE
										--print '004.-Costo después:> ' + cast(@Costo as varchar(20))
									end
							end
                  end
               else
                  begin
                    -- no hay parte falsa para esta condición
                    PRINT 'LLEGÓ A ESTA ZONA, NO DEBERÍA PASAR'
                  end
                  
              end
            else
              begin								
                set @Costo = @Costo + (@COSTOU * @CANTIDAD)
              end
            
             set @iCont2 = @iCont2 + 1
          end
        
        delete #componentes_PT_DET01
		    DBCC CHECKIDENT('#componentes_PT_DET01', RESEED, 0)
        
      end
		---
		/* poner aquí el código que actualiza el costo en la tabla maestra */
		update #tabla_maestra set COSTO = @Costo,COMENTARIO = @Comentario where [UID] = @iCont
		---
		set @iCont = @iCont + 1
	end




select * from #tabla_maestra

drop table #tabla_maestra
drop table #componentes_PT_DET01

GO
