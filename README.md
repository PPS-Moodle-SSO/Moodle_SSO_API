# Moodle SSO API

## Descripción

**Moodle SSO API** es una API desarrollada como Práctica Profesional Supervisada por UTN. Consiste en una API REST que sirve como intermediaria entre una aplicación y una o varias instancias de Moodle, proporcionando un sistema de tipo **Single Sign-On (SSO)** que permite validar usuarios existentes en sistemas de Moodle.

Este proyecto fue desarrollado usando **ASP.NET Core API** en su versión 8.0.

## Características Principales

- ✅ Autenticación de usuarios mediante SSO con Moodle
- ✅ Soporte para múltiples instancias de Moodle (multi-tenant)
- ✅ Gestión de empresas/organizaciones con sus respectivas configuraciones de Moodle
- ✅ Integración con Moodle Web Services API
- ✅ Validación de usuarios por email
- ✅ Actualización de configuración de empresas
- ✅ Documentación automática con Swagger/OpenAPI

## Tecnologías Utilizadas

- **.NET 8.0** - Framework principal
- **ASP.NET Core Web API** - Framework web
- **Entity Framework Core 8.0** - ORM para acceso a datos
- **SQL Server** - Base de datos
- **AutoMapper** - Mapeo de objetos
- **Swagger/OpenAPI** - Documentación de API
- **Newtonsoft.Json** - Serialización JSON
- **HttpClient** - Comunicación con APIs externas

## Requisitos Previos

