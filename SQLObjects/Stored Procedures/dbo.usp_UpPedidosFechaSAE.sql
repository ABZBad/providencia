SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Victor L>
-- Create date: <08/05/2014>
-- Description:	Develueve los datos para el reporte: "Exporta UpPedidos a Excel con base a fecha SAE"
-- =============================================
CREATE PROCEDURE [dbo].[usp_UpPedidosFechaSAE] 
	(
		@fecha_inicial		as date	= '01-01-1999',
		@fecha_final		as date = '01-01-1999',
		@sinFechaSurtido	as bit,
		@sinFechaEntregado	as bit,
		@vendedor			as nvarchar(255) = ''
	)
AS
BEGIN

SET NOCOUNT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED


declare @sqlBase				as nvarchar(max)
declare @sqlSinFechaSurtido		as nvarchar(max)
declare @sqlSinFechaEntregado	as nvarchar(max)
declare @sqlVendedor			as nvarchar(max)

set @sqlSinFechaSurtido		= ''
set @sqlSinFechaEntregado	= ''
set @sqlVendedor			= ''

create table #tmp(
ID	int null, 
PEDIDO	float null, 
COD_CLIENTE	char(10) null, 
F_VENCIMIENTO	smalldatetime null, 
F_VENCIMIENTO2	smalldatetime null, 
F_ESTANDAR	smalldatetime null, 
PROCESOS	varchar(max) null, 
COMENTARIOS	varchar(max) null, 
F_CAPT	smalldatetime null, 
F_IMPRESION	smalldatetime null, 
F_GESTION	smalldatetime null, 
F_CAPT_ASPEL	smalldatetime null, 
F_CREDITO	smalldatetime null, 
F_ASIG_RUTA	smalldatetime null, 
F_LIBERADO	smalldatetime null, 
F_SURTIDO	smalldatetime null, 
F_BORDADO	smalldatetime null, 
F_COSTURA	smalldatetime null, 
F_CORTE	smalldatetime null, 
F_ESTAMPADO	smalldatetime null, 
F_INICIALES	smalldatetime null, 
F_EMPAQUE	smalldatetime null, 
F_EMBARQUE	smalldatetime null, 
FECHAPEDIDO	smalldatetime null, 
FECHARUTA	smalldatetime null, 
GUIA	char(15) null, 
CAJAS	int null, 
CHOFER	varchar(max) null, 
DEPARTAMENTO	varchar(max) null, 
DESTINO	varchar(max) null, 
OBSERVACIONES	text null, 
ESTATUS	varchar(max) null, 
TRANSPORTE	varchar(max) null, 
COM_SURTIDO	varchar(max) null, 
COM_BORDADO	varchar(max) null, 
COM_COSTURA	varchar(max) null, 
COM_CORTE	varchar(max) null, 
COM_ESTAMPADO	varchar(max) null, 
COM_INICIALES	varchar(max) null, 
COM_EMPAQUE	varchar(max) null, 
COM_CREDITO	varchar(max) null, 
ESTATUS_UPPEDIDOS	varchar(max) null, 
F_ENTREGADO	smalldatetime null, 
COM_ENTREGA	varchar(max) null, 
NOMBRE	varchar(max) null, 
TOT_PEDIDOS	int null, 
VEND	varchar(max) null
)


if (@sinFechaSurtido = 1)
	begin
		set @sqlSinFechaSurtido = ' AND UPPEDIDOS.F_SURTIDO IS NULL'
	end
if (@sinFechaEntregado = 1)
	begin
		set @sqlSinFechaEntregado = ' AND UPPEDIDOS.F_ENTREGADO IS NULL'
	end
if (@vendedor <> '')	
	begin
		set @sqlVendedor = ' AND LTRIM(RTRIM(CVE_VEND) = ''' + @vendedor + ''''
	end


set @sqlBase =
	'SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED INSERT INTO #TMP SELECT 
		UPPEDIDOS.*,
		NOMBRE,
		TOT_PEDIDOS = (select ISNULL(sum(CANTIDAD),0) AS suma from aspel_sae50.dbo.PED_DET WHERE PEDIDO = UPPEDIDOS.PEDIDO),
		CVE_VEND AS VEND
FROM
		aspel_sae50.dbo.UPPEDIDOS
	JOIN
		aspel_sae50.dbo.CLIE01
	ON
		ltrim(rtrim(UPPEDIDOS.COD_CLIENTE)) = ltrim(rtrim(CLIE01.CLAVE))
	INNER JOIN
		aspel_sae50.dbo.PED_MSTR P
	ON
		UPPEDIDOS.PEDIDO = P.PEDIDO
	WHERE
		convert(datetime2,convert(varchar(255),F_CAPT_ASPEL,102),102) BETWEEN ''' + convert(varchar(255),@fecha_inicial) + ''' and ''' + convert(varchar(255),@fecha_final) + '''' +
		' AND
		P.ESTATUS <> ''C'' AND P.TIPO = ''OV''  AND (
													SELECT TOP 1
															CVE_DOC_E
													FROM
															aspel_sae50.dbo.DOCTOSIGF01
													WHERE
															CVE_DOC IN (
																			SELECT TOP 1
																					CVE_DOC_E
																			FROM
																					aspel_sae50.dbo.DOCTOSIGF01
																			WHERE
																					CVE_DOC = ''P'' + CONVERT(VARCHAR,UPPEDIDOS.PEDIDO)
																			AND
																					TIP_DOC_E = ''F''
																			ORDER BY
																					CVE_DOC_E
																			DESC
																			)
													AND
														TIP_DOC_E = ''D''
													ORDER BY
														CVE_DOC_E
													DESC
													) IS NULL'

	execute(@sqlBase + @sqlSinFechaSurtido + @sqlSinFechaEntregado + @sqlVendedor)
	SELECT * FROM #tmp
	
END
GO
