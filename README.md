# Sacco API

.NET 8 Web API — Clean Architecture for the Sacco management platform.  
PostgreSQL · EF Core · MediatR (CQRS) · FluentValidation · Unit of Work

## Project Structure

```
src/
├── SaccoApi.API/               Controllers, middleware, DI wiring, Swagger
├── SaccoApi.Application/       CQRS commands/queries, DTOs, validators, Result<T>
├── SaccoApi.Domain/            Entities, BaseEntity (no external dependencies)
└── SaccoApi.Infrastructure/    EF Core, PostgreSQL, UnitOfWork, Repositories
```

## Quick Start

### Prerequisites
- .NET 8 SDK
- PostgreSQL 14+
- EF Core CLI: `dotnet tool install --global dotnet-ef`

### 1. Configure connection string
Edit `src/SaccoApi.API/appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=SaccoDB_Dev;Username=postgres;Password=yourpassword"
  }
}
```

### 2. Create DB schema
```sql
CREATE DATABASE "SaccoDB_Dev";
\c SaccoDB_Dev
CREATE SCHEMA IF NOT EXISTS sacco;
```

### 3. Run EF migration
```bash
dotnet ef migrations add InitialCreate \
  --project src/SaccoApi.Infrastructure \
  --startup-project src/SaccoApi.API \
  --output-dir Data/Migrations

dotnet ef database update \
  --project src/SaccoApi.Infrastructure \
  --startup-project src/SaccoApi.API
```

### 4. Run the API
```bash
cd src/SaccoApi.API && dotnet run
```

- **Swagger UI**: http://localhost:5000
- **Liveness**: `GET /api/health/ping`
- **Full health**: `GET /api/health`

### 5. Initialize the organization
```
POST /api/organization
{
  "registeredName": "Kampala Community SACCO",
  "shortName": "KCS",
  "registrationNumber": "REG-2024-001",
  "businessLine": "Savings and Credit",
  "contactEmail": "info@kcs.co.ug",
  "defaultBranch": {
    "branchName": "Head Office",
    "address": "Plot 12, Kampala Road, Kampala",
    "emailAddress": "headoffice@kcs.co.ug",
    "postalAddress": "P.O. Box 1234, Kampala"
  }
}
```

## API Endpoints

### Organization
| Method | Route | Description |
|--------|-------|-------------|
| `GET`  | `/api/organization` | Get organization with all branches |
| `GET`  | `/api/organization/{id}` | Get by ID |
| `POST` | `/api/organization` | Register organization + default branch |
| `PUT`  | `/api/organization/{id}` | Update profile |

### Branches
| Method | Route | Description |
|--------|-------|-------------|
| `GET`  | `/api/organization/{orgId}/branches` | List all branches |
| `GET`  | `/api/organization/{orgId}/branches/{branchId}` | Get single branch |
| `POST` | `/api/organization/{orgId}/branches` | Add a branch |
| `PATCH`| `/api/organization/{orgId}/branches/{branchId}/set-default` | Promote to default |

### Health
| Method | Route | Description |
|--------|-------|-------------|
| `GET`  | `/api/health/ping` | Liveness — API running? |
| `GET`  | `/api/health` | Readiness — DB reachable + org initialized? |
| `GET`  | `/health/ready` | ASP.NET probe (Docker/K8s) |

## Module Status

| Module | Status |
|--------|--------|
| **Organization** | ✅ Complete |
| Customer KYC | Pending |
| Savings | Pending |
| Time Deposits | Pending |
| Vendors | Pending |
| Shares | Pending |
| Micro Insurance | Pending |
| Accounting | Pending |
| System Audits | Pending |
| Business Operations | Pending |

## Architecture Patterns

### Adding a new module (checklist)

1. **Domain entity** in `SaccoApi.Domain/Entities/`  
   → Extend `BaseEntity` (gets `Id`, `CreatedAt`, `IsDeleted`, etc.)

2. **EF configuration** in `SaccoApi.Infrastructure/Data/Configurations/`  
   → Implement `IEntityTypeConfiguration<T>`

3. **Repository interface + implementation** in `SaccoApi.Infrastructure/Repositories/`  
   → Extend `IBaseRepository<T>` / `BaseRepository<T>`

4. **Register in UnitOfWork** — add property to `IUnitOfWork` and `UnitOfWork`

5. **Register in DI** — add `services.AddScoped<IRepo, Repo>()` in `InfrastructureExtensions`

6. **Commands & Queries** in `SaccoApi.Application/Modules/{Module}/`  
   → Return `Result<T>` · Use `IUnitOfWork` — never DbContext directly

7. **Validators** in `SaccoApi.Application/Modules/{Module}/Validators/`

8. **Controller** in `SaccoApi.API/Controllers/`  
   → Inject `IMediator` · Map `Result<T>` to HTTP status codes

9. **EF migration**  
   → `dotnet ef migrations add Add_{Module}Module ...`

## Key Design Decisions

**`IsDefault` on Branch** — enforced with a partial unique index in PostgreSQL so only one branch per organization can be default at a time, even under concurrent writes.

**Branch as aggregate root** — `Branch.Id` is the FK that all future modules (BranchHoliday, BranchCostCenter, BranchLedgerAccount, BranchConfiguration, BranchEntityAccess) will reference. The entity is intentionally sparse now; other modules extend it through their own tables.

**Organization registration requires a default branch** — you cannot create an orphan organization. The org + first branch are written in a single transaction.

**`Result<T>` pattern** — handlers never throw business exceptions across layer boundaries. Every service/handler returns `Result<T>` with `IsSuccess`, `Data`, `Error`, and `ErrorCode`. Controllers map error codes to HTTP status codes.

**Soft delete everywhere** — `BaseEntity.IsDeleted` means no org or branch data is ever physically removed. All `BaseRepository<T>` queries filter `IsDeleted = false` automatically via a global query filter.