Antes de comenzar, asegúrate de tener instalado:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) (2019 o superior)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [Visual Studio Code](https://code.visualstudio.com/)
- SQL Server Management Studio (SSMS) o Azure Data Studio (opcional)

## Instalación y Configuración

### 1. Clonar el Repositorio

```bash
git clone https://github.com/tu-usuario/Moodle_SSO_API.git
cd Moodle_SSO_API
```

### 2. Creación de la Base de Datos

Dentro de la carpeta `Recursos` se encuentran dos scripts SQL:

1. **CreateDatabaseAndTables.sql**: Crea la base de datos y las tablas necesarias
2. **FillTables.sql**: Inserta datos de ejemplo (opcional)

**Pasos para crear la base de datos:**

1. Abre SQL Server Management Studio (SSMS) o tu herramienta SQL preferida
2. Conéctate a tu instancia de SQL Server
3. Ejecuta el script `Recursos/CreateDatabaseAndTables.sql`
4. (Opcional) Ejecuta el script `Recursos/FillTables.sql` para datos de ejemplo

### 3. Configuración de la Cadena de Conexión

Edita el archivo `Moodle_SSO_API/appsettings.json` y configura la cadena de conexión según tu entorno:

**Para conexión local:**
```json
"ConnectionStrings": {
  "ConnectionStrSQLServer": "Server=tcp:127.0.0.1,1433;Database=MoodleSSOTest;User ID=sa;Password=YourStrong!Passw0rd;Encrypt=False;TrustServerCertificate=True;Integrated Security=False;Authentication=SqlPassword;MultipleActiveResultSets=True"
}
```

**Para conexión remota:**
```json
"ConnectionStrings": {
  "ConnectionStrSQLServer": "Server=host,port;Initial Catalog=MoodleSSOTest;User ID=user;Password=password;TrustServerCertificate=True;MultipleActiveResultSets=True"
}
```

### 4. Configuración de Moodle

En el mismo archivo `appsettings.json`, configura los parámetros de Moodle:

```json
"Moodle": {
  "wsfunction": "core_user_get_users_by_field",
  "moodlewsrestformat": "json",
  "LocalUrl": "http://localhost:8888/moodle405",
  "CommonToken": "2f09224d4ee4dd75e1481ef30a0076a6"
}
```

**Parámetros:**
- `wsfunction`: Función del web service de Moodle a utilizar
- `moodlewsrestformat`: Formato de respuesta (json)
- `LocalUrl`: URL base de tu instancia de Moodle (opcional, se puede sobrescribir por empresa)
- `CommonToken`: Token común para pruebas (opcional, cada empresa tiene su propio token)

### 5. Ejecutar la Aplicación

**Opción 1: Desde Visual Studio**
1. Abre la solución `Moodle_SSO_API.sln`
2. Presiona `F5` o haz clic en "Ejecutar"

**Opción 2: Desde la Terminal**
```bash
cd Moodle_SSO_API
dotnet restore
dotnet run
```

La API estará disponible en:
- **HTTPS**: `https://localhost:7210`
- **HTTP**: `http://localhost:5000`

### 6. Acceder a la Documentación Swagger

Una vez que la aplicación esté ejecutándose, puedes acceder a la documentación interactiva de Swagger en:

- **Swagger UI**: `https://localhost:7210/swagger`

## Estructura del Proyecto

```
Moodle_SSO_API/
├── Controllers/              # Controladores de la API
│   ├── Enterprises/         # Endpoints para gestión de empresas
│   └── Moodle/              # Endpoints para operaciones con Moodle
├── Data/                    # Contexto de Entity Framework
│   └── AppDbContext.cs
├── Exceptions/              # Excepciones personalizadas
├── Handlers/                # Lógica de negocio
│   ├── Enterprises/
│   └── Moodles/
├── Helpers/                 # Utilidades y helpers
├── Models/                  # Modelos de dominio
├── Repository/              # Patrón Repository para acceso a datos
├── Services/                # Servicios externos (HTTP, Moodle)
└── Program.cs               # Punto de entrada de la aplicación
```

## Endpoints de la API

### Base URL
```
https://localhost:7210/api
```

### 1. Autenticar Usuario

Autentica un usuario en Moodle mediante su email.

**Endpoint:** `POST /api/moodle/authenticate`

**Request Body:**
```json
{
  "email": "usuario@ejemplo.com",
  "domain": "ejemplo.com"
}
```

**Response (200 OK):**
```json
{
  "statusCode": 200,
  "isSuccessful": true,
  "errorsMessage": null,
  "result": {
    "success": true,
    "token": "2f09224d4ee4dd75e1481ef30a0076a6",
    "privatetoken": "",
    "errorcode": "",
    "error": "",
    "userData": [
      {
        "id": 1,
        "username": "usuario",
        "firstname": "Juan",
        "lastname": "Pérez",
        "email": "usuario@ejemplo.com",
        "country": "AR"
      }
    ]
  }
}
```

**Response (400 Bad Request) - Empresa no encontrada:**
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Enterprise not found": ["Enterprise not found for the provided domain"]
  }
}
```

**Response (401 Unauthorized) - Autenticación fallida:**
```json
{
  "type": "https://tools.ietf.org/html/rfc7235#section-3.1",
  "title": "Unauthorized",
  "status": 401,
  "errors": {
    "Authentication failed": ["Invalid credentials"]
  }
}
```

**Códigos de Estado:**
- `200 OK`: Autenticación exitosa
- `400 Bad Request`: Datos de entrada inválidos o empresa no encontrada para el dominio proporcionado
- `401 Unauthorized`: Credenciales inválidas o usuario no encontrado en Moodle
- `500 Internal Server Error`: Error interno del servidor

---

### 2. Obtener Usuario

Obtiene la información de un usuario de Moodle por su email.

**Endpoint:** `POST /api/moodle/get-user`

**Request Body:**
```json
{
  "userEmail": "usuario@ejemplo.com",
  "domain": "ejemplo.com"
}
```

**Response (200 OK):**
```json
{
  "statusCode": 200,
  "isSuccessful": true,
  "errorsMessage": null,
  "result": {
    "id": 1,
    "username": "usuario",
    "firstname": "Juan",
    "lastname": "Pérez",
    "email": "usuario@ejemplo.com",
    "country": "AR"
  }
}
```

**Response (400 Bad Request) - Empresa no encontrada:**
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Enterprise not found": ["Enterprise not found for the provided domain"]
  }
}
```

