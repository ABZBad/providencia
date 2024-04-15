CREATE TABLE [dbo].[DDLControl]
(
[uid] [int] NOT NULL IDENTITY(1, 1),
[Version] [int] NULL CONSTRAINT [DF__DDLContro__Versi__73501C2F] DEFAULT ((0)),
[UpdateDate] [date] NOT NULL,
[UpdateTime] [time] NOT NULL,
[ObjectName] [varchar] (255) COLLATE Modern_Spanish_CI_AS NOT NULL,
[UpdateDesc] [varchar] (500) COLLATE Modern_Spanish_CI_AS NOT NULL,
[UpdatedBy] [varchar] (255) COLLATE Modern_Spanish_CI_AS NOT NULL
) ON [PRIMARY]
GO
