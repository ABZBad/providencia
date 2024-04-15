SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_RepDesempeñoTotalPedidosSegunFecha]
	(
		@fecha_inicial		as datetime,
		@fecha_final		as datetime,
		@tipo_fecha			as int --0=FECHAS CUMPLIDAS,1=FECHAS ADELANTADAS,2=FECHAS NO CUMPLIDAS,3=FECHAS NO ENTREGADAS
	)
AS
BEGIN
	
	SET NOCOUNT ON

	declare @sqlBase		as varchar(max)
	declare @sqlTipoFecha	as varchar(max)
	
	set @sqlBase = 'SELECT 
							COUNT(*) AS Cumplidos
					FROM
							aspel_sae50.dbo.UPPEDIDOS U
						INNER JOIN
							aspel_sae50.dbo.PED_MSTR P
							ON U.PEDIDO = P.PEDIDO
						INNER JOIN
							aspel_sae50.dbo.CLIE01 CL
							ON LTRIM(RTRIM(P.CLIENTE)) =LTRIM(RTRIM(CL.CLAVE))
						LEFT JOIN
							aspel_sae50.dbo.ESTDPEDI
							ON P.PEDIDO = ESTDPEDI.PEDIDO
						WHERE 
							convert(datetime2,convert(varchar(255),F_VENCIMIENTO,102),102) BETWEEN ''' + cast(@fecha_inicial as varchar(20)) + ''' AND ''' + cast(@fecha_final as varchar(20)) + ''''
	
	if @tipo_fecha = 0
		begin
			set @sqlTipoFecha = ' AND CONVERT(VARCHAR,U.F_VENCIMIENTO,6) = CONVERT(VARCHAR,U.F_EMBARQUE,6)'
		end
	if @tipo_fecha = 1		
		begin
			set @sqlTipoFecha = ' AND CONVERT(VARCHAR,U.F_EMBARQUE,6) < CONVERT(VARCHAR,U.F_VENCIMIENTO,6)'		
		end
	if @tipo_fecha = 2
		begin
			set @sqlTipoFecha = ' AND CONVERT(VARCHAR,U.F_EMBARQUE,6) > CONVERT(VARCHAR,U.F_VENCIMIENTO,6)'		
		end
	if @tipo_fecha = 3
		begin
			set @sqlTipoFecha = ' AND U.F_EMBARQUE IS NULL'			
		end
	print @sqlBase + @sqlTipoFecha
	execute(@sqlBase + @sqlTipoFecha)
	
END
GO