**Response (404 Not Found) - Usuario no encontrado:**
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
  "title": "Not Found",
  "status": 404,
  "errors": {
    "User not found": ["User not found for the provided email"]
  }
}
```

**Códigos de Estado:**
- `200 OK`: Usuario encontrado
- `400 Bad Request`: Datos de entrada inválidos o empresa no encontrada para el dominio proporcionado
- `404 Not Found`: Usuario no encontrado en Moodle
- `500 Internal Server Error`: Error interno del servidor

---

### 3. Actualizar Empresa

Actualiza la configuración de una empresa. Solo actualiza los campos `moodleUrl` y `moodleApiKey`. Los demás campos de la empresa (nombre, dominio, etc.) no se modifican.

**Endpoint:** `POST /api/enterprise/update`

**Request Body:**
```json
{
  "idEnterprise": 1,
  "moodleUrl": "https://moodle.ejemplo.com",
  "moodleApiKey": "nuevo_token_api_key"
}
```

**Response (200 OK):**
```json
{
  "statusCode": 200,
  "isSuccessful": true,
  "errorsMessage": null,
  "result": {
    "idEnterprise": 1
  }
}
```

**Nota:** Este endpoint solo actualiza los campos `moodleUrl` y `moodleApiKey` de la empresa. La respuesta solo devuelve el `idEnterprise` de la empresa actualizada.

**Response (404 Not Found):**
```json
{
  "statusCode": 404,
  "isSuccessful": false,
  "errorsMessage": ["Enterprise not found"],
  "result": null
}
```

**Códigos de Estado:**
- `200 OK`: Empresa actualizada exitosamente
- `400 Bad Request`: Datos de entrada inválidos
- `404 Not Found`: Empresa no encontrada
- `500 Internal Server Error`: Error interno del servidor

## Modelos de Datos

### Enterprise (Empresa)

Representa una empresa/organización con su configuración de Moodle.

```csharp
{
  "idEnterprise": int,              // ID único (auto-generado)
  "nameEnterprise": string,         // Nombre de la empresa
  "idOrganizationDefault": string, // ID de organización por defecto
  "domain": string,                 // Dominio de la empresa (usado para identificación)
  "moodleUrl": string,              // URL de la instancia de Moodle
  "moodleApiKey": string            // API Key de Moodle para esta empresa
}
```

### APIResponse<T>

Respuesta estándar de la API.

```csharp
{
  "statusCode": HttpStatusCode,    // Código de estado HTTP
  "isSuccessful": bool,             // Indica si la operación fue exitosa
  "errorsMessage": List<string>?,   // Lista de mensajes de error (si los hay)
  "result": T?                      // Resultado de la operación (genérico)
}
```

### GetUserResponse

Información de un usuario de Moodle.

```csharp
{
  "id": int,           // ID del usuario en Moodle
  "username": string,  // Nombre de usuario
  "firstname": string, // Nombre
  "lastname": string,  // Apellido
  "email": string,     // Email
  "country": string    // Código de país
}
```

### AuthenticateResponse

Respuesta de autenticación.

```csharp
{
  "success": bool,                    // Indica si la autenticación fue exitosa
  "token": string,                    // Token de autenticación
  "privatetoken": string,             // Token privado (si aplica)
  "errorcode": string,                // Código de error (si hay error)
  "error": string,                    // Mensaje de error (si hay error)
  "userData": List<GetUserResponse>?  // Datos del usuario autenticado
}
```

## Configuración de Moodle

Para que esta API funcione correctamente, necesitas configurar los Web Services en Moodle siguiendo estos pasos:

### Paso 1: Habilitar Protocolo REST

1. Ve a: **Site administration > Server > Manage protocols**
2. Asegúrate de que el protocolo **REST** esté activado/habilitado
3. Si no está habilitado, actívalo haciendo clic en el icono del ojo o el enlace correspondiente

### Paso 2: Configurar External Services (Servicios Externos)

1. Ve a: **Site administration > Server > External services**
2. En la sección **Custom services**, puedes crear un nuevo servicio personalizado o usar uno existente
3. Si creas un nuevo servicio:
   - Haz clic en "Add" o "Create service"
   - Asigna un nombre (ej: "test" o "SSO API Service")
   - Configura los usuarios autorizados (puede ser "All users" o "Authorised users")
4. Si usas un servicio existente, haz clic en "Edit" para configurarlo

### Paso 3: Agregar Funciones al Servicio

1. En la página del servicio (External services), haz clic en el enlace **"Functions"** del servicio que vas a usar
2. Haz clic en **"Add functions"** para agregar las funciones necesarias
3. Agrega las siguientes funciones (puedes buscarlas por nombre):
   - **`core_user_get_users_by_field`**: Función principal utilizada por la API para obtener usuarios por email
     - *Descripción*: "Retrieve users' information for a specified unique field"
     - *Capabilities requeridas*: `moodle/user:viewdetails`, `moodle/user:viewhiddendetails`, `moodle/course:useremail`, `moodle/user:update`

### Paso 4: Crear Token de API

1. Ve a: **Site administration > Server > Manage tokens**
2. Haz clic en el botón **"Create token"**
3. Completa el formulario:
   - **Name**: Asigna un nombre descriptivo (ej: "testtoken" o "SSO API Token")
   - **User**: Selecciona el usuario de Moodle que usará este token (recomendado: crear un usuario específico para la API)
   - **Service**: Selecciona el servicio personalizado que creaste en el Paso 2 (ej: "test")
   - **IP restriction** (opcional): Puedes restringir el uso del token a IPs específicas
   - **Valid until** (opcional): Puedes establecer una fecha de expiración
4. Haz clic en **"Save changes"**
5. **IMPORTANTE**: Copia el token generado inmediatamente, ya que no podrás verlo nuevamente después de cerrar la página

### Paso 5: Verificar Permisos del Usuario

1. En la página "Manage tokens", verifica la columna **"Missing capabilities"** del token creado
2. Si aparecen capabilities faltantes, necesitas otorgar esos permisos al usuario:
   - `moodle/user:viewdetails` - Ver perfiles de usuario
   - `moodle/user:viewhiddendetails` - Ver detalles ocultos de usuarios
   - `moodle/course:useremail` - Para acceder a la dirección de email de los usuarios
   - `moodle/user:update` - Actualizar perfiles de usuario
3. Para otorgar permisos:
   - Ve a: **Site administration > Users > Permissions > Define roles**
   - Edita el rol del usuario o asigna un rol con estos permisos
   - O ve a: **Site administration > Users > Permissions > Assign system roles** y asigna un rol apropiado al usuario

### Paso 6: Agregar la Empresa en la Base de Datos

Una vez que tengas el token, agrega la empresa en la base de datos:

**Opción A: Usando SQL directamente**
```sql
INSERT INTO Enterprise (nameEnterprise, idOrganizationDefault, domain, moodleUrl, moodleApiKey)
VALUES ('Nombre de la Empresa', 'org-default-id', 'ejemplo.com', 'https://moodle.ejemplo.com', 'token_generado_en_paso_4');
```

**Opción B: Usando el endpoint de la API** (si ya tienes una empresa creada)
```bash
POST /api/enterprise/update
{
  "idEnterprise": 1,
  "moodleUrl": "https://moodle.ejemplo.com",
  "moodleApiKey": "token_generado_en_paso_4"
}
```

**Campos requeridos:**
- `nameEnterprise`: Nombre de la empresa
- `idOrganizationDefault`: ID de organización por defecto (puede ser NULL)
- `domain`: Dominio de la empresa (ej: "ejemplo.com") - **Este es el valor que se usará en las peticiones a la API**
- `moodleUrl`: URL completa de Moodle sin barra final (ej: "https://moodle.ejemplo.com")
- `moodleApiKey`: Token generado en el Paso 4

### Notas Importantes

- El **domain** debe coincidir exactamente con el valor que envíes en las peticiones a la API
- Asegúrate de que el usuario asociado al token tenga todos los permisos necesarios
- La URL de Moodle debe ser accesible desde el servidor donde corre la API
- Si cambias el token, actualiza el campo `moodleApiKey` en la base de datos usando el endpoint `/api/enterprise/update`

## Ejemplos de Uso

### Ejemplo 1: Autenticar un Usuario

```bash
curl -X POST "https://localhost:7210/api/moodle/authenticate" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "usuario@ejemplo.com",
    "domain": "ejemplo.com"
  }'
