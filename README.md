![C#](https://img.shields.io/badge/C%23-9.0-purple)
![.NET](https://img.shields.io/badge/.NET-6.0-purple)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20CORE-6.0-purple)
![Identity](https://img.shields.io/badge/Identity-blue)
![JWT](https://img.shields.io/badge/JWT-blue)
![MySQL](https://img.shields.io/badge/Database-MySQL-blue)

# WebLibrary

## 📖 Description

An asynchronous backend REST API built with ASP.NET Core, designed to manage a library system.

## 🚀 **Technologies**

The main technologies used in this project are:

- ⚙️ C# 9.0
- 🧠 ASP.NET Core 6
- 🔐 JWT (HMAC)
- 👥 ASP.NET Identity
- 🐬 MySQL
- 📦 Entity Framework Core

## 🎯 **Features**

- 👤 User registration and authentication (JWT-based)
- 📚 Book management (CRUD)
- 📖 Loan and renewal handling
- 💸 Penalty tracking for late returns
- 🔐 Role-based access control (Admin / User)

## ⚙ Prerequisites

Install these programs:

- **.NET SDK**
- **MySQL**
- **IDE** (Visual Studio, VSCode, Rider.)
- **Entity Framework Core Tools**

## ⚡ Steps to Run the Project

### 1. Clone the repository

Clone the project to your local environment:

```bash
git clone https://github.com/Dionclei-Pereira/WebLibrary.git
```
### 2. Configure MySQL

Locate the configuration file, the file is located at:

```text
   WebLibrary/Program
```

You can change the String Connection

example:
```C#
  var connection = "server=localhost;userid=root;password=12345678;database=library";
```
### 3. Apply migrations

You can use the Entity Framework CLI or the Visual Studio terminal to apply migrations:

```bash
dotnet ef database update
```
```bash
Update-Database
```

### 4. Run the Project

To run the project, you can use your IDE or DOTNET CLI at the .csproj folder
```bash
  dotnet run
```

### 5. Testing the API

The API is configured to allow login and generate a JWT token. You can use **Postman** to test the routes or swagger at {url}/swagger/index.html

- **POST** `/api/auth/login`: Send an `email` and `password` to receive a JWT token.
- **GET** `/api/books`: This route is protected and requires a valid JWT token in the Authorization header.

Example request for login:

POST /api/auth/login
```json
{
  "email": "email@gmail.com",
  "password": "password"
}
```

If the login is successful, a JWT token will be returned.

Example request to access a protected route:

- **GET** `/api/books` <br>
Authorization: _your-jwt-token_

## 📑 API Endpoints

Please use swagger to see the API Endpoints, some endpoints you must be an Admin to acess, the default admin is "email: dionclei2@gmail.com, password: ILoveDotNet"

## 📜Author

**Dionclei de Souza Pereira**

[Linkedin](https://www.linkedin.com/in/dionclei-de-souza-pereira-07287726b/)

⭐️ If you like this project, give it a star!  
