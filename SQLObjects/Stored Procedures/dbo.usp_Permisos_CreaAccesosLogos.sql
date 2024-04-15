SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
create procedure [dbo].[usp_Permisos_CreaAccesosLogos]
as

	insert into PermisosModuloAtributos (AtributoNombre,AtributoAccion,ModuloId) values ('Logos: Agregar nuevos','Click',6)	
	insert into PermisosModuloAtributos (AtributoNombre,AtributoAccion,ModuloId) values ('Logos: Cambiar Imagen','Click',6)
	insert into PermisosModuloAtributos (AtributoNombre,AtributoAccion,ModuloId) values ('Logos: Salvar','Click',6)
	insert into PermisosModuloAtributos (AtributoNombre,AtributoAccion,ModuloId) values ('Logos: Borrar','Click',6)	
	insert into PermisosModuloAtributos (AtributoNombre,AtributoAccion,ModuloId) values ('Logos: Imprimir','Click',6)

GO
