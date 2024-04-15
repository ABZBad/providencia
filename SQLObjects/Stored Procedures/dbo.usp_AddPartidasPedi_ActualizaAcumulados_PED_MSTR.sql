SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Israel Aragón
-- Create date: 25/11/2014
-- Description:	Procedimiento que actualiza valores en PED_MSTR
-- =============================================
CREATE PROCEDURE [dbo].[usp_AddPartidasPedi_ActualizaAcumulados_PED_MSTR]
(
	@Pedido int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT PEDIDO, SUM(CANTIDAD) AS PRENDAS,
		SUM(PRECIO_PROD * CANTIDAD) AS TOT_VENDIDO, 
		SUM(CANTIDAD * (PRECIO_PROD + PREC_PROCESO)) AS SUBTOTAL, 
		case when SUM(PRECIO_LISTA * (CANTIDAD)   ) >0 then
			1 -(SUM(PRECIO_PROD * CANTIDAD) / SUM(PRECIO_LISTA * (CANTIDAD)   )) 
		else	
			0
		end
		AS DESCUENTO 
	into #tmp
	FROM aspel_sae50.dbo.PED_DET WHERE PEDIDO = @Pedido
	group by PEDIDO

	update aspel_sae50.dbo.PED_MSTR
	SET
		DESCUENTO = t.DESCUENTO,
		IMPORTE = t.SUBTOTAL,
		PRENDAS = t.PRENDAS,
		DESC_DADO = t.DESCUENTO,
		COMISION = case when p.TIPO = 'OT' then
						(6.5 + (-0.33333333333 * (t.DESCUENTO * 100))) / 100
					else
						(6.5 + (-0.33333333333 * (t.DESCUENTO * 100)))
					end
	from aspel_sae50.dbo.PED_MSTR p 
	inner join #tmp t on p.PEDIDO = t.PEDIDO

	drop table #tmp

END
GO
