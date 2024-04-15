SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_RepClientesNuevos]
	(
		@fecha_doc		as		datetime,
		@tipo			as		int				--0=FORANEO,	1=METROPOLITANO
	)
	
AS
BEGIN
	SET NOCOUNT ON
	
	
	SELECT VEND.NOMBRE   AS 'Vendedor', 
		   CLIE.CLAVE    AS 'Clv. Cliente', 
		   CLIE.CVE_VEND AS 'Clv. Vendedor', 
		   CLIE.NOMBRE   AS 'Cliente', 
		   CVE_DOC       AS Factura, 
		   P2.PRENDAS    AS Prendas 
	From   (SELECT LTRIM(RTRIM(CVE_CLPV)) AS CVE_CLPV, 
				   FECHA_DOC, 
				   CVE_DOC, 
				   ROW_NUMBER() 
					 OVER( 
					   PARTITION BY LTRIM(RTRIM(CVE_CLPV)) 
					   ORDER BY FECHA_DOC ASC)                    ROWNUM, 
				   (SELECT SUM(CANT) 
					FROM   aspel_sae50.dbo.PAR_FACTF01 
					WHERE  PAR_FACTF01.CVE_DOC = FACTF01.CVE_DOC) AS PRENDAS 
			FROM   aspel_sae50.dbo.FACTF01 
			WHERE  STATUS <> 'C') P2 
		   INNER JOIN aspel_sae50.dbo.CLIE01 CLIE 
				   ON LTRIM(RTRIM(P2.CVE_CLPV)) = LTRIM(RTRIM(CLIE.CLAVE)) 
		   INNER JOIN aspel_sae50.dbo.VEND01 VEND 
				   ON CLIE.CVE_VEND = VEND.CVE_VEND 
	WHERE  P2.ROWNUM = 1 
		   AND convert(datetime2,convert(varchar(255),P2.FECHA_DOC,102),102) BETWEEN DATEADD(DAY, -1, DATEADD(MONTH, -1,@fecha_doc)) AND @fecha_doc 
		   AND CLIE.STATUS = 'A' AND VEND.CORREOE = case when @tipo = 0 then 'F' else 'M' end
	       
	ORDER  BY VEND.NOMBRE,CLIE.CLAVE 
    
END
GO
