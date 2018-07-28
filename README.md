# VEGA BACKEND

Basado en el curso de mosh separado en 2 proyectos. 
Este proyecto es en ASPNET CORE 2.1 con visual studio code.

Guia de creación de backend desde cero gracias a https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-vsc
 y 
https://developer.okta.com/blog/2018/04/26/build-crud-app-aspnetcore-angular#configure-the-database-connection-on-startup

Este proyecto trabaja con Base de Datos en SQL Server. Por lo que si se usa, se tiene que tener instalado.

# Paquetes instalados

> dotnet add package Microsoft.EntityFrameworkCore.SqlServer

# Agregar migraciones en consola

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


