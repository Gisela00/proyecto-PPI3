# Comandos Docker

## 1. Ciclo de Vida del Contenedor (Reconstruir y Actualizar)

Si realizaste cambios en el código (como en `Program.cs` o los servicios) y necesitas actualizar el contenedor con la nueva versión:

* Detiene el contenedor de pruebas actual (si está corriendo)
docker stop berum-api-test

* Elimina el contenedor detenido para liberar el nombre
docker rm berum-api-test

* Compila una nueva imagen de Docker usando el Dockerfile en modo Release
docker build -t berum-webapi .

* Levanta un nuevo contenedor en segundo plano (-d), mapeando el puerto 8080
docker run -d -p 8080:8080 --name berum-api-test berum-webapi

## Otros comandos útiles

* Ver los contenedores activos actualmente (comprobar que esté 'Up')
docker ps

* Ver los logs en tiempo real de la aplicación (ideal para depurar errores o ver peticiones)
docker logs -f berum-api-test

* Detener el contenedor sin borrarlo
docker stop berum-api-test

* Iniciar nuevamente un contenedor que ya fue creado pero está detenido
docker start berum-api-test