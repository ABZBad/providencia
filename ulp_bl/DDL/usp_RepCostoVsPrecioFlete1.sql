/*
18-Mar-2015 : Se modifican los parámetros de entrada de datetime a date
26-Mar-2015 : Se cambia el universo por solo los pedidos facturados
*/
--usp_RepClientesNuevos
ALTER procedure usp_RepCostoVsPrecioFlete
	(
		@fecha_inicial	as	date,
		@fecha_final	as	date,
		@proceso		as	varchar(1)	--'F'=Flete, 'C'=Costura
	)
as
begin
		SELECT
			TBL.CVE_DOC AS Factura,
			TBL.Pedido,
			TBL.[Clv. Cliente],
			TBL.Nombre,
			SUM(TBL.Prendas) AS Prendas,
			--REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(TBL.Precio,'.','x'),'1','R'),'2','E'),'3','P'),'4','U'),'5','B'),'6','L'),'7','I'),'8','C'),'9','A'),'0','Z') AS Precio,
			--REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(SUM(TBL.[P. Total]),'.','x'),'1','R'),'2','E'),'3','P'),'4','U'),'5','B'),'6','L'),'7','I'),'8','C'),'9','A'),'0','Z') AS 'P. Total',
			TBL.Precio AS Precio,
			SUM(TBL.[P. Total]) AS 'P. Total',
			TBL.Costo,
			SUM(TBL.[C. Total]) AS 'C. Total',
			SUM(TBL.[P. Total]) - SUM(TBL.[C. Total]) AS Diferencia
		FROM
			(SELECT
					FACTF01.CVE_DOC, 
					C.CMT_PEDIDO AS Pedido,
					C.CMT_CLIENTE AS 'Clv. Cliente',
					CL.NOMBRE AS 'Nombre',
					(SELECT ISNULL(SUM(PD.CANTIDAD),0) FROM aspel_sae50.dbo.PED_DET PD WHERE PD.PEDIDO = C.CMT_PEDIDO AND PD.AGRUPADOR = C.CMT_AGRUPADOR) AS 'Prendas',
					C.CMT_PRE_PROCESO AS Precio, 
					(SELECT ISNULL(SUM(PD.CANTIDAD),0) FROM aspel_sae50.dbo.PED_DET PD WHERE PD.PEDIDO = C.CMT_PEDIDO AND PD.AGRUPADOR = C.CMT_AGRUPADOR) * ISNULL(C.CMT_PRE_PROCESO,0) AS 'P. Total',
					C.CMT_COS_PROCESO AS Costo,
					(SELECT ISNULL(SUM(PD.CANTIDAD),0) FROM aspel_sae50.dbo.PED_DET PD WHERE PD.PEDIDO = C.CMT_PEDIDO AND PD.AGRUPADOR = C.CMT_AGRUPADOR) * ISNULL(C.CMT_COS_PROCESO,0) AS 'C. Total'
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
			INNER JOIN
				aspel_sae50..FACTF01
			ON
				'P' + CAST(P.PEDIDO AS VARCHAR(20)) = FACTF01.DOC_ANT
			Where
				C.CMT_COS_PROCESO Is Not Null AND P.TIPO = 'OV'
				AND
				P.ESTATUS <> 'C'  AND C.CMT_PROCESO = @proceso
				AND
				FACTF01.[STATUS] <> 'C'
				AND		
				convert(datetime2,convert(varchar(255),FACTF01.FECHA_DOC,102),102) BETWEEN @fecha_inicial AND @fecha_final	
				--convert(datetime2,convert(varchar(255),P.FECHA,102),102) BETWEEN @fecha_inicial AND @fecha_final
			) AS TBL
			GROUP BY
				TBL.CVE_DOC,
				TBL.Pedido,
				TBL.[Clv. Cliente],
				TBL.Nombre,
				TBL.Precio,
				TBL.Costo,
				TBL.[C. Total]
end