```

### Ejemplo 2: Obtener Información de Usuario

```bash
curl -X POST "https://localhost:7210/api/moodle/get-user" \
  -H "Content-Type: application/json" \
  -d '{
    "userEmail": "usuario@ejemplo.com",
    "domain": "ejemplo.com"
  }'
```

### Ejemplo 3: Actualizar Configuración de Empresa

```bash
curl -X POST "https://localhost:7210/api/enterprise/update" \
  -H "Content-Type: application/json" \
  -d '{
    "idEnterprise": 1,
    "moodleUrl": "https://nuevo-moodle.ejemplo.com",
    "moodleApiKey": "nuevo_token_12345"
  }'
```

### Ejemplo con JavaScript (Fetch API)

```javascript
// Autenticar usuario
async function authenticateUser(email, domain) {
  const response = await fetch('https://localhost:7210/api/moodle/authenticate', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      email: email,
      domain: domain
    })
  });
  
  const data = await response.json();
  return data;
}
```

## Arquitectura

El proyecto sigue una arquitectura en capas:

1. **Controllers**: Manejan las peticiones HTTP y validan los datos de entrada
2. **Handlers**: Contienen la lógica de negocio
3. **Services**: Gestionan la comunicación con servicios externos (Moodle)
4. **Repository**: Abstrae el acceso a la base de datos
5. **Models**: Representan las entidades del dominio

### Flujo de Autenticación

```
Cliente → Controller → Handler → Service → Moodle API
                ↓
         Repository (DB)
