-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- 27-04-2015: Se modifica SP para que considere solo TIPO = 'OV'
-- =============================================
ALTER procedure usp_RepDesmpeñoPorAreasResumen
	(
		@fecha_inicial		as	datetime,
		@fecha_final		as	datetime	
	)
AS
BEGIN
	set nocount on
	
	declare @total_pedidos		as		decimal(15,11)
	declare @tot_s				as		decimal(15,11)
	
	
	set @total_pedidos = 0
	
	create table #DesempeñoPorAreas (
		[Id]					[int]	identity(1,1)			NOT NULL,
		[Area]					[varchar](50)					NOT NULL,
		[PedidosNoCumplidos]	[int]							NOT NULL default 0,
		[%NoCumplidos]			[decimal](15,11)				NOT NULL default 0,
		[PedidosCumplidos]		[int]							NOT NULL default 0,
		[%Cumplidos]			[decimal](15,11)				NOT NULL default 0
		
	)
	
	
	
	
	SELECT 
			@total_pedidos = case when count(*) = 0 then 1 else count(*) end
		FROM 
				aspel_sae50.dbo.UPPEDIDOS as U
			INNER JOIN
				aspel_sae50.dbo.PED_MSTR P ON U.PEDIDO = P.PEDIDO AND P.TIPO = 'OV'
			INNER JOIN
				aspel_sae50.dbo.CLIE01 CL ON LTRIM(RTRIM(P.CLIENTE)) =LTRIM(RTRIM(CL.CLAVE))
			LEFT JOIN
				aspel_sae50.dbo.ESTDPEDI ON P.PEDIDO = ESTDPEDI.PEDIDO
		WHERE 
			convert(datetime2,convert(varchar(255),F_VENCIMIENTO,103),103)  BETWEEN @fecha_inicial AND @fecha_final
	
	
	
	insert into #DesempeñoPorAreas (
									Area,
									PedidosNoCumplidos,
									[%NoCumplidos]									
									)
							SELECT 
									'Ventas',
									COUNT(*),
									count(*) / @total_pedidos
							From
									aspel_sae50.dbo.DESEMBYAREA WHERE PEDIDO IN (SELECT U.PEDIDO FROM aspel_sae50.dbo.UPPEDIDOS U WHERE  F_VENCIMIENTO  BETWEEN @fecha_inicial AND @fecha_final) AND DEPTO = 'Ventas' AND CUMPLIO = 'N'
									


set @tot_s = isnull((SELECT COUNT(*) as nVentas From aspel_sae50.dbo.DESEMBYAREA WHERE PEDIDO IN (SELECT U.PEDIDO FROM aspel_sae50.dbo.UPPEDIDOS U WHERE  F_VENCIMIENTO  BETWEEN @fecha_inicial AND @fecha_final) AND DEPTO = 'Ventas' AND CUMPLIO = 'S'),0)

update #DesempeñoPorAreas  set [PedidosCumplidos] = @tot_s, [%Cumplidos] = @tot_s / @total_pedidos where #DesempeñoPorAreas.Id = @@identity
	
insert into #DesempeñoPorAreas (
									Area,
									PedidosNoCumplidos,
									[%NoCumplidos]									
									)
							SELECT 
									'Almacen',
									COUNT(*),
									count(*) / @total_pedidos
							From
									aspel_sae50.dbo.DESEMBYAREA WHERE PEDIDO IN (SELECT U.PEDIDO FROM aspel_sae50.dbo.UPPEDIDOS U WHERE  F_VENCIMIENTO  BETWEEN @fecha_inicial AND @fecha_final) AND DEPTO = 'Almacen' AND CUMPLIO = 'N'
									


set @tot_s = isnull((SELECT COUNT(*) as nVentas From aspel_sae50.dbo.DESEMBYAREA WHERE PEDIDO IN (SELECT U.PEDIDO FROM aspel_sae50.dbo.UPPEDIDOS U WHERE  F_VENCIMIENTO  BETWEEN @fecha_inicial AND @fecha_final) AND DEPTO = 'Almacen' AND CUMPLIO = 'S'),0)

update #DesempeñoPorAreas  set [PedidosCumplidos] = @tot_s, [%Cumplidos] = @tot_s / @total_pedidos where #DesempeñoPorAreas.Id = @@identity


insert into #DesempeñoPorAreas (
									Area,
									PedidosNoCumplidos,
									[%NoCumplidos]									
									)
							SELECT 
									'Compras',
									COUNT(*),
									count(*) / @total_pedidos
							From
									aspel_sae50.dbo.DESEMBYAREA WHERE PEDIDO IN (SELECT U.PEDIDO FROM aspel_sae50.dbo.UPPEDIDOS U WHERE  F_VENCIMIENTO  BETWEEN @fecha_inicial AND @fecha_final) AND DEPTO = 'Compras' AND CUMPLIO = 'N'
									


set @tot_s = isnull((SELECT COUNT(*) as nVentas From aspel_sae50.dbo.DESEMBYAREA WHERE PEDIDO IN (SELECT U.PEDIDO FROM aspel_sae50.dbo.UPPEDIDOS U WHERE  F_VENCIMIENTO  BETWEEN @fecha_inicial AND @fecha_final) AND DEPTO = 'Compras' AND CUMPLIO = 'S'),0)

