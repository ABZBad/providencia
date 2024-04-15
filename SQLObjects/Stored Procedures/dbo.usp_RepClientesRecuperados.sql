SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_RepClientesRecuperados]
	(
		@fecha_doc		as	datetime
	)
AS
BEGIN
	
			SET NOCOUNT ON

			SELECT 
				CVE_CLPV,
				FECHA_DOC,
				CVE_DOC
			INTO
				#CLIENTES_PEDIDO2
			FROM (
					SELECT 
							LTRIM(RTRIM(CVE_CLPV)) CVE_CLPV,
							FECHA_DOC,
							CVE_DOC,
							ROW_NUMBER() OVER(PARTITION BY LTRIM(RTRIM(CVE_CLPV))
					ORDER BY
							FECHA_DOC DESC
					) 
					ROWNUM FROM aspel_sae50.dbo.FACTF01 WHERE STATUS<>'C'
				) P2 
			WHERE P2.ROWNUM=2 AND convert(datetime2,convert(varchar(255),P2.FECHA_DOC,102),102) <= DATEADD(DAY,-1,DATEADD(MONTH,-13,@fecha_doc)) ORDER BY CVE_CLPV 

			SELECT 
				V.NOMBRE AS 'Nombre',
				P1.CVE_CLPV AS 'Código',
				V.CVE_VEND AS 'Vendedor',
				C.NOMBRE AS 'Razón Social',
				P1.CVE_DOC AS 'Factura',
				P1.PRENDAS AS 'Prendas'
			FROM 
				(
					SELECT 
							LTRIM(RTRIM(CVE_CLPV)) CVE_CLPV,
							LTRIM(RTRIM(CVE_VEND)) CVE_VEND,
							FECHA_DOC, CVE_DOC, (
													SELECT 
															SUM(CANT)
													FROM
															aspel_sae50.dbo.PAR_FACTF01
													WHERE
														PAR_FACTF01.CVE_DOC = FACTF01.CVE_DOC
												) AS PRENDAS,
							ROW_NUMBER() OVER(PARTITION BY LTRIM(RTRIM(CVE_CLPV))
							ORDER BY FECHA_DOC DESC
				) ROWNUM FROM aspel_sae50.dbo.FACTF01
			WHERE STATUS<>'C') P1 
				LEFT JOIN aspel_sae50.dbo.CLIE01 C ON LTRIM(RTRIM(P1.CVE_CLPV))=LTRIM(RTRIM(C.CLAVE)) 
				LEFT JOIN aspel_sae50.dbo.VEND01 V ON P1.CVE_VEND=LTRIM(RTRIM(V.CVE_VEND))
				LEFT JOIN #CLIENTES_PEDIDO2 P2 ON P1.CVE_CLPV=P2.CVE_CLPV
			WHERE P1.ROWNUM=1 AND convert(datetime2,convert(varchar(255),P1.FECHA_DOC,102),102) BETWEEN DATEADD(DAY,-1,DATEADD(MONTH,-1,@fecha_doc)) AND @fecha_doc
			AND
				P1.CVE_CLPV IN (SELECT CVE_CLPV FROM #CLIENTES_PEDIDO2) ORDER BY V.NOMBRE, P1.CVE_CLPV
			drop table #CLIENTES_PEDIDO2
	
END
GO
