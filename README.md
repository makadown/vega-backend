# VEGA BACKEND

Basado en el curso de mosh separado en 2 proyectos. 
Este proyecto es en ASPNET CORE 2.1 con visual studio code.

Guia de creación de backend desde cero gracias a https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-vsc
 y 
https://developer.okta.com/blog/2018/04/26/build-crud-app-aspnetcore-angular#configure-the-database-connection-on-startup

Este proyecto trabaja con Base de Datos en SQL Server. Por lo que si se usa, se tiene que tener instalado.

- Descargar e instalar apnetcore :
> https://www.microsoft.com/net/learn/get-started/windows

En caso de no tenerlo, crear la variable de entorno:
> SET ASPNETCORE_Environment=Development

## Creacion desde scratch

> dotnet new webapi -o vega-backend

## Paquetes instalados

> dotnet add package Microsoft.AspNetCore.Cors

> dotnet add package Microsoft.EntityFrameworkCore.SqlServer

> dotnet add package AutoMapper

> dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection

> dotnet restore          (Sólo si el VS Code se ofreció a -y no aceptaste- restaurar)


## Agregar migraciones en consola

Para iniciar mi desarrollo code-first o si quiero iniciar siembra (seed) de datos en mi db:

> dotnet ef migrations add modelName

> dotnet ef database update


el comando `database update` actualizara la base de datos de acuerdo con hasta la ultima migración.


Si quiero eliminar de mi BD mi unico estado de mi unica migracion que tengo (si solo tengo 1):

> dotnet ef database update 0

para despues 

> dotnet ef migrations remove

OJO: no es recomendable estar removeando cuando se tienen varias migraciones.


Para inicializar una BD Vacía desde una migracion de este proyecto (Ver nombre de clase en folder /Migrations)

> dotnet ef database update `nombreDeMigracion`

## Inicio de ejecucion de backend

Presionar F5 o teclear:

> dotnet run


## Cotorreo en uso de modelos

Lo que está en folder `Models` se utiliza unicamente como ORM para la BD. NO para usarse al consumir APIS. Para consumir APIS, se usan las clases contenidas en `Controllers\Resources`.  


## APIs creadas

(gracias a https://www.tablesgenerator.com/markdown_tables )
ej. 
http://localhost:5000/api/makes

|      API          | Descripción             | Request Body |             Response Body                  |
|-------------------|-------------------------|--------------|------------------------------------------- |
|  `/api/makes`     | Obtiene todos los makes |     None     | Colección de makes con detalle de models   |
|  `/api/models`    | Obtiene todos los models|     None     | Colección de models                        |
|  `/api/features`  | Obtiene características |     None     | Colección de features (características)    |
|                   |                         |              |                                            |
|                   |                         |              |                                            |
|                   |                         |              |                                            |













