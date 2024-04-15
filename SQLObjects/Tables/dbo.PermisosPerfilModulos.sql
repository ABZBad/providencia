CREATE TABLE [dbo].[PermisosPerfilModulos]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ModuloId] [int] NOT NULL,
[AtributoId] [int] NOT NULL,
[PerfilId] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PermisosPerfilModulos] ADD CONSTRAINT [PK_PermisosPerfilModulos] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_PerfilPerfilModulo] ON [dbo].[PermisosPerfilModulos] ([PerfilId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PermisosPerfilModulos] ADD CONSTRAINT [FK_PerfilPerfilModulo] FOREIGN KEY ([PerfilId]) REFERENCES [dbo].[PermisosPerfiles] ([Id])
GO
