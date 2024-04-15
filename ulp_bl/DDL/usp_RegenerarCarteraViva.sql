ALTER procedure usp_RegenerarCarteraViva
(
	@annio		int
)
as

set nocount on
set transaction isolation level read uncommitted
declare @iCont		int
declare @iTot		int

declare @MES		float
declare @PRENDAS	float
declare @TIP_DOC	varchar(2)


--set @annio		= 2014


create table #tabla_maestra
(
	[UID]		int identity(1,1) not null,
	CLIENTE		[varchar](50)	
)
create unique clustered index idx on #tabla_maestra ([UID])

create table #tabla_maestra_detalle
(
	[UID]		int identity(1,1) not null,
	CLIENTE		[varchar](50),
	MES			[float],
	PRENDAS		[float],
	TIP_DOC		[varchar](2)
)
create clustered index idx on #tabla_maestra_detalle([CLIENTE])

create table #tabla_maestra_detalle_x_cliente
(
	[UID]		int identity(1,1) not null,
	CLIENTE		[varchar](50),
	MES			[float],
	PRENDAS		[float],
	TIP_DOC		[varchar](2)
)
create clustered index idx on #tabla_maestra_detalle_x_cliente([UID])

delete from aspel_sae50..CARTERA WHERE ANNIO = @annio


DBCC CHECKIDENT('aspel_sae50..CARTERA', RESEED, 0)



insert into #tabla_maestra_detalle (CLIENTE,MES,PRENDAS,TIP_DOC)
SELECT rtrim(ltrim(F.CVE_CLPV)) CLIENTE, MONTH(FECHA_DOC) MES,PRENDAS = CASE WHEN F.TIP_DOC = 'F' THEN SUM(CANT) ELSE SUM(CANT) * -1 END, F.TIP_DOC
            FROM aspel_sae50..FACTF01 F JOIN aspel_sae50..PAR_FACTF01 FY ON F.CVE_DOC = FY.CVE_DOC
            JOIN aspel_sae50..INVE01 I ON LTRIM(RTRIM(I.CVE_ART))=LTRIM(RTRIM(FY.CVE_ART))
            WHERE FY.TIPO_PROD='P' AND LTRIM(RTRIM(I.LIN_PROD))<>'MP'
            AND YEAR(FECHA_DOC)=@annio AND F.STATUS<>'C'
            GROUP BY F.CVE_CLPV, MONTH(F.FECHA_DOC), F.TIP_DOC
            UNION
            SELECT rtrim(ltrim(F.CVE_CLPV)) CLIENTE, MONTH(FECHA_DOC) MES,PRENDAS = CASE WHEN F.TIP_DOC = 'F' THEN SUM(CANT) ELSE SUM(CANT) * -1 END, F.TIP_DOC
            FROM aspel_sae50..FACTD01 F JOIN aspel_sae50..PAR_FACTD01 FY ON F.CVE_DOC = FY.CVE_DOC
            JOIN aspel_sae50..INVE01 I ON LTRIM(RTRIM(I.CVE_ART))=LTRIM(RTRIM(FY.CVE_ART))
            WHERE FY.TIPO_PROD='P' AND LTRIM(RTRIM(I.LIN_PROD))<>'MP'
            AND YEAR(FECHA_DOC)= @annio AND F.STATUS<>'C'
            GROUP BY F.CVE_CLPV, MONTH(F.FECHA_DOC), F.TIP_DOC
            
            
insert into #tabla_maestra (CLIENTE) select distinct CLIENTE from #tabla_maestra_detalle --where CLIENTE = '1018'
set @iTot	=	@@rowcount            
set @iCont	=	1


declare @iCont2		int
declare @iTot2		int

declare @iMes		int
declare @factura	float
declare	@nc			float

declare @cliente	varchar(20)
declare @uid		int
declare @sql		varchar(max)
declare @m1			float
declare @m2			float
declare @m3			float
declare @m4			float
declare @m5			float
declare @m6			float
declare @m7			float
declare @m8			float
declare @m9			float
declare @m10		float
declare @m11		float
declare @m12		float
declare @total		float

