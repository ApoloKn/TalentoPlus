# TalentoPlus Employee Management System

## Overview
A comprehensive Employee Management System built with .NET 9/10, PostgreSQL, and Clean Architecture. Key features include:
- **Admin Portal**: ASP.NET Core MVC with Identity.
- **REST API**: JWT Authentication, Swagger UI.
- **AI Dashboard**: Gemini-powered employee insights.
- **Excel Import**: Bulk employee registration.
- **PDF Generation**: Employee resumes.

## Prerequisites
- Docker & Docker Compose
- .NET 9 SDK (or later)

## Getting Started

### 1. Run with Docker Compose
The easiest way to run the entire solution (Database + API + Web App).

```bash
docker-compose up --build
```

- **Web App**: http://localhost:5001
- **API**: http://localhost:5000 / http://localhost:5000/swagger
- **Database**: Port 5432 (User: postgres, Pass: password)

### 2. Manual Setup
Update connection strings in `appsettings.json` found in `TalentoPlus.API` and `TalentoPlus.Web`.

```bash
# Run Database (e.g., via Docker)
docker run -e POSTGRES_PASSWORD=password -p 5433:5432 -d postgres:15-alpine

# Run API
cd TalentoPlus.API
dotnet run

# Run Web
cd TalentoPlus.Web
dotnet run

# or
dotnet run --project TalentoPlus.Web/ & dotnet run --project TalentoPlus.API/
```

## Features & Usage
- **Admin Login**: Register a new user on the Web Portal.
- **Upload Excel**: Use the Import feature to load employees.
- **AI Dashboard**: Go to the Dashboard and ask questions like "How many employees are in IT?".
- **API**: Use Postman or Swagger to test endpoints.
    - Login: `POST /api/auth/login`
    - Get Resume: `GET /api/employee/resume` (Requires Bearer Token)

## Architecture
- **Domain**: Entities, Enums, Repo Interfaces.
- **Application**: Business Logic, DTOs, Service Interfaces.
- **Infrastructure**: EF Core, Implementation of Services (Email, PDF, Excel).
- **API**: REST Controllers.
- **Web**: MVC Controllers & Views.

## Credentials
- **JWT Secret**: `SuperSecretKey12345678901234567890` (Configure in appsettings for Prod)
- **SMTP**: Mocked (logs to console/void).