update #DesempeñoPorAreas  set [PedidosCumplidos] = @tot_s, [%Cumplidos] = @tot_s / @total_pedidos where #DesempeñoPorAreas.Id = @@identity



insert into #DesempeñoPorAreas (
									Area,
									PedidosNoCumplidos,
									[%NoCumplidos]									
									)
							SELECT 
									'Sistemas',
									COUNT(*),
									count(*) / @total_pedidos
							From
									aspel_sae50.dbo.DESEMBYAREA WHERE PEDIDO IN (SELECT U.PEDIDO FROM aspel_sae50.dbo.UPPEDIDOS U WHERE  F_VENCIMIENTO  BETWEEN @fecha_inicial AND @fecha_final) AND DEPTO = 'Sistemas' AND CUMPLIO = 'N'
									


set @tot_s = isnull((SELECT COUNT(*) as nVentas From aspel_sae50.dbo.DESEMBYAREA WHERE PEDIDO IN (SELECT U.PEDIDO FROM aspel_sae50.dbo.UPPEDIDOS U WHERE  F_VENCIMIENTO  BETWEEN @fecha_inicial AND @fecha_final) AND DEPTO = 'Sistemas' AND CUMPLIO = 'S'),0)

update #DesempeñoPorAreas  set [PedidosCumplidos] = @tot_s, [%Cumplidos] = @tot_s / @total_pedidos where #DesempeñoPorAreas.Id = @@identity


insert into #DesempeñoPorAreas (
									Area,
									PedidosNoCumplidos,
									[%NoCumplidos]									
									)
							SELECT 
									'Operaciones',
									COUNT(*),
									count(*) / @total_pedidos
							From
									aspel_sae50.dbo.DESEMBYAREA WHERE PEDIDO IN (SELECT U.PEDIDO FROM aspel_sae50.dbo.UPPEDIDOS U WHERE  F_VENCIMIENTO  BETWEEN @fecha_inicial AND @fecha_final) AND DEPTO = 'Operaciones' AND CUMPLIO = 'N'
									


set @tot_s = isnull((SELECT COUNT(*) as nVentas From aspel_sae50.dbo.DESEMBYAREA WHERE PEDIDO IN (SELECT U.PEDIDO FROM aspel_sae50.dbo.UPPEDIDOS U WHERE  F_VENCIMIENTO  BETWEEN @fecha_inicial AND @fecha_final) AND DEPTO = 'Operaciones' AND CUMPLIO = 'S'),0)

update #DesempeñoPorAreas  set [PedidosCumplidos] = @tot_s, [%Cumplidos] = @tot_s / @total_pedidos where #DesempeñoPorAreas.Id = @@identity



insert into #DesempeñoPorAreas (
									Area,
									PedidosNoCumplidos,
									[%NoCumplidos]									
									)
							SELECT 
									'Credito',
									COUNT(*),
									count(*) / @total_pedidos
							From
									aspel_sae50.dbo.DESEMBYAREA WHERE PEDIDO IN (SELECT U.PEDIDO FROM aspel_sae50.dbo.UPPEDIDOS U WHERE  F_VENCIMIENTO  BETWEEN @fecha_inicial AND @fecha_final) AND DEPTO = 'Credito' AND CUMPLIO = 'N'
									


set @tot_s = isnull((SELECT COUNT(*) as nVentas From aspel_sae50.dbo.DESEMBYAREA WHERE PEDIDO IN (SELECT U.PEDIDO FROM aspel_sae50.dbo.UPPEDIDOS U WHERE  F_VENCIMIENTO  BETWEEN @fecha_inicial AND @fecha_final) AND DEPTO = 'Credito' AND CUMPLIO = 'S'),0)

update #DesempeñoPorAreas  set [PedidosCumplidos] = @tot_s, [%Cumplidos] = @tot_s / @total_pedidos where #DesempeñoPorAreas.Id = @@identity

insert into #DesempeñoPorAreas (
									Area,
									PedidosNoCumplidos,
									[%NoCumplidos]									
									)
							SELECT 
									'Cliente',
									COUNT(*),
									count(*) / @total_pedidos
							From
									aspel_sae50.dbo.DESEMBYAREA WHERE PEDIDO IN (SELECT U.PEDIDO FROM aspel_sae50.dbo.UPPEDIDOS U WHERE  F_VENCIMIENTO  BETWEEN @fecha_inicial AND @fecha_final) AND DEPTO = 'Cliente' AND CUMPLIO = 'N'
									


set @tot_s = isnull((SELECT COUNT(*) as nVentas From aspel_sae50.dbo.DESEMBYAREA WHERE PEDIDO IN (SELECT U.PEDIDO FROM aspel_sae50.dbo.UPPEDIDOS U WHERE  F_VENCIMIENTO  BETWEEN @fecha_inicial AND @fecha_final) AND DEPTO = 'Cliente' AND CUMPLIO = 'S'),0)

update #DesempeñoPorAreas  set [PedidosCumplidos] = @tot_s, [%Cumplidos] = @tot_s / @total_pedidos where #DesempeñoPorAreas.Id = @@identity


	select * from #DesempeñoPorAreas
	drop table #DesempeñoPorAreas
	
	
END