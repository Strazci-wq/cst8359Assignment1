# Veterinary Clinic System

An ASP.NET Core MVC application for managing veterinarians, pets, and pet profiles.

## Features

- **Veterinarian Management**: CRUD operations for managing veterinarians and their specialties
- **Pet Management**: CRUD operations for managing pets with assigned veterinarians
- **Pet Profile Management**: 
  - Create profiles with veterinary notes via form entry
  - Create profiles by uploading .txt files containing veterinary notes
  - Full CRUD operations for pet profiles

## Prerequisites

- .NET 8.0 SDK or later
- SQL Server (LocalDB is used by default)
- Visual Studio 2022 or VS Code (optional)

## Setup Instructions

1. **Restore NuGet packages:**
   ```bash
   dotnet restore
   ```

2. **Restore client-side libraries (Bootstrap, jQuery):**
   ```bash
   dotnet tool install -g Microsoft.Web.LibraryManager.Cli
   libman restore
   ```
   Or manually download Bootstrap and jQuery to `wwwroot/lib/` if libman is not available.

3. **Run the application:**
   ```bash
   dotnet run
   ```

4. **Access the application:**
   - Navigate to `https://localhost:5001` or `http://localhost:5000` in your browser
   - The database will be automatically created and seeded with initial data

## Database

The application uses Entity Framework Core with SQL Server LocalDB by default. The connection string is configured in `appsettings.json`.

The database will be automatically created when you run the application for the first time, and it will be seeded with:
- 3 Veterinarians
- 4 Pets
- 4 Pet Profiles

## Project Structure

```
VeterinaryClinic/
├── Controllers/
│   ├── HomeController.cs
│   ├── VetDoctorsController.cs
│   ├── PetsController.cs
│   └── PetProfilesController.cs
├── Models/
│   ├── VetDoctor.cs
│   ├── Pet.cs
│   ├── PetProfile.cs
│   └── ErrorViewModel.cs
├── Data/
│   ├── VeterinaryClinicDbContext.cs
│   └── SeedData.cs
├── Views/
│   ├── Home/
│   ├── VetDoctors/
│   ├── Pets/
│   └── PetProfiles/
└── wwwroot/
```

## Usage

1. **Veterinarians**: Navigate to "Veterinarians" to manage veterinarian records
2. **Pets**: Navigate to "Pets" to manage pet records. When creating/editing, select a veterinarian from the dropdown
3. **Pet Profiles**: Navigate to "Pet Profiles" to manage veterinary notes. You can:
   - Create a profile by entering notes manually
   - Create a profile by uploading a .txt file containing the notes

## Notes

- Each pet can only have one profile (one-to-one relationship)
- A veterinarian can have multiple pets assigned (one-to-many relationship)
- When uploading a file for pet profile creation, only .txt files are accepted
- The file content is stored in the database, not the file itself

## Deployment

For Azure deployment:
1. Create an Azure App Service
2. Create an Azure SQL Database (or use SQL Server)
3. Update the connection string in Azure App Settings
4. Deploy the application using Visual Studio or Azure CLI

