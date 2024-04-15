/*
12-Mar-2015 : Se mejora performance en la generación del reporte
*/
ALTER procedure usp_RepEstadoCuentaXDepto
	(
		@nombre_departamento	as varchar(50)
	)
as
begin

	SET TRANSACTION ISOLATION LEVEL	READ UNCOMMITTED
	set nocount on	
	SELECT		
		CMT_INDX AS 'Indice',
		CMT_PEDIDO AS'Pedido',
		CMT_MODELO AS'Modelo',
		CLIE01.NOMBRE AS 'Cliente',
		CMT_CANTIDAD AS 'Cantidad',
		CMT_RUTA AS 'Ruta',
		CMT_DEPARTAMENTO AS 'Depto.',
		CMT_TIPO AS 'Tipo',
		CMT_FVENC AS 'F. Venc.'
	into #t_final
	FROM
		aspel_sae50.dbo.CMT_DET
	JOIN
		aspel_sae50.dbo.CLIE01
		ON
		ltrim(rtrim(CMT_CLIENTE)) = rtrim(ltrim(CLAVE))
	join
		aspel_prod30.dbo.U_DEPARTAMENTO
		ON
		CMT_DEPARTAMENTO = aspel_prod30.dbo.U_DEPARTAMENTO.NOMBRE
	Where
		DEPARTAMENTO = @nombre_departamento and CMT_ESTATUS = 'R'
	/*order by
		CMT_INDX,CMT_PEDIDO*/
		
	select * from #t_final order by Indice,Pedido
	drop table #t_final
end