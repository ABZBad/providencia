/*
Tabla controlar los cambios de los objetos en la base de datos
*/
create table DDLControl (
	[uid] int identity(1,1) not null,
	[Version] int default(0),
	[UpdateDate] date not null,
	[UpdateTime] [time](7) not null,
	[ObjectName] varchar(255) not null,
	[UpdateDesc] varchar(500) not null,
	[UpdatedBy] varchar(255) not null
)
