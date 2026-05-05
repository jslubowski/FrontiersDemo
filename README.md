# FrontiersDemo

A .NET 10 Web API demo project.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) — for running locally
- [Docker](https://www.docker.com/get-started) — for running in a container

---

## Running locally with the .NET SDK

```bash
dotnet run --project src/FrontiersDemo.Api
```

The API starts on `http://localhost:5090` by default (or whichever port is configured in `launchSettings.json`).

Scalar and the OpenAPI spec are available at:

- Scalar UI: `http://localhost:5090/scalar/v1`
- OpenAPI JSON: `http://localhost:5090/openapi/v1.json`

### Configuration

The `Frontiers` section in `appsettings.json` controls the external organizations API:

```json
"Frontiers": {
  "BaseUrl": "https://organizations-api.frontiersin.org",
  "MaxCount": 1
}
```

Override either value via environment variable before running:

```bash
Frontiers__BaseUrl=https://... Frontiers__MaxCount=5 dotnet run --project src/FrontiersDemo.Api
```

### Running tests

```bash
dotnet test
```

---

## Running with Docker Compose

```bash
docker-compose up --build
```

The API is mapped to host port **5090**:

- Scalar UI: `http://localhost:5090/scalar/v1`
- OpenAPI JSON: `http://localhost:5090/openapi/v1.json`

To run in the background:

```bash
docker-compose up --build -d
```

To stop the container:

```bash
docker-compose down
```