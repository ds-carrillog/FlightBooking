# Sistema de Reservas de Vuelos

Este proyecto es un sistema de reservas de vuelos que permite a los usuarios realizar búsquedas de vuelos disponibles, hacer reservas, y consultar estadísticas de las aerolíneas más reservadas. El proyecto está dividido en dos partes principales: un frontend desarrollado en React y un backend desarrollado en .NET.

## Descripción

El sistema ofrece las siguientes funcionalidades:
- Registrar vuelos (con origen, destino, fechas, aerolínea y precio).
- Buscar vuelos disponibles con filtros por origen, destino y fecha.
- Realizar reservas seleccionando un vuelo disponible.
- Consultar estadísticas sobre las aerolíneas más reservadas y el número total de aerolíneas registradas.

## Tecnologías usadas
- Frontend: React
- Backend: .NET Core con SQLite
- Despliegue:
  - Frontend desplegado en Vercel.
  - Backend desplegado en Render.

## Backend
1. Clonar el repositorio:

```bash
git clone https://github.com/ds-carrillog/FlightBooking.git
```

2. Navegar a la carpeta del backend:

```bash
cd FlightBooking
```
3. Configurar la base de datos:

El proyecto utiliza SQLite como base de datos. Asegurarse de que el archivo de la base de datos esté correctamente configurado en la conexión.

4. Correr las migraciones de la base de datos:
```bash
dotnet ef database update
```

5. Iniciar el servidor:
```bash
dotnet run
```
El backend estará corriendo en http://localhost:8080.

## Frontend

1. Navegar a la carpeta del frontend:
```bash
cd frontend
```

2. Crear el archivo .env en la carpeta del frontend y agregar las variables de entorno:
```bash
REACT_APP_API_URL=https://flightbooking-1.onrender.com
```

3. Instalar las dependencias:
```bash
npm install
```

4. Iniciar el frontend:
```bash
npm start
```

El frontend estará disponible en http://localhost:3000.

## Despliegue en producción
El proyecto ha sido desplegado en:
- Frontend (Vercel): https://flight-booking-lake.vercel.app
- Backend (Render): https://flightbooking-1.onrender.com

## Docker

Ejecución con Docker Compose:
Si prefiere ejecutar el proyecto completo usando Docker, puede hacerlo con Docker Compose.

1. Asegúrese de que tiene Docker y Docker Compose instalados.

2. Navegar a la raíz del proyecto (donde se encuentra el archivo docker-compose.yml).

3. Ejecutar el siguiente comando:
```bash
docker-compose up --build
```

Esto levantará tanto el frontend como el backend. El frontend estará disponible en http://localhost:3000 y el backend en http://localhost:8080.

Variables de entorno para Docker
El archivo .env dentro del frontend debe estar correctamente configurado con las siguientes variables de entorno:
```bash
REACT_APP_API_URL=http://localhost:8080
```

## Endpoints de la API

Vuelos
1. GET /Flights: Obtiene la lista de todos los vuelos disponibles.
2. POST /Flights: Crea un nuevo vuelo.
3. GET /Flights/{id}: Obtiene los detalles de un vuelo específico por su ID.

Reservas
1. GET /Reservations: Obtiene la lista de todas las reservas.
2. POST /Reservations: Crea una nueva reserva para un vuelo.

Estadísticas
1. GET /Flights/Statistics/TopAirlines: Obtiene las aerolíneas con más reservas.
2. GET /Flights/Statistics/AirlinesCount: Obtiene el número total de aerolíneas registradas.
