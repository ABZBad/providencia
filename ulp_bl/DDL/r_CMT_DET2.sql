UPDATE aspel_sae50..CMT_DET SET CMT_ESTATUS = 'R' 
from aspel_sae50..CMT_DET cmt 
INNER JOIN aspel_sae50..UPPEDIDOS up ON cmt.CMT_PEDIDO = up.PEDIDO 
WHERE cmt.CMT_ESTATUS ='P' and up.F_LIBERADO is not null and year(up.F_LIBERADO)=2015