SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE procedure [dbo].[usp_RegresaCostoInventarioMP]
(
	@operador		as	nvarchar(3),
	@order			as	nvarchar(4),
	@articulo		as	nvarchar(20),
	@fecha			as	date
)
as

declare	@sql			as	nvarchar(max)

set @sql =
'SELECT 			
			FECHA_DOCU,COSTO
		FROM
			aspel_sae50.dbo.MINVE01
		WHERE
			CVE_CPTO=1
			AND
			LTRIM(RTRIM(CVE_ART)) = ''' + @articulo + ''' AND convert(date,FECHA_DOCU) ' + @operador + ' ''' + cast(@fecha as nvarchar(20)) + ''' ORDER BY FECHA_DOCU ' + @order + ', NUM_MOV ' + @order


--print @sql
exec(@sql)


GO
