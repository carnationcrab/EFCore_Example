# Entity Framework - Model-First Setup for .NET APIs

## What is Entity Framework?

Entity Framework (EF) is an Object-Relational Mapper (ORM) for .NET, which allows developers to interact with databases using .NET objects. EF can generate database tables from C# classes (Model-First) or generate models from existing database tables (Database-First). In this example, we'll be using the **Model-First** approach.

### Key Benefits of EF:
- **Data Models in Code**: Define database tables directly in C#.
- **Migration Support**: Track and update database schema changes over time. Like `git` for your db!
- **Automatic CRUD Operations**: Easily interact with the database without writing SQL.

---

## Setup

### Step 1: Install Dependencies

First, restore any existing dependencies in your .NET project, if using a prebuilt repo:

```sh
dotnet restore
```

If you’re starting from scratch, add the following packages:

```sh
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Microsoft.EntityFrameworkCore.Design
```

### Step 2: Install the EF Command-Line Tool

The EF tool is a global command that helps you create migrations and update the database. Install it once globally:

```sh
dotnet tool install --global dotnet-ef
```

---

## Setting up the Database Connection

### Define the Connection String

In `appsettings.json`, specify your connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=my_database;"
  }
}
```

In `Startup.cs`, configure the connection string to use either the environment variable or the default in `appsettings.json`:

```csharp
string connectionString = Environment.GetEnvironmentVariable("DATABASE_URL_STR") ?? Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"Using connection string: {connectionString}");
```

---

## Defining the Model

### Step 1: Create the Model Class

In the `Models` folder, create a file called `yourModelNameHere.cs`:

### Step 2: Add the Model to the Database Context

In the `ApplicationContext.cs` file, register the model with the database context. This lets EF know that we want a table based on that class.

## Running Migrations

Migrations allow us to apply changes in our models to the database schema. Think of them as "commits" for your database structure.

### Step 1: Create the Migration

Run the following command to create a migration:

```sh
dotnet ef migrations add __
```
(replace ____ with something like CreateBakerTable)

This will generate a migration file in the `Migrations` folder with code to create the table.

### Step 2: Update the Database

After creating the migration, apply it to the database with:

```sh
dotnet ef database update
```

This command will execute the migration code, creating the table in your database.

---

## CRUD Operations

Now that we have a model and database table, we can create a controller to handle CRUD (Create, Read, Update, Delete) operations.

### Create the Controller

### Test with Postman

## Summary

1. **Define Models** - Create C# classes that represent database tables.
2. **Configure Context** - Add models to the `ApplicationContext` for EF to track.
3. **Run Migrations** - Use EF CLI to create and apply migrations to the database.
4. **Build Controllers** - Create controllers to handle CRUD operations for your models.
5. **Test in Postman** - Use Postman or another API client to test your endpoints.