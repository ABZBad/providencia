CREATE TABLE [dbo].[PermisosUsuarioPerfiles]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[PerfilId] [int] NOT NULL,
[UsuarioId] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PermisosUsuarioPerfiles] ADD CONSTRAINT [PK_PermisosUsuarioPerfiles] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
