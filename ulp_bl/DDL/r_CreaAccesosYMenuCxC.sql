/*
Script para la generación del Menú y del permiso del módulo:

"Captura de facturas de proveedores de Fletes" solicitado por Rubén Alamán



Inserto el nuevo elemento del menú...
*/
INSERT INTO PermisosModulos
           (
           [Descripcion]
           ,[MenuOrigen]
           ,[OrdenMenu]
           ,[PuedeEntrar]
           ,[PuedeInsertar]
           ,[PuedeModificar]
           ,[PuedeBorrar]
           ,[GrupoId]
           )
     VALUES
           (
           'Ajuste de CxC (Aspel-SAE/SIP)'
           ,81
           ,3
           ,1
           ,0
           ,0
           ,0
           ,2
           )

/*
Inserta el registro necesario para administrar y asignar permisos a los usuarios
*/
INSERT INTO PermisosModuloAtributos
           (
           [AtributoNombre]
           ,[AtributoAccion]
           ,[ModuloId]
           )
     VALUES
           (
           'Puede Entrar'
           ,'Puede_Entrar'
           ,@@identity
           )


