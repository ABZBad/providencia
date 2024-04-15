
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE procedure [dbo].[usp_RegresaRutasPorPedido]
(
	@pedido			int
)

as


set transaction isolation level read uncommitted
set nocount on

--set @pedido = 33285


declare @iCont	int
declare @iTot	int

SELECT
        PEDIDO,
        SUBSTRING(CODIGO, 1, 8) AS MODELO,
        AGRUPADOR,
        SUM(PED_DET.CANTIDAD) AS SUMA
into #tabla_mestra
FROM
	aspel_sae50..PED_DET

WHERE PEDIDO = @pedido
AND PROCESOS <> ''
GROUP BY PEDIDO,
         SUBSTRING(CODIGO, 1, 8),
         AGRUPADOR
set @iTot = @@rowcount         
set @iCont = 1

declare @modelo		as	varchar(8)
declare @agrupador	as	varchar(20)
declare @cmt_ruta	as	varchar(15)

alter table #tabla_mestra add [RUTA] varchar(20)
alter table #tabla_mestra add [UID] int identity(1,1)
create unique clustered index idx on #tabla_mestra([UID])


while @iCont <= @iTot
	begin
		select
			@modelo		= MODELO,
			@agrupador	= AGRUPADOR						
		from
			#tabla_mestra
		where
			[UID] = @iCont
		
		SELECT distinct 
			@cmt_ruta = CMT_RUTA
		FROM
			aspel_sae50..CMT_DET
		WHERE
			CMT_PEDIDO = cast(@pedido as varchar(15)) AND ltrim(rtrim(CMT_AGRUPADOR)) = @agrupador AND CMT_MODELO=@modelo
		
		update #tabla_mestra set RUTA = @cmt_ruta where [UID] = @iCont
		set @cmt_ruta = '--'	
		set @iCont = @iCont + 1
	end



select * from #tabla_mestra order by MODELO

drop table #tabla_mestra

GO
