# 🧩 User Management Service

A **modular, scalable, and maintainable** backend service for managing users, built using **.NET** and following **Clean Architecture** principles.  
This service provides all core functionalities needed to handle users — including creation, update, deletion, and retrieval — with support for **caching**, **repository abstraction**, and **Docker-based deployment**.

---

## 📘 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Architecture](#architecture)
- [Project Structure](#project-structure)
- [Tech Stack](#tech-stack)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Running the Project](#running-the-project)
- [Docker Setup](#docker-setup)
- [Environment Configuration](#environment-configuration)
- [API Endpoints](#api-endpoints)
- [Contributing](#contributing)
- [License](#license)
- [Author](#author)

---

## 🧾 Overview

The **User Management Service** is designed to handle core user operations (CRUD) in a structured and extensible manner.  
It is part of a microservice or modular backend architecture, and can easily integrate with authentication, authorization, or other business services.

This project emphasizes:
- Separation of concerns
- Scalability and performance
- Maintainable and testable codebase
- Production-grade readiness

---

## 🚀 Features

- ✅ **User CRUD Operations** — Create, Read, Update, and Delete users.  
- ⚙️ **Clean Architecture** — Proper separation of Domain, Application, and Infrastructure layers.  
- 🗄️ **Repository & Unit of Work Pattern** — Abstracts database logic for maintainability.  
- 🧠 **Caching Support** — Integrated caching layer for performance optimization.  
- 🧩 **Dependency Injection** — Built-in .NET DI container used throughout.  
- 🐳 **Docker Support** — Fully containerized setup using `docker-compose.yml`.  
- 🧱 **Extensible Design** — Easily add roles, authentication, or additional entities.  

---


This structure ensures:
- Clear separation between layers
- Easy testability
- Independent scalability for each component

---




---

## 🧰 Tech Stack

- **Language:** C#  
- **Framework:** .NET (Core)  
- **Architecture:** Clean / Layered Architecture  
- **Database:** SQL Server
- **Cache:** In-Memory / Redis  
- **Containerization:** Docker, Docker Compose  
- **ORM:** Entity Framework Core / Dapper  

---

## ⚙️ Getting Started

### 🧩 Prerequisites

Before running the project, ensure you have the following installed:

- [.NET SDK 8.0+](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://www.docker.com/)
- [Git](https://git-scm.com/)
- A running database (SQL Server / PostgreSQL)

---

### 🛠️ Installation

Clone the repository and restore dependencies:

```bash
git clone https://github.com/dihanhassan/user-managment-system.git
cd user-managment-system
dotnet restore
▶️ Running the Project
Option 1: Run via .NET CLI
dotnet build
dotnet run --project TodoApp.Application

🐳 Docker Setup

To run using Docker Compose:

docker-compose up --build

This will start:

User Management Service container

Database container (if configured)

Cache container (if configured)

After running, the API will be available at:

http://localhost:5000

⚙️ Environment Configuration

Update appsettings.json or environment variables as needed:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=UserDB;User Id=sa;Password=yourpassword;"
  },
  "CacheSettings": {
    "UseRedis": false,
    "RedisConnection": "localhost:6379"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning"
    }
  }
}

📡 API Endpoints
Method	Endpoint	Description
GET	/api/users	Get all users
GET	/api/users/{id}	Get user by ID
POST	/api/users	Create a new user
PUT	/api/users/{id}	Update existing user
DELETE	/api/users/{id}	Delete a user
