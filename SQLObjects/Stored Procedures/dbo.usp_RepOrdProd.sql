SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
--usp_RepOrdProd '1', '500', 2

CREATE PROCEDURE [dbo].[usp_RepOrdProd] 	
	@ReferenciaInicial varchar(20),
	@ReferenciaFinal varchar(20), 
	@tipo int --1. reporte de órdenes de produccion 2. reporte de órdenes de produccion no liberadas
AS
BEGIN

	set ansi_warnings off;
	SET NOCOUNT ON;

SELECT REFERENCIA, PRODUCTO, STATUS, CANTIDAD - CANTTERM AS CANTIDAD, X_OBSER AS OBS, DESCRIPCION = (select top 1 DESCR from aspel_sae50.dbo.INVE01 where SUBSTRING(CVE_ART,1,8) = SUBSTRING(PRODUCTO,1,8))
into #totales
FROM aspel_prod30.dbo.ORD_FAB01 
LEFT JOIN aspel_prod30.dbo.OBS_ORD01 
ON aspel_prod30.dbo.ORD_FAB01.OBSORD = aspel_prod30.dbo.OBS_ORD01.NUM_REG
WHERE ltrim(rtrim(REFERENCIA)) >=@ReferenciaInicial AND ltrim(rtrim(REFERENCIA)) <= @ReferenciaFinal AND CANTIDAD - CANTTERM > 0 
ORDER BY aspel_prod30.dbo.ORD_FAB01.REFERENCIA ASC,SUBSTRING(PRODUCTO,9,2),SUBSTRING(PRODUCTO,11,2)

if @tipo <>1
begin
	delete from #totales where STATUS <> 0 
end

select 
	REFERENCIA, 
	PRODUCTO, 
	STATUS, 
	CANTIDAD, 
	OBS, 
	DESCRIPCION
from #totales



END
GO
