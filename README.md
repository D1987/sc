# SC — Server/VM/App Inventory

A self-hosted inventory system for tracking physical servers, virtual machines and the applications deployed on them, with their network, OS and access details in one place.

## What it does

- **Hosts** — physical/base servers: name, IP, OS, credentials, location, description, enabled flag.
- **VMs** — virtual machines attached to a host, with their own OS/credentials and a `Critical` flag.
- **Apps** — applications/services attached to a host and/or a VM, with domain, project, type and a `Critical` flag.
- Full CRUD over all three entities via a REST API, browsable through an Angular admin UI.
- JWT-based authentication (7‑day tokens) protecting the API; login is the only anonymous endpoint.

## Architecture

**Backend** — ASP.NET Core 3.1 Web API, layered as:

| Project | Responsibility |
|---|---|
| `Server.Application` | API host, controllers (`Host`, `VM`, `App`, `Users`), JWT auth |
| `Server.Services` | Business logic, AutoMapper profiles |
| `Server.Dal` | EF Core `DbContext`, entity configurations, migrations |
| `Server.Entities` | Domain models (`Host`, `VM`, `App`, `User`) |
| `Server.Dtos` | Request/response models |
| `Server.Exceptions` | Shared exception types |
| `Server.ModelsGenerator` (utils) | Generates TypeScript models for the UI from the C# DTOs |

**Frontend** — `ui/`, an Angular 10 + Angular Material single-page app consuming the REST API.

**Data** — SQL Server via EF Core; schema managed with EF Core migrations (`src/Server.Dal/Migrations`).

## Running locally

Backend:

```bash
cd src/Server.Application
dotnet run
```

Frontend:

```bash
cd ui
npm install
npm start
```

Configure the database connection and JWT secret in `src/Server.Application/appsettings.json` (or an environment-specific `appsettings.<env>.json`).

## Build & deploy

`deploy/Dockerfile` builds the whole stack in one image:

1. Builds `Server.Dtos` and runs `Server.ModelsGenerator` to produce TypeScript models for the UI.
2. Installs Node.js and builds the Angular app (`npm run deploy`), copying the output into the API's `wwwroot`.
3. Publishes the ASP.NET Core app and serves the built UI from the same container.

CI/CD is wired up via `.gitlab-ci.yml` for GitLab pipelines.

## TO DO

1. edit detect changes in form
2. services
3. table lazy loading
4. button up in tables
5. ngxs
6. hash passwords
7. error message for mat-select
8. not update if wasnt changes
9. fix appear login form
10. check create/edit one path
11. dark view
12. load not all data from method get all
13. to do common components
14. to do common c# projects' names
15. one import angular
16. if host autofill ip... for application
