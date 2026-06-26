"# User Management API

This project implements a simple ASP.NET Core Web API for managing users for TechHive Solutions. It exposes CRUD endpoints backed by an in-memory collection so it can be run and tested quickly.

## What the API supports
- GET /users to list all users
- GET /users/{id} to retrieve one user
- POST /users to create a user
- PUT /users/{id} to update a user
- DELETE /users/{id} to remove a user

## Run locally
1. Open the solution in Visual Studio or VS Code.
2. Start the project with `dotnet run --project UserManagementAPI`.
3. Use the included HTTP file or Postman to exercise the endpoints.

## Copilot assistance notes
Microsoft Copilot helped by:
- scaffolding the ASP.NET Core Web API project structure and boilerplate code
- generating the CRUD endpoint layout and route group organization
- suggesting request/response models and validation logic for user input
- helping document the API flow for manual testing and future expansion

The API uses an in-memory store, so records reset when the process restarts." 