set @total = 0

while @iCont <= @iTot
	begin
		select @cliente = CLIENTE from #tabla_maestra where [UID] = @iCont
		
		insert into #tabla_maestra_detalle_x_cliente (CLIENTE,MES,PRENDAS,TIP_DOC) select CLIENTE,MES,PRENDAS,TIP_DOC from #tabla_maestra_detalle where CLIENTE = @cliente order by MES
		set @iTot2	= @@rowcount
		set @iCont2	= 1
		while @iCont2 <= @iTot2
			begin
				select @MES = MES,@PRENDAS = PRENDAS,@TIP_DOC = TIP_DOC from #tabla_maestra_detalle_x_cliente where [UID] = @iCont2
				select @uid = ID,@m1 = isnull(MES1,0), @m2 = isnull(MES2,0), @m3 = isnull(MES3,0), @m4 = isnull(MES4,0), @m5 = isnull(MES5,0), @m6 = isnull(MES6,0), @m7 = isnull(MES7,0),@m8 =isnull(MES8,0), @m9 = isnull(MES9,0), @m10 = isnull(MES10,0), @m11 = isnull(MES11,0), @m12 = isnull(MES12,0) from aspel_sae50..CARTERA where CLIENTE = @cliente and ANNIO = @annio
				if @@rowcount = 0
					begin
						--select @MES AS MES,@PRENDAS AS PRENDAS
						set @sql = 'insert into aspel_sae50..CARTERA (CLIENTE,ANNIO,MES' + cast(@MES as varchar(2)) + ') values (''' + @cliente + ''',' + cast(@annio as varchar(4)) + ',' + cast(@PRENDAS as varchar(50)) +  ')'
						--print @sql
						exec (@sql)
						set @total = @total + @PRENDAS
					end
				else
					begin
						--select @MES,@m1,@m2,@m3,@m4,@m5,@m6,@m7,@m8,@m9,@m10,@m11,@m12,@PRENDAS
						if (@MES = 1)							
							set @m1 = @m1 + @PRENDAS
						
						if (@MES = 2)							
							set @m2= @m2 + @PRENDAS
							
						if (@MES = 3)							
							set @m3 = @m3 + @PRENDAS
						
						if (@MES = 4)							
							set @m4 = @m4 + @PRENDAS
							
						if (@MES = 5)							
							set @m5 = @m5 + @PRENDAS
							
						if (@MES = 6)							
							set @m6 = @m6 + @PRENDAS
							
						if (@MES = 7)							
							set @m7 = @m7 + @PRENDAS
							
						if (@MES = 8)							
							set @m8 = @m8 + @PRENDAS
							
						if (@MES = 9)							
							set @m9 = @m9 + @PRENDAS
							
						if (@MES = 10)							
							set @m10 = @m10 + @PRENDAS
							
						if (@MES = 11)							
							set @m11 = @m11 + @PRENDAS
							
						if (@MES = 12)							
							set @m12 = @m12 + @PRENDAS
						
						set @total = @total + @PRENDAS
						
						update aspel_sae50..CARTERA set MES1 = @m1,MES2 = @m2,MES3 = @m3,MES4 = @m4,MES5 = @m5,MES6 = @m6,MES7 = @m7,MES8 = @m8,MES9 = @m9,MES10 = @m10, MES11 = @m11, MES12 = @m12 WHERE CLIENTE = @cliente and ANNIO = @annio
						
					end					
				set @iCont2 = @iCont2 + 1
			end			
			update aspel_sae50..CARTERA set TOTAL = @total WHERE CLIENTE = @cliente and ANNIO = @annio
			set @total = 0
		delete #tabla_maestra_detalle_x_cliente
		DBCC CHECKIDENT('#tabla_maestra_detalle_x_cliente', RESEED, 0)		
		set @iCont = @iCont + 1
	end



--select * from aspel_sae50..CARTERA

drop table #tabla_maestra
drop table #tabla_maestra_detalle
drop table #tabla_maestra_detalle_x_cliente
