ALTER PROCEDURE [dbo].[usp_BuscaPedidosPorCriterio]
	(
		@criterio	as varchar(100),
		@clave		as varchar(50)
	)
AS


declare @acceso			as varchar(10)

	select top 1 @acceso = RTRIM(LTRIM(isnull(ACCESO,'L'))) from aspel_sae50..USUARIOS where CLAVE = @clave


SELECT
		PEDIDO,
		AGENTE,
		FECHA, 
		CLIENTE,
		NOMBRE
	into #final
	FROM
		aspel_sae50.dbo.PED_MSTR
	JOIN 
		aspel_sae50.dbo.CLIE01 ON ltrim(rtrim(PED_MSTR.CLIENTE)) = ltrim(rtrim(CLIE01.CLAVE))
	WHERE 
		convert(varchar(8), PEDIDO) LIKE '%' + @criterio + '%'		

	insert into #final
	SELECT
		PEDIDO AS CVE_DOC,
		AGENTE,
		FECHA, 
		CLIENTE AS CVE_CLPV,
		NOMBRE	
	FROM
		aspel_sae50.dbo.PED_MSTR
	JOIN 
		aspel_sae50.dbo.CLIE01 ON ltrim(rtrim(PED_MSTR.CLIENTE)) = ltrim(rtrim(CLIE01.CLAVE))
	WHERE 
		UPPER(NOMBRE) LIKE '%' + UPPER(@criterio) + '%'


	select 
			PEDIDO,
			FECHA,
			CLIENTE,
			NOMBRE
	from
		#final
	WHERE
		rtrim(ltrim(AGENTE)) like case when @acceso = 'T' then '%%%' else '%' + @clave + '%' end
	
	order by PEDIDO desc

	drop table #final