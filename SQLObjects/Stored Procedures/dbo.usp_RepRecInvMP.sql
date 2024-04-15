SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
create procedure [dbo].[usp_RepRecInvMP]
(
	@fecha date
)
as 

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED



--estrcutrua de tabla "Reporte de Reconstruccion de inventario MP"
create table #inventario_mp (
	[ARTICULO]		[varchar](30),
	[DESCRIPCION]	[varchar](1000),
	[EXISTENCIA]	[decimal](12,2),
	[ENTRADAS]		[decimal](12,2),
	[SALIDAS]		[decimal](12,2),	
	[LINEA]			[varchar](5),
	[COSTO]			[decimal](12,2),
	[OBSERVACION]	[varchar](1000)
)



--create table #tabla_final

--creacion de temporales para entradas y salidas
create table #entradas (
	[ENTRADAS]	[decimal](10,2) DEFAULT (0),
	[CLV_ART]	[varchar](30)
)
create table #salidas (
	[SALIDAS]	[decimal](10,2) DEFAULT (0),
	[CLV_ART]	[varchar](30)
)

--se de terminan la suma de entradas
insert into #entradas (ENTRADAS,CLV_ART) SELECT ISNULL(SUM(CANT),0),LTRIM(RTRIM(CVE_ART)) From aspel_sae50.dbo.MINVE01 WHERE SIGNO = 1 and convert(date,FECHA_DOCU) >= @fecha group by LTRIM(RTRIM(CVE_ART))
--se de terminan la suma de salidas
insert into #salidas (SALIDAS,CLV_ART) SELECT ISNULL(SUM(CANT),0),LTRIM(RTRIM(CVE_ART)) From aspel_sae50.dbo.MINVE01 WHERE SIGNO = -1 and convert(date,FECHA_DOCU) >= @fecha group by LTRIM(RTRIM(CVE_ART))

/*
--temporales para determinar costos a una fecha
create table #costos_x_fecha
(
	[CLV_ART]		[varchar](30),	
	[Q1_Costo]			[decimal](10,2) default (0),	
	[Q2_Costo]			[decimal](10,2) default (0),	
	[Q3_Costo]			[decimal](10,2) default (0),	
	[Q4_Costo]			[decimal](10,2) default (0),
	[Costo_Final]		[decimal](10,2) default (0),
	[Comentario]		[varchar](255)	
)
*/
/*
se obtiene la consulta principal
*/

insert into #inventario_mp (ARTICULO,DESCRIPCION,EXISTENCIA,LINEA,ENTRADAS,SALIDAS)
select t01.* from (
			SELECT
					INVE01_ORI.CVE_ART				AS CLV_ART,
					INVE01_ORI.DESCR,
					INVE01_ORI.EXIST,
					INVE01_ORI.LIN_PROD,
					isnull(#entradas.ENTRADAS,0)	as ENTRADAS,
					isnull(#salidas.SALIDAS,0)		as SALIDAS
			FROM
					aspel_sae50.dbo.INVE01 INVE01_ORI
				left join
					#entradas
					on
					rtrim(ltrim(INVE01_ORI.CVE_ART)) = #entradas.CLV_ART COLLATE Modern_Spanish_CI_AS
				left join
					#salidas
					on
					rtrim(ltrim(INVE01_ORI.CVE_ART)) = #salidas.CLV_ART COLLATE Modern_Spanish_CI_AS
					
			WHERE
					(LIN_PROD = 'MP')
			OR
				(LIN_PROD = 'HABI'
			AND
				CVE_ART NOT LIKE 'BOT%'
			AND
				CVE_ART NOT LIKE 'ESC%'
			AND
				CVE_ART NOT LIKE 'POS%'
			AND
				CVE_ART NOT LIKE 'VL%')	
			/*ORDER BY
				LIN_PROD DESC,
				CLV_ART
			*/	
) as t01
where t01.EXIST + t01.ENTRADAS + t01.SALIDAS > 0
order by
	t01.LIN_PROD DESC,
	t01.CLV_ART

--se insertan primero las claves de los artículos
--insert into #costos_x_fecha (CLV_ART) select ARTICULO from #inventario_mp

/*		
update #costos_x_fecha set 
	Q1_Costo = SIP.dbo.udf_RegresaCostoPorCveArt(#costos_x_fecha.CLV_ART,'<=',@fecha),
	Q2_Costo = SIP.dbo.udf_RegresaCostoPorCveArt(#costos_x_fecha.CLV_ART,'>',@fecha),
	Q3_Costo = SIP.dbo.udf_RegresaCostoPorCveArt2(#costos_x_fecha.CLV_ART,'<=',@fecha),
	Q4_Costo = SIP.dbo.udf_RegresaCostoPorCveArt2(#costos_x_fecha.CLV_ART,'>',@fecha)
	
update #costos_x_fecha set
	Costo_Final = case when Q1_Costo is null then
						case when Q2_Costo is null then
							case when Q3_Costo is null then
								Q4_Costo
							else
								Q3_Costo
							end
						else
							Q2_Costo
						end
					else
						Q1_Costo
					end	

update #costos_x_fecha set
	comentario = case when Q1_Costo is null and Q2_Costo is not null then
							'No hay Compras Efectivas Anteriores para el Producto'
						else
							case when Q1_Costo is null and Q2_Costo is null and Q3_Costo is not null then
								'No hay Compras Efectivas Anteriores ni Posteriores para el Producto'
							else
								case when Q1_Costo is null and Q2_Costo is null and Q3_Costo is null and Q4_Costo is not null then
									'No hay Compras Efectivas Anteriores ni Posteriores; ni Movimientos de Compra por Ajuste Anteriores para el Producto'
								else
									case when Q1_Costo is null and Q2_Costo is null and Q3_Costo is null and Q4_Costo is null then
										'No hay Compras Efectivas Anteriores ni Posteriores; ni Movimientos de Compra por Ajuste Anteriores ni Posteriores para el Producto'
									end
								end
							end
						end
	

update #inventario_mp set
	COSTO = #costos_x_fecha.Q1_Costo
from
	#costos_x_fecha
inner join
	#inventario_mp
	on
	#inventario_mp.ARTICULO = #costos_x_fecha.CLV_ART
	*/
--selección de la tabla final
SELECT * FROM #inventario_mp
--select * from #costos_x_fecha
--select CLV_ART,sum(Costo) from #costos_x_fecha	GROUP BY CLV_ART



--se borran las temporales utilizadas	
drop table #entradas
drop table #salidas
drop table #inventario_mp
--drop table #costos_x_fecha



--select * from #costos_x_fecha where CLV_ART ='08MOKXAM170'


GO
