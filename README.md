# VEGA BACKEND

Basado en el curso de mosh separado en 2 proyectos. 
Este proyecto es en ASPNET CORE 2.1 con visual studio code.

Guia de creación de backend desde cero gracias a https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-vsc
 y 
https://developer.okta.com/blog/2018/04/26/build-crud-app-aspnetcore-angular#configure-the-database-connection-on-startup

# Paquetes instalados

> dotnet add package Microsoft.EntityFrameworkCore.SqlServer

# Agregar migraciones en consola

> dotnet ef migrations add modelName

> dotnet ef database update