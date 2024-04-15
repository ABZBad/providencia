
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/*
08-Abr-2015 : se agrega 'xx' cuando la funcíon que regresa los procesos devuelve nulo
09-Abr-2015 : se modifica la lógica para insertar la descripción en PED_TEMP
*/
CREATE PROCEDURE [dbo].[usp_Inserta_PED_TEMP_de_PED_DET]
(
	@idPedido int,
	@tipo int --1. Pedidos 2. Órden de Trabajo
)
AS
BEGIN
	set ansi_warnings off;
	SET NOCOUNT ON;

	--select top 1 * from  aspel_sae50.dbo.PED_TEMP
	--select top 1 * from  aspel_sae50.dbo.PED_DET 
	--SELECT PEDIDO, AGRUPADOR, CODIGO, CANTIDAD, SUBTOTAL, PRECIO_PROD FROM aspel_sae50.dbo.PED_DET WHERE PEDIDO= 34017

		--recalcula valores de PED_DET
	SELECT SUM(CMT_PRE_PROCESO) AS SUMA,CMT_PEDIDO AS PEDIDO,CMT_AGRUPADOR AS AGRUPADOR 
	into #tmpCmt
	From aspel_sae50.dbo.CMT_DET Where CMT_PEDIDO = @idPedido GROUP BY CMT_PEDIDO,CMT_AGRUPADOR


	update aspel_sae50.dbo.PED_DET set PREC_PROCESO = isnull(t.SUMA, 0), 
		SUBTOTAL = PRECIO_PROD + isnull(t.SUMA, 0), 
		PROCESOS = isnull(dbo.udf_Regresa_procesos_por_pedido_y_agrupador(pd.PEDIDO, pd.AGRUPADOR),'xx')
	FROM aspel_sae50.dbo.PED_DET pd
	left outer join #tmpCmt t on pd.PEDIDO = t.PEDIDO and pd.AGRUPADOR = t.AGRUPADOR
	WHERE pd.PEDIDO = @idPedido

	---trabaja con PED_TEMP
	delete aspel_sae50.dbo.PED_TEMP where PEDIDO = @idPedido
	
	if (@tipo=1)
	begin
		insert into aspel_sae50.dbo.PED_TEMP(PEDIDO, MODELO, DESCRIPCION, AGRUPADOR, IMPORTE, PRECIO, PRENDAS, TALLAS)
		SELECT --DISTINCT
			PEDIDO,
			substring(CODIGO,1,8) AS MODELO,
			DESCRIPCION = (SELECT top 1 DESCRIPCION FROM aspel_sae50.dbo.PED_DET WHERE PEDIDO = @idPedido and AGRUPADOR = ped_d.AGRUPADOR),
			AGRUPADOR,
			SUBTOTAL,
			PRECIO_PROD, SUM(CANTIDAD), ''
		FROM aspel_sae50.dbo.PED_DET ped_d
		WHERE PEDIDO = @idPedido
		GROUP BY PEDIDO,
			substring(CODIGO,1,8), /*DESCRIPCION, */
			AGRUPADOR, SUBTOTAL, PRECIO_PROD			
	end
	else
	begin
		insert into aspel_sae50.dbo.PED_TEMP(PEDIDO, MODELO, DESCRIPCION, AGRUPADOR, PRENDAS, TALLAS)	
		SELECT 
			PEDIDO,substring(CODIGO,1,8) AS MODELO,
			DESCRIPCION = (SELECT top 1 DESCRIPCION FROM aspel_sae50.dbo.PED_DET WHERE PEDIDO = @idPedido and AGRUPADOR=ped_d.AGRUPADOR),
			AGRUPADOR,
			SUM(CANTIDAD), ''
		FROM aspel_sae50.dbo.PED_DET ped_d
		WHERE PEDIDO = @idPedido --AND PROCESOS <> ' '
		GROUP BY PEDIDO,
			substring(CODIGO,1,8), /*DESCRIPCION, */
			AGRUPADOR

	end
	-------------------actualiza tallas
		declare @AGRUPADOR_com  char(10)
		declare @Tallas_a_asignar varchar(1000)		
		set @Tallas_a_asignar = ''
		declare @PEDIDO int, @AGRUPADOR char(10), @CODIGO char(12), @CANTIDAD int
		declare cursor_tallas CURSOR FOR 
		 SELECT PEDIDO, AGRUPADOR, CODIGO, CANTIDAD FROM aspel_sae50.dbo.PED_DET 
			WHERE PEDIDO= @idPedido order by AGRUPADOR 

		OPEN cursor_tallas
		FETCH NEXT FROM cursor_tallas INTO @PEDIDO, @AGRUPADOR, @CODIGO, @CANTIDAD
		set @AGRUPADOR_com = @AGRUPADOR
		WHILE @@FETCH_STATUS = 0
		BEGIN			
			if @AGRUPADOR_com <> @AGRUPADOR
			begin
				--SELECT @Tallas_a_asignar, @AGRUPADOR_com
				update aspel_sae50.dbo.PED_TEMP set TALLAS = @Tallas_a_asignar where PEDIDO = @idPedido and AGRUPADOR = @AGRUPADOR_com
				set @Tallas_a_asignar = ''
				set @AGRUPADOR_com = @AGRUPADOR
			end
			set @Tallas_a_asignar = @Tallas_a_asignar + RIGHT(RTRIM(LTrim(@CODIGO)), 4) + '/' + CONVERT(varchar(12), sum(@CANTIDAD)) + ' '
			--print @Tallas_a_asignar 
			--print @AGRUPADOR_com
			FETCH NEXT FROM cursor_tallas INTO @PEDIDO, @AGRUPADOR, @CODIGO, @CANTIDAD
		END
		update aspel_sae50.dbo.PED_TEMP set TALLAS = @Tallas_a_asignar where PEDIDO = @idPedido and AGRUPADOR = @AGRUPADOR_com
		CLOSE cursor_tallas
		DEALLOCATE cursor_tallas

END


GO
