CREATE TABLE [dbo].[PermisosUsuarioEspeciales]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ModuloId] [int] NOT NULL,
[AtributoId] [int] NOT NULL,
[UsuarioId] [int] NOT NULL,
[Modulo_Id] [int] NOT NULL,
[ModuloAtributo_Id] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PermisosUsuarioEspeciales] ADD CONSTRAINT [PK_PermisosUsuarioEspeciales] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_usuarioEspecialModulo] ON [dbo].[PermisosUsuarioEspeciales] ([Modulo_Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_usuarioEspecialModuloAtributo] ON [dbo].[PermisosUsuarioEspeciales] ([ModuloAtributo_Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_UsuarioEspeciales] ON [dbo].[PermisosUsuarioEspeciales] ([UsuarioId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PermisosUsuarioEspeciales] ADD CONSTRAINT [FK_usuarioEspecialModulo] FOREIGN KEY ([Modulo_Id]) REFERENCES [dbo].[PermisosModulos] ([Id])
GO
ALTER TABLE [dbo].[PermisosUsuarioEspeciales] ADD CONSTRAINT [FK_usuarioEspecialModuloAtributo] FOREIGN KEY ([ModuloAtributo_Id]) REFERENCES [dbo].[PermisosModuloAtributos] ([Id])
GO
ALTER TABLE [dbo].[PermisosUsuarioEspeciales] ADD CONSTRAINT [FK_UsuarioEspeciales] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[PermisosUsuarios] ([Id])
GO
