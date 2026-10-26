<div align="center">
  <h1 align="center">Berum: Gestión de Servicio Técnico</h1>

  <p align="center">
       <img src="https://img.shields.io/badge/React-%2320232a.svg?style=for-the-badge&logo=react&logoColor=%2361DAFB" alt="React" />
       <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET" />
       <img src="https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white" alt="Docker" />
       <img src="https://img.shields.io/badge/AWS%20Amplify-FF9900?style=for-the-badge&logo=aws-amplify&logoColor=white" alt="AWS Amplify" />
       <img src="https://img.shields.io/badge/Amazon%20EC2-FF9900?style=for-the-badge&logo=amazonec2&logoColor=white" alt="Amazon EC2" />
       <img src="https://img.shields.io/badge/Amazon%20DynamoDB-4053B8?style=for-the-badge&logo=amazondynamodb&logoColor=white" alt="Amazon DynamoDB" />
       <img src="https://img.shields.io/badge/AWS%20Lambda-FF9900?style=for-the-badge&logo=awslambda&logoColor=white" alt="AWS Lambda" />
  </p>
</div>

<p>
  Sistema web integral para la digitalización, seguimiento y trazabilidad de reparaciones informáticas, diseñado para optimizar el flujo operativo y automatizar la     comunicación con los clientes.
</p>

## 1. Arquitectura de la Solución

<p>
    El sistema implementa una arquitectura desacoplada basada en servicios administrados de AWS y contenedores, estructurada en tres capas principales:
</p>

* Capa de Presentación: Aplicación web desarrollada en React y alojada en Amazon Amplify, con integración continua (CI/CD) desde el repositorio de código.
* Capa de Lógica de Negocio: API REST desarrollada en .NET (C#) y ejecutada dentro de un contenedor Docker sobre una instancia Ubuntu en Amazon EC2. La infraestructura de red utiliza una IP Elástica para garantizar una ruta de comunicación persistente con el frontend.
* Capa de Persistencia: Base de datos NoSQL Amazon DynamoDB, integrada mediante roles IAM asociados a la instancia EC2.

### Flujo de Datos y Operación

* Presentación: El usuario interactúa con la interfaz web alojada en Amazon Amplify.
* Enrutamiento: Las peticiones HTTP se dirigen a través del Internet Gateway hacia la instancia EC2 utilizando la IP Elástica configurada.
* Procesamiento: Las solicitudes son procesadas por la API .NET ejecutándose en un contenedor Docker en modo Release.
* Persistencia: La API se comunica con Amazon DynamoDB mediante credenciales temporales basadas en roles IAM.
* Optimización Operativa: Una función de AWS Lambda automatiza el ciclo de vida (encendido y apagado) de la instancia EC2 para la gestión de recursos.

## 2. Componentes de la Infraestructura

| Capa | Servicio AWS | Implementación y Justificación |
| :--- | :--- | :--- |
| **Presentación** | **Amazon Amplify** | Alojamiento serverless de la SPA en React, garantizando alta disponibilidad sin gestión de servidores web. |
| **Cómputo / API** | **Amazon EC2 + Docker** | Servidor virtual Ubuntu ejecutando la API .NET containerizada para garantizar portabilidad local/nube y control del entorno. |
| **Redes y Seguridad** | **Elastic IP & Security Groups** | IP fija permanente para el frontend y reglas de firewall estrictas (apertura exclusiva de SSH y puerto 8080). |
| **Acceso a Datos** | **AWS IAM Roles** | Autenticación nativa basada en roles para autorizar las operaciones del SDK de DynamoDB (`AWSSDK.DynamoDBv2`). |
| **Base de Datos** | **Amazon DynamoDB** | Persistencia NoSQL clave-valor administrada para almacenar órdenes, clientes y técnicos con consultas de ultra baja latencia. |
| **Automatización** | **AWS Lambda** | Script serverless administrativo para la gestión del ciclo de vida de la instancia EC2 y control presupuestario. |

## 3. Funcionalidades del Sistema

* Gestión de Órdenes: Registro y seguimiento del ciclo de vida del equipo (ingreso, diagnóstico, reparación y entrega).
* Tracking Público: Módulo de consulta para clientes mediante un código de seguimiento único, sin requerir autenticación.
* Historial Técnico: Trazabilidad completa de intervenciones pasadas asociadas a cada equipo.
* Escalabilidad: Estructura preparada para la futura integración de módulos de stock y tarifarios.
