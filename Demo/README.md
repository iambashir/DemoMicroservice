# Customer Management System Practical Exam

এই project-এ প্রশ্ন অনুযায়ী Angular frontend এবং ASP.NET Core 8 clean architecture microservices তৈরি করা হয়েছে।

## Install/Environment Check

ইনস্টল আছে:

- .NET SDK 8.0.423, 9, 10
- ASP.NET Core Runtime 8
- Node.js 20.9.0
- npm 10.1.0
- Angular CLI 17.3.17
- SQL Server Express: running
- sqlcmd
- Git

ইনস্টল নেই:

- Docker

Docker এই solution run করার জন্য বাধ্যতামূলক না।

## Projects

- `src/UserService/UserService.Api`
  - `POST /api/UserRegistration`
  - `POST /api/Login`
  - `PUT /api/changePassword`
- `src/CustomerService/CustomerService.Api`
  - `GET /api/customer?page=1&pageSize=10&search=abc`
- `client`
  - Angular UI: Registration, Login, Change Password, Customers

## Database

Default SQL Server connection:

- `Server=.\SQLEXPRESS`
- `YourID_UserService`
- `YourID_CustomerService`

তোমার student ID থাকলে `appsettings.json`-এ database name replace করে নিতে পারো।

## Run Commands

UserService:

```powershell
dotnet run --project src\UserService\UserService.Api\UserService.Api.csproj --launch-profile http
```

CustomerService:

```powershell
dotnet run --project src\CustomerService\CustomerService.Api\CustomerService.Api.csproj --launch-profile http
```

Angular:

```powershell
cd client
npm start
```

URLs:

- Angular: `http://127.0.0.1:4200`
- UserService Swagger: `http://localhost:5001/swagger`
- CustomerService Swagger: `http://localhost:5002/swagger`

## Demo Login

Smoke test-এর সময় এই user তৈরি করা হয়েছে:

- User Name: `johnsmith`
- Password: `Password@123`

## Verification Done

- Backend build: success, 0 errors
- Angular build: success
- Registration API: tested
- Login API: tested
- Change Password API: tested and reverted
- Customer API with JWT: tested
- Initial customer load: 10 records
