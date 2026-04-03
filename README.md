# 🚀 Top5 APIs

A modern **ASP.NET Core Web API** for managing football pitch bookings, match results, and team rankings. Every pitch runs its own local leaderboard — teams accumulate points per match and compete for the top 5 spots.

---

## 📌 Overview

**Top5 APIs** is a backend system built to handle match scheduling, result recording, and automatic ranking updates.

The project focuses on **clean code, maintainability, and real-world backend patterns**.

It's ideal for:

- Learning backend architecture
- Practicing RESTful API design
- Understanding scalable systems

---

## 🧠 Key Features

- 🔹 Create & manage teams and players
- 🔹 Match scheduling with time-conflict validation (no double-booking a pitch)
- 🔹 Points system — teams earn points per match result
- 🔹 Ranking / Top 5 leaderboard per pitch
- 🔹 Clean architecture (separation of concerns)
- 🔹 Repository Pattern + Service Layer
- 🔹 DTOs & AutoMapper integration
- 🔹 Async operations with Entity Framework Core
- 🔹 Validation & business rules enforcement

---

## 🏗️ Tech Stack

| Layer | Technology |
|---|---|
| Backend | ASP.NET Core Web API |
| Language | C# / .NET 10 |
| Database | SQL Server |
| ORM | Entity Framework Core |
| Mapping | AutoMapper |
| Architecture | Clean Architecture (Layered) |

---

## 📂 Project Structure
```
Top5/
├── src/
│   ├── Top5.Api          # Presentation layer — controllers, middleware
│   ├── Top5.Business     # Business logic — services, validators
│   ├── Top5.Domain       # Entities, enums, domain interfaces
│   ├── Top5.Data         # EF Core DbContext, repositories, migrations
│   └── Top5.Contracts    # DTOs and shared interfaces
└── tests/
    └── Top5.Tests        # Unit & integration tests
```

---

## ⚙️ Getting Started

### 🔧 Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server (local or Docker)
- Visual Studio 2022 / VS Code / Rider

---

### ▶️ Run the Project
```bash
# 1. Clone the repository
git clone https://github.com/MrMeligy/Top5_APIs.git
cd Top5_APIs

# 2. Restore dependencies
dotnet restore

# 3. Install the EF Core CLI tool (if not already installed)
dotnet tool install --global dotnet-ef

# 4. Update appsettings.json with your SQL Server connection string
#    See: src/Top5.Api/appsettings.json → "ConnectionStrings:DefaultConnection"

# 5. Apply database migrations
dotnet ef migrations add InitialCreate --project src/Top5.Data --startup-project src/Top5.Api
dotnet ef database update --project src/Top5.Data --startup-project src/Top5.Api

# 6. Run the API
dotnet run --project src/Top5.Api
```

Swagger UI will be available at: `https://localhost:{port}/swagger`

---

### 🐳 Running SQL Server with Docker (optional)

If you don't have SQL Server installed locally:
```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong@Passw0rd" \
  -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest
```

Then set your connection string to:
```
Server=localhost,1433;Database=Top5Db;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;
```

---

### 🧪 Running Tests
```bash
dotnet test
```

---
Full interactive docs available via Swagger at `/swagger`.

