SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE procedure [dbo].[usp_RepFacturaXMaquilador]
		(
			@clave_maquilero		as varchar(50),
			@factura				as varchar(15)
		)
	as
begin
		set nocount on
		declare @costo_bordado			as float(8)
		declare @nombre_maquilero		as varchar(max)
		declare @titulo_fac_maq			as varchar(max)
		declare @modelo					as varchar(50)
		declare @cantidad				as int


		--set @clave_maquilero	=	'1'
		--set @factura			=	'a18'


		set @nombre_maquilero	=	''
		set @titulo_fac_maq		=	''
		set @modelo				=	''
		set @cantidad			=	0


		--obtención del costo x puntada
		select @costo_bordado = COSTOXPUNTADA from aspel_sae50.dbo.COSTO_BORDADO
		--se obtiene nombre del maquilero
		select @nombre_maquilero = ISNULL(NOMBRE,'') /*AS AD_NAME*/ FROM aspel_sae50.dbo.PROV01 WHERE ltrim(rtrim(CLAVE)) = @clave_maquilero

		set @titulo_fac_maq = 'Factura  ' + @factura + ', Maquilador  ' + @clave_maquilero +  ' ' + @nombre_maquilero

		--temporal de resultados finales
		CREATE TABLE #temp_1 (
		  [TEMP1_INDICE] [int] identity(1,1) NOT NULL,
		  [TEMP1_AD_NAME] [varchar](150) NULL,
		  [TEMP1_AGENTE] [char](10) NOT NULL,
		  [TEMP1_MODELO] [char](10) NULL,
		  [TEMP1_CMT_PEDIDO] [int] NULL,
		  [TEMP1_CANTIDAD] [int] NULL,
		  [TEMP1_CMT_COMO] [varchar](50) NULL,
		  [TEMP1_CMT_AGRUPADOR] [varchar](50) NULL,
		  [TEMP1_PUNTADAS] [int] NULL,
		  [TEMP1_COSTOVENTA] [money] NULL,
		  [TEMP1_PRECIOVENTA] [money] NULL,
		  [TEMP1_COSTOTOTALVENTA] [money] NULL,
		  [TEMP1_PRECIOTOTALVENTA] [money] NULL,		  
		  [TEMP1_CMTINDX] [int] NULL,
		  [TEMP1_PRECIOTOTALVENTA_R] [varchar](50) NULL,
		  [TEMP1_PRECIOVENTA_R] [varchar](50) NULL
		) ON [PRIMARY]



		--consulta la información de la factura vs maquilero
		insert into #temp_1
			(
				[TEMP1_AD_NAME],
				[TEMP1_AGENTE],
				[TEMP1_CMT_PEDIDO],
				[TEMP1_CMT_COMO],
				[TEMP1_CMT_AGRUPADOR],
				[TEMP1_CMTINDX],
				[TEMP1_PUNTADAS],		
				[TEMP1_COSTOVENTA],
				[TEMP1_PRECIOVENTA]		
			)	
				SELECT
				  ISNULL(NOMBRE, '')				AS AD_NAME,
				  ISNULL(AGENTE, '')				AS AGENTE,
				  ISNULL(CMT_PEDIDO, 0)				AS CMT_PEDIDO,
				  ISNULL(CMT_COMO, '')				AS CMT_COMO,
				  ISNULL(CMT_AGRUPADOR, '')			AS CMT_AGRUPADOR,
				  ISNULL(CMT_INDX, '')				AS CMT_INDX,
				  ISNULL(PUNTADAS, 0)				AS PUNTADAS,
				  ISNULL(PUNTADAS, 0) * 0.00085		AS COSTOVENTA,
				  ISNULL(CMT_PRE_PROCESO, 0)		AS PRECIOVENTA
				FROM
					aspel_sae50.dbo.CLIE01
				JOIN
					aspel_sae50.dbo.PED_MSTR
					ON
					LTRIM(RTRIM(CLAVE)) = LTRIM(RTRIM(CLIENTE))
				JOIN
					aspel_sae50.dbo.CMT_DET
					ON
					CMT_PEDIDO = PEDIDO
				JOIN
					aspel_sae50.dbo.IMAGENES
					ON
					CMT_COMO = COD_CATALOGO
				WHERE
					LTRIM(RTRIM(CMT_MAQUILERO)) = LTRIM(RTRIM(@clave_maquilero))
				AND
					LTRIM(RTRIM(CMT_FACT_MAQUILA)) = LTRIM(RTRIM(@factura))



		/*
		update t
		set name='HIGH'
		from table1 t
		inner join (select type,max(age) mage from table1 group by type) t1
		on t.type = t1.type and t.age = t1.mage;
		*/
		
		  
		  
update #temp_1 set
		[TEMP1_MODELO]			= kk.MODELO,
		[TEMP1_CANTIDAD]		= kk.CANTIDAD	  
	FROM #temp_1
	inner join (
			SELECT
			  ISNULL(PEDIDO, '')					AS PEDIDO,
			  ISNULL(SUM(CANTIDAD), 0)				AS CANTIDAD,
			  ISNULL(SUBSTRING(CODIGO, 1, 8), '')	AS MODELO,
			  ISNULL(AGRUPADOR, '')					AS AGRUPADOR
			FROM
				aspel_sae50.dbo.PED_DET
			JOIN
				#temp_1
			  ON
				PEDIDO = CAST(TEMP1_CMT_PEDIDO AS varchar)
			  AND
				AGRUPADOR COLLATE Latin1_General_BIN = TEMP1_CMT_AGRUPADOR
			GROUP BY
				PEDIDO,
				SUBSTRING(CODIGO, 1, 8),
				AGRUPADOR,
				TEMP1_CMT_COMO,
				TEMP1_CMTINDX
		) as kk
	on
		CAST(#temp_1.TEMP1_CMT_PEDIDO AS varchar) = kk.PEDIDO
		AND
		kk.AGRUPADOR COLLATE Latin1_General_BIN = #temp_1.TEMP1_CMT_AGRUPADOR

		update #temp_1 set			
			[TEMP1_COSTOVENTA]		= case when 
											[TEMP1_PUNTADAS] * @costo_bordado < 3.5 then 
												3.5
											else 
												case when [TEMP1_PUNTADAS] is null  then
													0
												else
													[TEMP1_PUNTADAS] * @costo_bordado
												end
											end
											
		update #temp_1 set
			[TEMP1_COSTOTOTALVENTA]			= [TEMP1_COSTOVENTA] * [TEMP1_CANTIDAD],
			[TEMP1_PRECIOTOTALVENTA]		= cast(convert(float(8),[TEMP1_PRECIOVENTA]) * [TEMP1_CANTIDAD] as varchar(50))



		--select @costo_bordado,@nombre_maquilero,@titulo_fac_maq
		select * from #temp_1
		
		drop table #temp_1
end
GO
