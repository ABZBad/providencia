CREATE TABLE [dbo].[PermisosModulos]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Descripcion] [varchar] (100) COLLATE Modern_Spanish_CI_AS NOT NULL,
[MenuOrigen] [numeric] (18, 0) NOT NULL,
[OrdenMenu] [numeric] (18, 0) NOT NULL,
[PuedeEntrar] [bit] NOT NULL CONSTRAINT [DF_PermisosModulos_PuedeEntrar] DEFAULT ((0)),
[PuedeInsertar] [bit] NOT NULL CONSTRAINT [DF_PermisosModulos_PuedeInsertar] DEFAULT ((0)),
[PuedeModificar] [bit] NOT NULL CONSTRAINT [DF_PermisosModulos_PuedeModificar] DEFAULT ((0)),
[PuedeBorrar] [bit] NOT NULL CONSTRAINT [DF_PermisosModulos_PuedeBorrar] DEFAULT ((0)),
[GrupoId] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PermisosModulos] ADD CONSTRAINT [PK_PermisosModulos] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_GrupoModulo] ON [dbo].[PermisosModulos] ([GrupoId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PermisosModulos] ADD CONSTRAINT [FK_GrupoModulo] FOREIGN KEY ([GrupoId]) REFERENCES [dbo].[PermisosGrupos] ([Id])
GO
