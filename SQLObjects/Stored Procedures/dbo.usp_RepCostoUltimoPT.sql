SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_RepCostoUltimoPT]
	
AS
BEGIN
	SELECT 
		CVE_ART AS CLV_ART,
		DESCR,
		LIN_PROD,
		FCH_ULTCOM,
		EXIST,
		ULT_COSTO = case when ULT_COSTO = 0 then
						case when EXISTS(SELECT * FROM aspel_prod30.dbo.PT_DET01 WHERE ltrim(rtrim(CLAVE)) = INVE01.CVE_ART) then
							'0'
						else
							'S/E'
						end
					else
						cast(convert(numeric(12,4),ULT_COSTO) as varchar(20))
					end,
		IMPORTE = EXIST * ULT_COSTO
	FROM
		aspel_sae50.dbo.INVE01
	WHERE
		LIN_PROD <> 'MP'
		AND
		LIN_PROD <> 'HABI'
		AND
		LIN_PROD <>	'INSU'
		AND
		TIPO_ELE = 'P'
		AND
		EXIST > 0
	ORDER BY
		LIN_PROD
	

 
END
GO
