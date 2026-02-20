# GeoRisk AI

GeoRisk AI es una aplicación web que permite consultar información geopolítica de un país y obtener un resumen junto con eventos importantes utilizando inteligencia artificial.

## Tecnologías utilizadas

- ASP.NET Core 8 (Web API)
- React + Vite
- OpenAI API
- REST Countries API



## Arquitectura

Frontend (React) → Backend (ASP.NET API) → OpenAI API



## Configuración del Backend

1. Clonar el repositorio
2. Crear un archivo `appsettings.Development.json`
3. Agregar la API Key de OpenAI:

```json
{
  "OpenAI": {
    "ApiKey": "TU_API_KEY_AQUI"
  }
}
```

4. Ejecutar:

```bash
dotnet run
```

El backend correrá en:


https://localhost:5000




## Endpoint principal


GET /api/risk/{country}


Ejemplo:
https://localhost:5000/api/risk/Colombia
