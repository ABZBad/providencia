CREATE TABLE [dbo].[PermisosModuloAtributos]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[AtributoNombre] [nvarchar] (250) COLLATE Modern_Spanish_CI_AS NOT NULL,
[AtributoAccion] [nvarchar] (250) COLLATE Modern_Spanish_CI_AS NOT NULL,
[ModuloId] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PermisosModuloAtributos] ADD CONSTRAINT [PK_PermisosModuloAtributos] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_ModuloAtributos] ON [dbo].[PermisosModuloAtributos] ([ModuloId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PermisosModuloAtributos] ADD CONSTRAINT [FK_ModuloAtributos] FOREIGN KEY ([ModuloId]) REFERENCES [dbo].[PermisosModulos] ([Id])
GO
