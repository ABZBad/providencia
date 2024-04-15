CREATE TABLE [dbo].[PermisosUsuarios]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[UsuarioUsuario] [nvarchar] (250) COLLATE Modern_Spanish_CI_AS NOT NULL,
[UsuarioNombre] [nvarchar] (300) COLLATE Modern_Spanish_CI_AS NOT NULL,
[UsuarioContraseña] [nvarchar] (250) COLLATE Modern_Spanish_CI_AS NOT NULL,
[UsuarioCorreo] [nvarchar] (300) COLLATE Modern_Spanish_CI_AS NOT NULL,
[UsuarioStatus] [bit] NOT NULL,
[UsuarioFechaIngreso] [datetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PermisosUsuarios] ADD CONSTRAINT [PK_PermisosUsuarios] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
