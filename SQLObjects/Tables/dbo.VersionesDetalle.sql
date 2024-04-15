CREATE TABLE [dbo].[VersionesDetalle]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[VersionesDescripcion] [nvarchar] (max) COLLATE Modern_Spanish_CI_AS NOT NULL,
[VersionesPrincipalId] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[VersionesDetalle] ADD CONSTRAINT [PK_VersionesDetalle] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_VersionesPrincipalVersionesDetalle] ON [dbo].[VersionesDetalle] ([VersionesPrincipalId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[VersionesDetalle] ADD CONSTRAINT [FK_VersionesPrincipalVersionesDetalle] FOREIGN KEY ([VersionesPrincipalId]) REFERENCES [dbo].[VersionesPrincipal] ([Id])
GO
