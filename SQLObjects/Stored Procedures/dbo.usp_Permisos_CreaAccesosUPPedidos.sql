SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
create procedure [dbo].[usp_Permisos_CreaAccesosUPPedidos]
as

	insert into PermisosModuloAtributos (AtributoNombre,AtributoAccion,ModuloId) values ('Desempeño de Areas','Click',54)	
	insert into PermisosModuloAtributos (AtributoNombre,AtributoAccion,ModuloId) values ('Fecha entregado','Click',54)
	

GO
