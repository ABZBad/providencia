SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Israel Aragón
-- Create date: 22/09/2014
-- Description:	Devuelve información para llenar el módulo frmTransferenciaXPedido
-- =============================================
--usp_frmTransferenciaXPedido 602, 
--usp_frmTransferenciaXPedido 29626, 
--usp_frmTransferenciaXPedido 29782, 
--usp_frmTransferenciaXPedido 3636, 
CREATE PROCEDURE [dbo].[usp_frmTransferenciaXPedido]
(
	@pedido int,
	@origen int,
	@destino int
)
AS
BEGIN
	set ansi_warnings off;
	SET NOCOUNT ON;

	declare @cliente varchar(500)
	IF (SELECT count(*) FROM aspel_sae50.dbo.CLIE01 c LEFT JOIN aspel_sae50.dbo.FACTP01 f ON c.CLAVE = f.CVE_CLPV WHERE f.CVE_DOC = 'P' + convert(varchar(10), @pedido) AND TIP_DOC = 'P')>0
		BEGIN
			
			SELECT @cliente=NOMBRE FROM aspel_sae50.dbo.CLIE01 c LEFT JOIN aspel_sae50.dbo.FACTP01 f ON c.CLAVE = f.CVE_CLPV WHERE f.CVE_DOC = 'P' + convert(varchar(10), @pedido) AND TIP_DOC = 'P'

			if	(SELECT count(*) FROM aspel_sae50.dbo.CLIE01 c LEFT JOIN aspel_sae50.dbo.FACTP01 f ON c.CLAVE = f.CVE_CLPV WHERE f.CVE_DOC = 'P' + convert(varchar(10), @pedido) AND ENLAZADO = 'O' AND TIP_DOC_E ='O' AND TIP_DOC = 'P') > 0
				BEGIN
					IF (SELECT count(*) FROM aspel_sae50.dbo.UPPEDIDOS WHERE PEDIDO = @pedido AND F_LIBERADO IS NULL) > 0
						BEGIN
							SELECT CLIENTE = @cliente, PY.CVE_ART, ISNULL(I.LIN_PROD, '') LINEA, PY.CANT, origen = @origen, 
								EXIST = ISNULL((SELECT EXIST FROM aspel_sae50.dbo.MULT01 WHERE CVE_ALM= case when I.LIN_PROD <> 'ESPE' then @origen else 32 end AND CVE_ART=PY.CVE_ART), 0),
								destino = @destino, PY.PXS 
							into #tmp
							FROM aspel_sae50.dbo.PAR_FACTP01 PY LEFT JOIN aspel_sae50.dbo.INVE01 I ON PY.CVE_ART=I.CVE_ART WHERE PY.TIPO_PROD='P' AND PY.CVE_DOC = 'P' + convert(varchar(10), @pedido) ORDER BY PY.NUM_PAR

							select *, Status = case when EXIST>=PXS then 'Exist. Suficientes' else 'Exist. Insuficientes' end from #tmp
						END
					ELSE
						BEGIN
							select resultado='El Pedido tiene Fecha de Liberado en UPPEDIDOS, NO se Puede Transferir en Modo Matricial.'
						END
					
				END
			ELSE
				BEGIN
					select resultado='El Pedido Ya Está Facturado.'
				END

		END
	ELSE
		BEGIN
			select resultado='El Pedido No Existe.'
		END



END
GO
