CREATE TABLE [dbo].[PermisosGrupos]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[GrupoNombre] [nvarchar] (max) COLLATE Modern_Spanish_CI_AS NOT NULL,
[UsuarioId] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[PermisosGrupos] ADD CONSTRAINT [PK_PermisosGrupos] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_UsuarioGrupos] ON [dbo].[PermisosGrupos] ([UsuarioId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PermisosGrupos] ADD CONSTRAINT [FK_UsuarioGrupos] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[PermisosUsuarios] ([Id])
GO