```

1. El cliente envía una petición con email y domain
2. El Controller valida los datos
3. El Handler busca la empresa por domain en la base de datos
4. El Service realiza la petición a Moodle usando la URL y API Key de la empresa
5. Se retorna la respuesta al cliente

## Excepciones Personalizadas

El proyecto incluye las siguientes excepciones personalizadas:

- `EnterpriseNotFoundException`: Se lanza cuando no se encuentra una empresa
- `InvalidEnterpriseDataException`: Se lanza cuando los datos de la empresa son inválidos
- `InvalidTokenException`: Se lanza cuando el token es inválido
- `UserNotFoundException`: Se lanza cuando no se encuentra un usuario

## CORS

La API está configurada para permitir peticiones desde cualquier origen en modo desarrollo. Para producción, se recomienda configurar orígenes específicos en `Program.cs`.

## Logging

El proyecto utiliza el sistema de logging integrado de ASP.NET Core. Los logs se pueden configurar en `appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## Desarrollo

### Estructura de Carpetas

- **Controllers/**: Controladores REST
- **Handlers/**: Lógica de negocio
- **Services/**: Servicios externos
- **Repository/**: Acceso a datos
- **Models/**: Entidades del dominio
- **Exceptions/**: Excepciones personalizadas
- **Helpers/**: Utilidades

### Patrones Utilizados

- **Repository Pattern**: Para abstraer el acceso a datos
- **Dependency Injection**: Para la inyección de dependencias
- **DTO Pattern**: Para transferir datos entre capas
- **AutoMapper**: Para mapeo automático de objetos

## Troubleshooting

### Error: "Enterprise not found"
- Verifica que la empresa esté registrada en la base de datos
- Asegúrate de que el `domain` coincida exactamente con el registrado

### Error: "User not found"
- Verifica que el usuario exista en Moodle
- Verifica que el email sea correcto
- Verifica que el token de API tenga permisos suficientes

### Error de Conexión a Base de Datos
- Verifica la cadena de conexión en `appsettings.json`
- Asegúrate de que SQL Server esté ejecutándose
- Verifica que la base de datos exista

### Error de Conexión a Moodle
- Verifica que la URL de Moodle sea accesible
- Verifica que el token de API sea válido
- Verifica que los Web Services estén habilitados en Moodle
