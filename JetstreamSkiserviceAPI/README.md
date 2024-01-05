# ICT-Module 295 - Jetstream Skiservice WebAPI

## Overview

The Jetstream Ski Service API is a robust backend service designed to manage the [Website](https://github.com/mahgoe/ICT_Modul294_Praxisarbeit) we created in the past. It provides endpoints for registration, status, prioriry and a JWT authentification.

This project was created under the conditions of the [ICT-Modulbaukasten](https://www.modulbaukasten.ch/module/295/1/de-DE?title=Backend-f%C3%BCr-Applikationen-realisieren) and the requirements of our teacher.

## Installation

### Prerequisites

- [.NET 7.0 SDK](https://dotnet.microsoft.com/en-us/download)
- [Microsoft SQL Server](https://www.microsoft.com/de-ch/sql-server/sql-server-downloads)

### Steps

1. Clone the repository to your local machine.
2. Ensure your SQL Server instance is running.
3. Ensure the right Connectionstring is choosen in appsettings.json
4. Open Packet Manager Terminal and Use: `Add-Migration InitialCreate` and after a successfull migration use `Update-Database` (If the Add-Migration doesn't work, please check if you have EF Core Tools installed).
5. Open the SQLScript that is in `JetstreamSkiserviceDB_SQLScript` and use the Query `SQLQuery_CREATE_USER`
6. Open Project Solution in Visual Studio or similar.
7. Update the database connectionstring in the application settings.
6. Open Project Solution in Visual Studio or similar
7. Open Packet Manager Terminal and Use: `Add-Migration InitialCreate` and after a successfull migration use `Update-Database`
9. Update the database connection string in the application settings.
10. With the SQL Query `SQLQuery_SELECT` you can check every data from the initial create.
11. Start the service and Swagger will open in your Browser. Access the website with localhost:xxxx/index.html (since the frontend is in wwwroot folder you can take the ip:port from the backend)

### All NuGet Packages

| NuGet-Package                                 | Version |
| --------------------------------------------- | ------- |
| Microsoft.AspNetCore.Authentication.JwtBearer | 7.0.14  |
| Microsoft.AspNetCore.OpenApi                  | 7.0.13  |
| Microsoft.EntityFrameworkCore                 | 7.0.13  |
| Microsoft.EntityFrameworkCore.SqlServer       | 7.0.13  |
| Microsoft.EntityFrameworkCore.Tools           | 7.0.13  |
| Microsoft.IdentityModel.Tokens                | 7.0.3   |
| Serilog.AspNetCore                            | 7.0.0   |
| Swashbuckle.AspNetCore                        | 6.5.0   |
| System.IdentityModel.Tokens.Jwt               | 7.0.3   |
| FluentAssertions                              | 6.12.0  |
| Microsoft.NET.Test.Sdk                        | 17.6.0  |
| Moq                                           | 4.20.69 |
| Xunit                                         | 2.6.1   |
| xunit.runner.visualstudio                     | 2.4.5   |
| coverlet.collector                            | 3.2.0   |

## Usage

After installation and database setup, you can start the API with `dotnet run`. The API is now ready to accept requests at the designated endpoints.

Refer to the Postman collection `JetstreamSkiserviceAPI.postman_collection.json` for examples of requests that can be made to the API.

## Database Setup

The SQL scripts provided in the `SQLScript_JetstreamSkiserviceDB` directory can be used to select all data after you used Add-Migration and Update-Database in your Packet Manager. The nessecary data will be inserted in the migration provided by `RegistrationDbContext.cs`

## Projectdocument

The full projectdocument is in `JetstreamSkiserviceAPI_Projectdocument` and in the german language.
