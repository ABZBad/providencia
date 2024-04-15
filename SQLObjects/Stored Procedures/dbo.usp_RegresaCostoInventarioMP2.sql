SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE procedure [dbo].[usp_RegresaCostoInventarioMP2]
(
	@operador		as	nvarchar(3),
	@order			as	nvarchar(4),
	@articulo		as	nvarchar(20),
	@fecha			as	date
)
as

declare	@sql			as	nvarchar(max)

set @sql =
'SELECT top 1			
			C.FECHA_DOC,
			isnull(CY.COST,0) as COSTO
		FROM
			aspel_sae50.dbo.COMPC01 C
		INNER JOIN
			aspel_sae50.dbo.PAR_COMPC01 CY
		ON
			C.CVE_DOC=CY.CVE_DOC
		WHERE
			C.TIP_DOC=''c''
			AND
			C.STATUS<>''C''
			AND
			LTRIM(RTRIM(CVE_ART)) = ''' + @articulo + '''
			AND
			convert(date,C.FECHA_DOC) ' + @operador + ' ''' + cast(@fecha as varchar(20)) + '''	ORDER BY C.FECHA_DOC ' + @order + ', C.CVE_DOC ' + @order


--print @sql
exec(@sql)


GO
