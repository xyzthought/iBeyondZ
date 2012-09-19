
/*
CreateBusinessObject 'tblCS_AppUserLogin'
*/
CREATE PROCEDURE CreateBusinessObject
@TableName NVARCHAR(150)
AS
--declare @TableName sysname = 'tblCS_AppUserLogin'
declare @result nvarchar(max) = ''
declare @declare nvarchar(max) = ''

SELECT @declare=@declare +

CASE WHEN ISNULL(ColumnType,'') <>''
THEN 'private ' + ISNULL(ColumnType,'')+ ' ' + ISNULL(Precedor,'')+ ColumnName +' ; '+CHAR(13) ELSE '' END 

FROM
(Select Column_id, ColumnName,ColumnType,Precedor FROM
(
	--declare @TableName sysname = 'tblCS_AppUserLogin'
    select col.column_id, replace(col.name, ' ', '_') ColumnName, 
        case typ.name 
            when 'bigint' then 'long'
            when 'binary' then 'byte[]'
            when 'bit' then 'bool'
            when 'char' then 'char'
            when 'date' then 'DateTime'
            when 'datetime' then 'DateTime'
            when 'datetime2' then 'DateTime'
            when 'datetimeoffset' then 'DateTimeOffset'
            when 'decimal' then 'decimal'
            when 'float' then 'float'
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
        end ColumnType,
        case typ.name 
            when 'bigint' then 'mlng'
            when 'binary' then 'mbyte'
            when 'bit' then 'mbln'
            when 'char' then 'mchr'
            when 'date' then 'mdt'
            when 'datetime' then 'mdt'
            when 'datetime2' then 'mdt'
            when 'datetimeoffset' then 'mdt'
            when 'decimal' then 'mdbl'
            when 'float' then 'mdbl'
            when 'image' then 'mbyte'
            when 'int' then 'mint'
            when 'money' then 'mdbl'
            when 'nchar' then 'mchr'
            when 'ntext' then 'mstr'
            when 'numeric' then 'mdbl'
            when 'nvarchar' then 'mstr'
            when 'real' then 'mdbl'
            when 'smalldatetime' then 'mdt'
            when 'smallint' then 'mint'
            when 'smallmoney' then 'mdbl'
            when 'text' then 'mstr'
            when 'time' then 'mtm'
            when 'timestamp' then 'mdt'
            when 'tinyint' then 'mint'
            when 'uniqueidentifier' then 'mg'
            when 'varbinary' then 'mbyte'
            when 'varchar' then 'mstr'
        end Precedor
    from sys.columns col
        join sys.types typ on
            col.system_type_id = typ.system_type_id 
    where object_id = object_id(@TableName) 
) tt) a  order by column_id

select @result = @result +

/*
get { return mintUserID; }
set { mintUserID = value; }
*/
CASE WHEN ISNULL(ColumnType,'') <>''
THEN 
'public '+ISNULL(ColumnType,'')+ +' ' +ColumnName +CHAR(13)+'{'+CHAR(13)+
'get { return '+Precedor+ColumnName+'; }'+CHAR(13)+
'set { '+Precedor+ColumnName+' = value; }'+CHAR(13)+'}'+CHAR(13)

 ELSE '' 
END 


from
(
    SELECT Column_id, ColumnName,ColumnType,Precedor FROM (
		select  col.column_id,replace(col.name, ' ', '_') ColumnName, 
        case typ.name 
            when 'bigint' then 'long'
            when 'binary' then 'byte[]'
            when 'bit' then 'bool'
            when 'char' then 'char'
            when 'date' then 'DateTime'
            when 'datetime' then 'DateTime'
            when 'datetime2' then 'DateTime'
            when 'datetimeoffset' then 'DateTimeOffset'
            when 'decimal' then 'decimal'
            when 'float' then 'float'
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
        end ColumnType,
        case typ.name 
            when 'bigint' then 'mlng'
            when 'binary' then 'mbyte'
            when 'bit' then 'mbln'
            when 'char' then 'mchr'
            when 'date' then 'mdt'
            when 'datetime' then 'mdt'
            when 'datetime2' then 'mdt'
            when 'datetimeoffset' then 'mdt'
            when 'decimal' then 'mdbl'
            when 'float' then 'mdbl'
            when 'image' then 'mbyte'
            when 'int' then 'mint'
            when 'money' then 'mdbl'
            when 'nchar' then 'mchr'
            when 'ntext' then 'mstr'
            when 'numeric' then 'mdbl'
            when 'nvarchar' then 'mstr'
            when 'real' then 'mdbl'
            when 'smalldatetime' then 'mdt'
            when 'smallint' then 'mint'
            when 'smallmoney' then 'mdbl'
            when 'text' then 'mstr'
            when 'time' then 'mtm'
            when 'timestamp' then 'mdt'
            when 'tinyint' then 'mint'
            when 'uniqueidentifier' then 'mg'
            when 'varbinary' then 'mbyte'
            when 'varchar' then 'mstr'
        end Precedor
    from sys.columns col
        inner join sys.types typ on
            col.system_type_id = typ.system_type_id 
    where object_id = object_id(@TableName)) t ) a order by column_id
    

print @declare
print @result