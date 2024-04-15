﻿UPDATE U SET U.CMT_FVENC= REG_A_ACTUALIZAR.CMT_FVENC
FROM
	aspel_sae50..CMT_DET U
	INNER JOIN 
		(
			SELECT
				B.CMT_PEDIDO,
				B.CMT_FVENC
			FROM
				aspel_sae50..CMT_DET C
			INNER JOIN (
						SELECT 
							CMT_PEDIDO,
							CMT_FVENC
						FROM
							aspel_sae50..CMT_DET
						WHERE
							CMT_FVENC IS NOT NULL
							AND year(CMT_FVENC) = 2015
						) AS B
			ON
				B.CMT_PEDIDO = C.CMT_PEDIDO
			WHERE C.CMT_FVENC IS NULL
			) AS REG_A_ACTUALIZAR
		ON U.CMT_PEDIDO = REG_A_ACTUALIZAR.CMT_PEDIDO
WHERE U.CMT_FVENC IS NULL