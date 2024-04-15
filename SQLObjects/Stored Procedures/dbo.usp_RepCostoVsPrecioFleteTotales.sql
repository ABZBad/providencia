SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
--usp_RepClientesNuevos
CREATE procedure [dbo].[usp_RepCostoVsPrecioFleteTotales]
	(
		@fecha_inicial	as datetime,
		@fecha_final	as datetime
	)
as
begin
		
		SELECT
			/*
			REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(SUM(TBL_FINAL.Precio),'.','x'),'1','R'),'2','E'),'3','P'),'4','U'),'5','B'),'6','L'),'7','I'),'8','C'),'9','A'),'0','Z'),
			REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(SUM(TBL_FINAL.Total),'.','x'),'1','R'),'2','E'),'3','P'),'4','U'),'5','B'),'6','L'),'7','I'),'8','C'),'9','A'),'0','Z')
			*/
			SUM(TBL_FINAL.Precio),
			SUM(TBL_FINAL.Total)
		FROM   
		   (SELECT
					TBL.Precio AS Precio,
					SUM(TBL.[P. Total]) AS Total
			FROM (
					SELECT
							C.CMT_PEDIDO AS Pedido,
							C.CMT_CLIENTE AS 'Clv. Cliente',
							CL.NOMBRE AS 'Nombre',
							(
								SELECT
										ISNULL(SUM(PD.CANTIDAD),0)
									FROM
										aspel_sae50.dbo.PED_DET PD
								WHERE
									PD.PEDIDO = C.CMT_PEDIDO
									AND
									PD.AGRUPADOR = C.CMT_AGRUPADOR
							) AS 'Prendas',
							C.CMT_PRE_PROCESO AS Precio,
							(
								SELECT
									ISNULL(SUM(PD.CANTIDAD),0)
								FROM
									aspel_sae50.dbo.PED_DET PD
								WHERE
									PD.PEDIDO = C.CMT_PEDIDO
									AND
									PD.AGRUPADOR = C.CMT_AGRUPADOR
							 ) * ISNULL(C.CMT_PRE_PROCESO,0) AS 'P. Total',
							C.CMT_COS_PROCESO AS Costo,
							(
								SELECT
									ISNULL(SUM(PD.CANTIDAD),0)
								FROM
									aspel_sae50.dbo.PED_DET PD
								WHERE
									PD.PEDIDO = C.CMT_PEDIDO
									AND
									PD.AGRUPADOR = C.CMT_AGRUPADOR
							) * ISNULL(C.CMT_COS_PROCESO,0) AS 'C. Total'
					FROM
						aspel_sae50.dbo.CMT_DET C
					INNER JOIN
						aspel_sae50.dbo.PED_MSTR P
						ON
						C.CMT_PEDIDO = P.PEDIDO
					INNER JOIN
						aspel_sae50.dbo.CLIE01 CL
						ON
						LTRIM(RTRIM(C.CMT_CLIENTE)) = LTRIM(RTRIM(CL.CLAVE))
					Where
						C.CMT_COS_PROCESO Is Not Null
						AND
						P.TIPO = 'OV'
						AND
						P.ESTATUS <> 'C'
						AND
						C.CMT_PROCESO = 'F'
						AND
						--P.FECHA BETWEEN '20130801 00:00:00' AND '20130831 23:59:59'
						convert(datetime2,convert(varchar(255),P.FECHA,102),102) BETWEEN @fecha_inicial and @fecha_final
					) AS TBL
				GROUP BY
					TBL.Pedido,
					TBL.[Clv. Cliente],
					TBL.Nombre,
					TBL.Precio,
					TBL.Costo,
					TBL.[C. Total]
			) as TBL_FINAL
end
GO
