# 🚀 Top5 APIs

A modern **ASP.NET Core Web API project** designed to manage and rank "Top 5" entities (teams, matches, players, etc.) with clean architecture and scalable backend practices.

---

## 📌 Overview

**Top5 APIs** is a backend system built to handle ranking logic, match management, and team/player interactions.  
The project focuses on **clean code, maintainability, and real-world backend patterns**.

It’s ideal for:
- Learning backend architecture
- Practicing RESTful API design
- Understanding scalable systems

---

## 🧠 Key Features

- 🔹 Create & manage teams and players  
- 🔹 Match scheduling and validation (no time conflicts)  
- 🔹 Ranking / Top 5 logic  
- 🔹 Clean architecture (separation of concerns)  
- 🔹 Repository Pattern + Service Layer  
- 🔹 DTOs & AutoMapper integration  
- 🔹 Async operations with Entity Framework  
- 🔹 Validation & business rules enforcement  

---

## 🏗️ Tech Stack

- **Backend:** ASP.NET Core Web API  
- **Language:** C#  
- **Database:** SQL Server  
- **ORM:** Entity Framework Core  
- **Mapping:** AutoMapper  
- **Architecture:** Clean Architecture (Layered)

---

## 📂 Project Structure

Top5/
│── Top5.Api            # Presentation Layer (Controllers)
│── Top5.Application    # Business Logic
│── Top5.Domain         # Entities & Core Models
│── Top5.Data           # Data Access (EF Core, Repositories)
│── Top5.Contracts      # DTOs & Interfaces

---

## ⚙️ Getting Started

### 🔧 Prerequisites
- .NET 10
- SQL Server
- Visual Studio / VS Code

---

### ▶️ Run the Project

```bash
git clone https://github.com/MrMeligy/Top5_APIs.git
cd Top5_APIs
dotnet restore
otnet ef migrations add InitialCreate
dotnet ef database update
//Make sure you have EF Core tools installed:
dotnet tool install --global dotnet-ef
//run
dotnet run
