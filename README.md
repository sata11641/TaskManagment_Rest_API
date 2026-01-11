# Task Management REST API

A simple REST API for managing tasks built with .NET 8 and following SOLID principles.

## What's Inside

- Create, read, update, and delete tasks
- Filter tasks by completion status (done/pending)
- Sort by priority (Low, Medium, High, Critical)
- Track when tasks are created and completed
- Swagger UI for easy API testing
- Dependency injection and clean architecture

## Tech Stack

- .NET 8 & C# 12
- ASP.NET Core
- Swagger/OpenAPI for documentation
- Repository Pattern
- SOLID principles

## Project Structure

## Quick Start

### Prerequisites
- .NET 8 SDK

### Installation
Clone the repo
git clone https://github.com/sata11641/TaskManagment_Rest_API.git cd TaskManagment_Rest_API
Restore dependencies
dotnet restore
Build
dotnet build
Run
dotnet run

Open `https://localhost:5001` to see Swagger UI.

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/tasks` | Get all tasks |
| GET | `/api/tasks/{id}` | Get task by ID |
| GET | `/api/tasks/completed` | Get completed tasks |
| GET | `/api/tasks/pending` | Get pending tasks |
| POST | `/api/tasks` | Create new task |
| PUT | `/api/tasks/{id}` | Update task |
| PATCH | `/api/tasks/{id}/complete` | Mark task as done |
| DELETE | `/api/tasks/{id}` | Delete task |

## Create a Task

## Priority Levels

- `0` = Low
- `1` = Medium
- `2` = High
- `3` = Critical

## Notes

- Currently uses in-memory storage (data resets on restart)
- For production, replace with a real database (SQL Server, PostgreSQL, etc.)
- CORS is enabled for all origins (restrict this in production)

## Example Usage

## Architecture

The project follows SOLID principles:

- **Single Responsibility**: Each class does one thing
- **Open/Closed**: Open for extension, closed for modification
- **Liskov Substitution**: Interfaces are properly implemented
- **Interface Segregation**: Small, focused interfaces
- **Dependency Inversion**: Depends on abstractions, not concrete implementations

Uses the **Repository Pattern** to abstract data access and **Dependency Injection** for loose coupling.

## License

MIT
