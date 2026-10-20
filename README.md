# Berum Web API + Frontend

Proyecto fullstack compuesto por una API REST en **.NET 10.0** con **SQL Server** como base de datos (alojada en la nube), y un frontend en **React**.

---

# Gestión de Servicio Técnico

Sistema web integral para la digitalización y seguimiento de reparaciones informáticas. Diseñado para eliminar el desorden de las anotaciones informales y automatizar la comunicación con el cliente.

### El Problema
Los procesos manuales y la falta de trazabilidad generan cuellos de botella operativos y una comunicación deficiente (consultas constantes vía WhatsApp/llamadas) sobre el estado de los equipos.

### Solución y Funcionalidades
* **Gestión de Órdenes:** Registro de ingreso, diagnóstico, reparación y entrega.
* **Tracking Público:** Portal de consulta para clientes mediante **código único**.
* **Historial de Trabajo:** Trazabilidad completa de intervenciones por equipo.
* **Escalabilidad:** Arquitectura preparada para gestión de stock y tarifarios.

### Stack Tecnológico
* **Frontend:** React
* **Backend:** .NET (C#)
* **Base de Datos:** SQL Server

## 📋 Requisitos previos

Antes de levantar el proyecto, asegurate de tener instalado lo siguiente:

| Herramienta | Versión recomendada | Descarga |
|---|---|---|
| .NET SDK | 10.0 o superior | https://dotnet.microsoft.com/download |
| Node.js | 18.x o superior | https://nodejs.org |
| Visual Studio Code | Última versión | https://code.visualstudio.com |
| Git | Última versión | https://git-scm.com |

> ℹ️ **No es necesario instalar SQL Server localmente.** La base de datos está alojada en la nube en **Somee.com** y se accede mediante la cadena de conexión.

### Extensiones recomendadas de VS Code

- **C# Dev Kit** (ms-dotnettools.csdevkit)
- **ES7+ React/Redux/React-Native snippets**
- **Prettier - Code formatter**

---

## 📁 Estructura del proyecto

```
/
├── Berum_WebAPI/          # Backend - API REST en .NET 10.0
│   ├── Controllers/
│   ├── DTOs/
│   ├── Models/
│   ├── Services/
│   ├── appsettings.json
│   └── Program.cs
│
└── berum-front/           # Frontend - React
    ├── src/
    │   ├── assets/
    │   ├── componentes/
    │   ├── context/
    │   ├── features/
    │   ├── router/
    │   └── types/
    ├── public/
    └── package.json
```

---

## ⚙️ Configuración del Backend (`Berum_WebAPI`)

### 1. Clonar el repositorio

```bash
git clone <URL_DEL_REPOSITORIO>
cd <NOMBRE_DEL_REPOSITORIO>
```

### 2. Configurar la cadena de conexión

La base de datos está alojada en la nube en **Somee.com** (SQL Server). No es necesario instalar ni configurar SQL Server localmente.

Abrí el archivo `Berum_WebAPI/appsettings.json` y reemplazá la sección `ConnectionStrings` con las credenciales del proyecto:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "workstation id=NOMBRE_BD.mssql.somee.com;packet size=4096;user id=USUARIO;pwd=CONTRASEÑA;data source=NOMBRE_BD.mssql.somee.com;persist security info=False;initial catalog=NOMBRE_BD;TrustServerCertificate=True;"
  }
}
```

> ⚠️ Reemplazá `NOMBRE_BD`, `USUARIO` y `CONTRASEÑA` con las credenciales reales. Estos datos los provee el equipo de desarrollo y **no deben subirse al repositorio**.

### 3. Aplicar las migraciones con Entity Framework Core

El proyecto usa **Entity Framework Core** para gestionar el esquema de la base de datos. Una vez configurada la connection string, ejecutá desde la carpeta `Berum_WebAPI/`:

```bash
cd Berum_WebAPI
dotnet ef database update
```

Esto aplicará todas las migraciones existentes sobre la base de datos remota en Somee.

> Si `dotnet ef` no está instalado, ejecutá primero:
> ```bash
> dotnet tool install --global dotnet-ef
> ```

### 4. Restaurar dependencias y ejecutar el backend

```bash
dotnet restore
dotnet run --launch-profile https
```

La API estará disponible en `https://localhost:5001` (o el puerto que indique la consola).

---

## 🌐 Configuración del Frontend (`berum-front`)

### 1. Instalar dependencias

```bash
cd berum-front
npm install
```

### 2. Configurar la URL de la API

Creá un archivo `.env` en la raíz de `berum-front/` con el siguiente contenido:

```env
REACT_APP_API_URL=https://localhost:5001
```

> Ajustá el puerto según el que use tu backend al iniciarse.

### 3. Ejecutar el frontend

```bash
npm start -- --https
```

La aplicación estará disponible en `https://localhost:3000`.

---

## ▶️ Levantar el proyecto completo

Para tener el entorno completo funcionando, necesitás tener **dos terminales abiertas en paralelo**, ambas ejecutando el perfil **HTTPS**:

**Terminal 1 — Backend:**
```bash
cd Berum_WebAPI
dotnet run --launch-profile https
```

**Terminal 2 — Frontend:**
```bash
cd berum-front
npm start -- --https
```

> Ambos proyectos deben estar corriendo al mismo tiempo para que la aplicación funcione correctamente.

---

## 🗃️ Base de datos

- Motor: **SQL Server**
- Hosting: **Somee.com** (acceso remoto, no requiere instalación local)
- Las migraciones se gestionan con **Entity Framework Core**
- Para crear una nueva migración (solo en desarrollo):

```bash
cd Berum_WebAPI
dotnet ef migrations add NombreDeLaMigracion
dotnet ef database update
```

---

## ❓ Problemas frecuentes

**Error de certificado SSL al conectar con Somee**
→ Asegurate de tener `TrustServerCertificate=True` en la cadena de conexión.

**`dotnet ef` no se reconoce como comando**
→ Instalalo con: `dotnet tool install --global dotnet-ef` y reiniciá la terminal.

**Puerto 5001 o 3000 ya en uso**
→ Cambiá el puerto en `Properties/launchSettings.json` (backend) o usá `PORT=3001 npm start` (frontend).

**Error de CORS en el frontend**
→ Verificá que la URL configurada en `.env` coincida exactamente con la que usa el backend.

**No se puede conectar a la base de datos**
→ Verificá que las credenciales en `appsettings.json` sean correctas y que tu conexión a internet esté activa.
