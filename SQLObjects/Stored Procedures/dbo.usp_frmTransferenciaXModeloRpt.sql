SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Israel Aragón
-- Create date: 10/09/2014
-- Description:	Devuelve información para llenar el módulo frmTransferenciaXModelo
-- =============================================
--usp_frmTransferenciaXModeloRpt 'PAGAMOSH2830'
CREATE PROCEDURE [dbo].[usp_frmTransferenciaXModeloRpt]
(
	@CVE_FOLIO varchar(9) 
)
AS
BEGIN
	set ansi_warnings off;
	SET NOCOUNT ON;


	select CVE_ART AS CLV_ART, CVE_CPTO AS TIPO_MOV, FECHA_DOCU, REFER, CANT, ALMACEN, EXISTENCIA AS DB8EXIST 
	FROM aspel_sae50.dbo.MINVE01 where CVE_FOLIO = @CVE_FOLIO order by CLV_ART, ALMACEN


END
GO
