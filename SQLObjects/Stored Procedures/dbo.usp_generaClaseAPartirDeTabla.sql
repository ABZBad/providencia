SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

--usp_generaClaseAPartirDeTabla 'ALMACENES01'

CREATE PROCEDURE [dbo].[usp_generaClaseAPartirDeTabla]
	(
		@TableName sysname
	)	
AS
BEGIN
	SET NOCOUNT ON;  

   
	declare @result varchar(2000) = 'public class ' + @TableName + '

{'

 

select @result = @result + '
    public ' + ColumnType + ' ' + ColumnName + ' { get; set; }'

from

(

    select

        replace(col.name, ' ', '_') ColumnName,

        column_id,
		
        case typ.name

            when 'bigint' then 'long'

            when 'binary' then 'byte[]'

            when 'bit' then 'bool'

            when 'char' then 'string'

            when 'date' then 'DateTime'

            when 'datetime' then 'DateTime'

            when 'datetime2' then 'DateTime'

            when 'datetimeoffset' then 'DateTimeOffset'

            when 'decimal' then 'decimal'

            when 'float' then 'double'

            when 'image' then 'byte[]'

            when 'int' then 'int'

            when 'money' then 'decimal'

            when 'nchar' then 'char'

            when 'ntext' then 'string'

            when 'numeric' then 'decimal'

            when 'nvarchar' then 'string'

            when 'real' then 'double'

            when 'smalldatetime' then 'DateTime'

            when 'smallint' then 'short'

            when 'smallmoney' then 'decimal'

            when 'text' then 'string'

            when 'time' then 'TimeSpan'

            when 'timestamp' then 'DateTime'

            when 'tinyint' then 'byte'

            when 'uniqueidentifier' then 'Guid'

            when 'varbinary' then 'byte[]'

            when 'varchar' then 'string'

            else 'UNKNOWN_' + typ.name

        end + case when col.is_nullable = 1 and typ.name not in('varchar','text') then '?' else '' end ColumnType 

    from sys.columns col
        join sys.types typ on

            col.system_type_id = typ.system_type_id AND col.user_type_id = typ.user_type_id

    where object_id = object_id(@TableName)

) t

order by column_id

 



set @result = @result  + '

}'

select @result

END
GO
