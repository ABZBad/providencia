
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Israel Aragón
-- Create date: 06/08/2014
-- Description:	Sp para reporte: Reporte recepción Maquila
--06-abr-2015 : Se cambian los nombres de las columnas
-- =============================================
CREATE PROCEDURE [dbo].[usp_RepRecepcionMaquila]
(
	@referencia varchar(5000)
)
AS
BEGIN

	SET NOCOUNT ON;
	set ansi_warnings off;

	SELECT
		NUM_MOV AS REG,
		CVE_ART AS ARTICULO,
		CASE SIGNO WHEN -1 THEN
						'E'
					WHEN 1 THEN
						'S'
					END AS MOV,
		FECHA_DOCU,
		DOCTO=rtrim(ltrim(REFER)),
		CANT,
		ALMACEN 
	FROM
		aspel_sae50.dbo.MINVE01 
	WHERE charindex('('+LTRIM(RTRIM(REFER))+')', @referencia)>0
		AND
			YEAR(FECHA_DOCU) = YEAR(getdate())
		AND
			MONTH(FECHA_DOCU) = MONTH(getdate()) 
		AND
			DAY(FECHA_DOCU) = DAY(getdate())
		
END
GO
