SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE procedure [dbo].[usp_ModelosYTallasPorModelo]
(
	@modelo		as varchar(25)
)
--datos del modelo
as
SELECT TOP 1
        SUBSTRING(I.CVE_ART, 1, 8) MODELO,
        DESCR,
        LIN_PROD,
        ISNULL(LIN.DESC_LIN, '') DESC_LIN,
        ISNULL(PXP.PRECIO, 0) PRECIO,
        I.CVE_ART
FROM
	aspel_sae50..INVE01 I
LEFT JOIN aspel_sae50..CLIN01 LIN
        ON I.LIN_PROD = LIN.CVE_LIN
LEFT JOIN aspel_sae50..PRECIO_X_PROD01 PXP
        ON I.CVE_ART = PXP.CVE_ART
WHERE I.CVE_ART LIKE @modelo
AND LEN(I.CVE_ART) = 12
AND TIPO_ELE = 'P'
AND I.STATUS = 'A'
ORDER BY I.CVE_ART



--Almacenes existentes
SELECT DISTINCT(CVE_ALM) CVE_ALM FROM aspel_sae50..MULT01 WHERE CVE_ART LIKE @modelo ORDER BY CVE_ALM

--Agregar_Codigos_Existentes
SELECT
        SUBSTRING(CVE_ART, 9, 4) TALLA,
        DESCR,
        CASE STATUS
                WHEN 'A' THEN 'ACTIVO'
                ELSE 'BAJA'
        END ESTATUS,
        ISNULL(EXIST, 0) EXIST, 
		Accion = 'Actualizar'
FROM
	aspel_sae50..INVE01
WHERE CVE_ART LIKE @modelo
AND LEN(CVE_ART) = 12
AND TIPO_ELE = 'P'
ORDER BY CVE_ART




GO
