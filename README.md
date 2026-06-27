# JobTracker API

A learning project built with ASP.NET Core 8, focused on practicing layered architecture, RESTful API design, Entity Framework Core, validation, exception handling, and domain business rules.

## Tech Stack

* .NET 8 / ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* Swagger / OpenAPI
* xUnit
* GitHub

## Architecture

The solution follows a layered architecture:

```text
JobTracker.API
    ↓
JobTracker.Application
    ↓
JobTracker.Domain

JobTracker.Infrastructure
    ↓
JobTracker.Application
    ↓
JobTracker.Domain
```

Projects:

```text
JobTracker.API             API controllers, middleware and application configuration
JobTracker.Application     DTOs, interfaces and application services
JobTracker.Domain          Entities, enums and domain business rules
JobTracker.Infrastructure  EF Core, SQL Server and repository implementations
JobTracker.Tests           Unit tests
```

## Features

* Job CRUD API
* Pagination, keyword search and status filtering
* DTO validation
* Swagger API documentation
* Global exception handling with ProblemDetails responses
* SQL Server persistence using EF Core migrations
* `JobStatus` enum stored as strings in the database
* Domain-level job status transition rules
* Unit tests for core job status rules

## Job Status Flow

```text
Applied → Interview → Offer
    ↓          ↓
Rejected ←─────┘
```

Allowed transitions:

```text
Applied   → Interview / Rejected
Interview → Offer / Rejected
Offer     → no further changes allowed
Rejected  → no further changes allowed
```

## API Endpoints

```text
GET    /api/jobs
GET    /api/jobs/{id}
POST   /api/jobs
PUT    /api/jobs/{id}
PATCH  /api/jobs/{id}/status
DELETE /api/jobs/{id}
```

Example status update request:

```json
{
  "status": "Interview"
}
```

## Run Locally

1. Create a local SQL Server database connection in:

```text
JobTracker.API/appsettings.Development.json
```

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=JobTrackerDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

2. Apply migrations:

```bash
dotnet ef database update --project JobTracker.Infrastructure --startup-project JobTracker.API
```

3. Run the API:

```bash
dotnet run --project JobTracker.API
```

4. Open Swagger using the URL shown in the terminal, usually:

```text
https://localhost:<port>/swagger
```

## Tests

Run all unit tests:

```bash
dotnet test
```

Current unit tests cover:

* New jobs default to `Applied`
* Valid transitions such as `Applied → Interview`
* Valid transitions such as `Interview → Offer`
* Invalid transitions such as `Rejected → Offer`
* Undefined enum values such as `(JobStatus)999`
