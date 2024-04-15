SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
--usp_frmPartidas 5876
CREATE PROCEDURE [dbo].[usp_frmPartidas]
(
	@idPedido int 
)
AS
BEGIN
	SET NOCOUNT ON;

	--'P5876'

	if	(SELECT count(CVE_DOC) FROM aspel_sae50.dbo.FACTP01 WHERE ltrim(rtrim(CVE_DOC)) = 'P'+convert(varchar(10), @idPedido))>0
	begin
		SELECT CVE_ART,CANT,PREC FROM aspel_sae50.dbo.PAR_FACTP01 
		WHERE TIPO_PROD = 'P' AND LTRIM(RTRIM(CVE_DOC)) = 'P'+convert(varchar(10), @idPedido)
	end		
	else		
	begin
		SELECT CODIGO AS CVE_ART,CANTIDAD AS CANT,SUBTOTAL AS PREC FROM aspel_sae50.dbo.PED_DET 
		WHERE LTRIM(RTRIM(PEDIDO)) = 'P'+convert(varchar(10), @idPedido)
	end
END
GO
