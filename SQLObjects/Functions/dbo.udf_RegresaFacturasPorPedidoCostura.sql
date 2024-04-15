SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/*
18-Mar-2015 : Se crea función necesaria para el módulo

"Captura de facturas de proveedores de Fletes" solicitado por Rubén Alamán

*/
create FUNCTION [dbo].[udf_RegresaFacturasPorPedidoCostura]
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

	set @cursor = cursor for select CVE_DOC from COST_ENLA where PEDIDO=@numero_pedido
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
