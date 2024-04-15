CREATE TABLE [dbo].[PermisosPerfiles]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[PerfilNombre] [nvarchar] (max) COLLATE Modern_Spanish_CI_AS NOT NULL,
[PerfilDescripcion] [nvarchar] (max) COLLATE Modern_Spanish_CI_AS NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[PermisosPerfiles] ADD CONSTRAINT [PK_PermisosPerfiles] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
