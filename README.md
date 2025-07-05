# Moodle_SSO
Práctica Profesional Supervisada por UTN. Consiste en una API que sirva como intermediaria entre una aplicación y una o varias instancias de Moodle con la idea de proporcionar un sistema del tipo Single Sign On proporcionando una validación para usuario existentes en un sistema de Moodle.

Este fue desarrollado usando ASP.NET Core API en su versión 8.

## Cómo configurar el proyecto en su equipo
Se debe clonar o descargar el proyecto desde [Github](https://github.com/...). 

## Creación de la base de datos:
Dentro de la carpeta "Recursos" se encuentran dos scripts SQL. 
Para crear la base de datos y las tablas se debe ejecutar dentro de SQLServer el archivo "CreateDatabaseAndTables.sql".
Una vez creada la base de datos y las tablas, se procede a ejecutar el archivo "FillTables.sql", ...

## Conexión de la solución con la base de datos SQLServer:
Dentro del archivo appsettings.json se encuentra la cadena de conexión (ConnectionStrings) para la base de datos. Escoja una de las dos dependiendo de si su base de datos se encuentra en la maquina local o si se encuentra en un servidor remoto, seteando los valores correspondientes de su Server, User ID y Password.

## Levantar el servidor de .NET:

Una vez se tenga el proyecto a disposición, para iniciar el servidor de .NET se debe abrir haciendo click en la solución del proyecto "Moodle_SSO_API.sln" y haciendo click en ejecutar la solución una vez abierta. El servidor correrá en localhost:7210.
## Endpoints
La aplicación hace uso de N endpoints en total:
- https://localhost:7210/api/...
