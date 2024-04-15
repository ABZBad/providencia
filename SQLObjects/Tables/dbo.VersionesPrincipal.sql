CREATE TABLE [dbo].[VersionesPrincipal]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[VersionesVersion] [nvarchar] (max) COLLATE Modern_Spanish_CI_AS NOT NULL,
[VersionFecha] [datetime] NOT NULL,
[VersionUsuario] [nvarchar] (max) COLLATE Modern_Spanish_CI_AS NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[VersionesPrincipal] ADD CONSTRAINT [PK_VersionesPrincipal] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
