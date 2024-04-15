/*
13-Mar-2015: se resetea la variable @prendas a 0 (cero)
*/
ALTER PROCEDURE usp_RepPedidosMas20Dias
	@Fecha Datetime
AS
BEGIN
	set transaction isolation level read uncommitted

declare @iTot	as int

create table #tabla_mestra
(	
	[F_VENCIMIENTO] [smalldatetime],
	[CVE_DOC]		[varchar](30),
	[FECHA_DOC]		[datetime],
	[CCLIE]			[varchar](20),
	[NOMBRE]		[varchar](130),
	[CVE_VEND]		[varchar](10),
	[PRENDAS]		[int] default(0),
	[Dif]			[int] default(0)
)
alter table #tabla_mestra add [UID] int identity(1,1)
create unique clustered index idx on #tabla_mestra([UID])


insert into #tabla_mestra (F_VENCIMIENTO,CVE_DOC,FECHA_DOC,CCLIE,NOMBRE,CVE_VEND,Dif)
SELECT
        F_VENCIMIENTO,
        CVE_DOC,
        FECHA_DOC,
        CLAVE CCLIE,
        NOMBRE,
        FACT01.CVE_VEND,
        DATEDIFF(day,FECHA_DOC,@Fecha)        
FROM aspel_sae50..UPPEDIDOS
RIGHT JOIN aspel_sae50..FACTP01 AS FACT01
        ON 'P' + LTRIM(RTRIM(PEDIDO)) = LTRIM(RTRIM(CVE_DOC))
JOIN aspel_sae50..CLIE01
        ON CLIE01.CLAVE = FACT01.CVE_CLPV
WHERE TIP_DOC = 'P'
AND FECHA_DOC <= DATEADD(DAY, 20, @Fecha)
AND ENLAZADO = 'O'
AND FACT01.STATUS <> 'C'
ORDER BY FECHA_DOC

set @iTot = @@rowcount

declare @iCont	as	int
declare @CVE_DOC	as	varchar(30)
declare @prendas	as int

set @iCont = 1
set @prendas = 0

while @iCont < = @iTot 
	begin
		select @CVE_DOC = CVE_DOC from #tabla_mestra where [UID] = @iCont
		
		SELECT
				@prendas = isnull(SUM(CANT),0)
		FROM aspel_sae50..PAR_FACTP01 AS FA0TY1
		JOIN aspel_sae50..INVE01
				ON LTRIM(RTRIM(INVE01.CVE_ART)) = LTRIM(RTRIM(FA0TY1.CVE_ART))
		WHERE
			LTRIM(RTRIM(CVE_DOC)) = @CVE_DOC
			AND TIPO_PROD = 'P'
			AND LTRIM(RTRIM(LIN_PROD)) <> 'MP'
		GROUP BY FA0TY1.CVE_DOC
		
		update #tabla_mestra set PRENDAS = @prendas where [UID] = @iCont
		
		set @iCont = @iCont + 1
		set @prendas = 0
	end


select * from #tabla_mestra order by FECHA_DOC,CVE_DOC

drop table #tabla_mestra


END