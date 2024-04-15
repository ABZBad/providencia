SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[udf_RegresaFacturasPorPedido] 
(
	@numero_pedido		as int
)
RETURNS varchar(max)
AS
BEGIN
	

	declare @cursor		as cursor
	declare @cve_doc	as	varchar(100)
	declare @facturas	as	varchar(max)

	set @facturas = ''

	set @cursor = cursor for select CVE_DOC from aspel_sae50.dbo.FLET_ENLA where PEDIDO=@numero_pedido
	open @cursor
	fetch next from @cursor into @cve_doc



	while @@fetch_status = 0
		begin
			set @facturas = @facturas + @cve_doc + ', '
			fetch next from @cursor into @cve_doc
		end
		
	close		@cursor
	deallocate	@cursor

	if (ltrim(rtrim(@facturas)) <> '')
		begin
			set @facturas = rtrim(ltrim(substring(@facturas,1,len(@facturas)-1)))
		end
	
	return @facturas

END
GO
