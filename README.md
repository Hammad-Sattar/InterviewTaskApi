# InterviewTask



## Overview
This project is a simple ASP.NET Core REST API to manage books. Authors are stored as a hardcoded list and are not linked to the database.

## Features
- Get all books
- Get book by ID
- Create, Update, Delete book
- Search books by title
- Get all authors (hardcoded list)

## Technologies
- .NET 8
- Entity Framework Core
- SQL Server

## Setup Instructions

1. Clone the repository:

git clone <https://github.com/Hammad-Sattar/InterviewTaskApi.git>

2. Update the SQL Server connection string in appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=BookDb;Trusted_Connection=True;"
}


3.  Apply migrations and create the database:

dotnet ef migrations add InitialCreate
dotnet ef database update


Run the project:

dotnet run


Test the API using Swagger at:

https://localhost:<port>/swagger

Notes

Authors are hardcoded and independent from books.

Basic error handling is implemented.

The repository pattern is used for data access.
