SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_RepPedidosCapturaSae]
	-- Add the parameters for the stored procedure here
	(
		@numero_pedido as	varchar(100)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		P.CVE_DOC,
		P.CVE_CLPV,
		C.NOMBRE,
		DETALLE.CVE_ART,
		DETALLE.DESCR,
		DETALLE.CANT
	FROM
		aspel_sae50.dbo.FACTP01 P 
	LEFT JOIN
		aspel_sae50.dbo.CLIE01 C 
		ON
		C.CLAVE = P.CVE_CLPV
	INNER JOIN
		(
			SELECT
				P.CVE_DOC,
				P.CVE_ART,
				DESCR,
				CANT 
			FROM
				aspel_sae50.dbo.PAR_FACTP01 P 
			LEFT JOIN
				aspel_sae50.dbo.INVE01 I
				ON
				I.CVE_ART = P.CVE_ART
			WHERE P.CVE_DOC = @numero_pedido
	 ) AS DETALLE
	 ON 
	 P.CVE_DOC = DETALLE.CVE_DOC
	where P.CVE_DOC = @numero_pedido


    
END
GO
