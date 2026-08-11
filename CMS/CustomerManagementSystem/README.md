# Customer Management System

এই project-টি .NET 8 Web API ভিত্তিক দুইটি independent microservice এবং Angular 17 frontend দিয়ে তৈরি করা হয়েছে। UserService registration, login, JWT এবং password change handle করে। CustomerService JWT validate করে customer paging/search data দেয়।

## Prerequisites

- Windows 10/11
- Visual Studio 2022
- .NET SDK 8.x
- SQL Server Express: `.\SQLEXPRESS`
- Node.js 20.x এবং npm 10.x
- VS Code

## Architecture

- `src/UserService`: User registration, login, password hashing, JWT issue.
- `src/CustomerService`: Authorized customer paging and search.
- `client`: Angular 17 module-based frontend.

প্রতিটি service Clean Architecture style follow করে:

- Domain: entity only
- Application: DTO, interface, business service
- Infrastructure: EF Core DbContext, repository, hashing/seed
- Api: controller, JWT configuration, Swagger, startup

UserService এবং CustomerService একে অন্যের project, DbContext অথবা database reference করে না।

## Database

User database:

```text
Server=.\SQLEXPRESS;Database=YourID_UserService;Trusted_Connection=True;TrustServerCertificate=True
```

Customer database:

```text
Server=.\SQLEXPRESS;Database=YourID_CustomerService;Trusted_Connection=True;TrustServerCertificate=True
```

First run-এ `EnsureCreatedAsync()` database auto-create করবে। Migration command লাগবে না। Customer database প্রথমবার create হলে demo হিসেবে 26টি realistic customer seed হবে।

Production environment-এ JWT key কখনো source control-এ রাখা যাবে না; secure secret manager বা environment variable ব্যবহার করতে হবে।

## Backend Build

PowerShell থেকে project root-এ চালান:

```powershell
dotnet restore CustomerManagementSystem.sln
dotnet build CustomerManagementSystem.sln
```

## Backend Run

UserService:

```powershell
dotnet run --project src\UserService\UserService.Api\UserService.Api.csproj --launch-profile http
```

CustomerService:

```powershell
dotnet run --project src\CustomerService\CustomerService.Api\CustomerService.Api.csproj --launch-profile http
```

Expected URLs:

- UserService Swagger: `http://localhost:5001/swagger`
- CustomerService Swagger: `http://localhost:5002/swagger`

## Angular Run

```powershell
cd client
npm install
npm start
```

Frontend URL:

```text
http://localhost:4200
```

## Visual Studio 2022 Run

1. `CustomerManagementSystem.sln` open করুন।
2. Solution Explorer-এ solution name-এর ওপর right click করুন।
3. `Configure Startup Projects` select করুন।
4. `Multiple startup projects` select করুন।
5. `UserService.Api` এবং `CustomerService.Api` project-এর Action `Start` দিন।
6. Apply করে Start চাপুন।
7. Swagger open না হলে manually `http://localhost:5001/swagger` এবং `http://localhost:5002/swagger` browse করুন।

## VS Code Frontend Run

1. VS Code দিয়ে `client` folder open করুন।
2. Terminal খুলুন।
3. `npm install` চালান।
4. `npm start` চালান।
5. Browser-এ `http://localhost:4200` open করুন।

## API Test Flow

1. `POST http://localhost:5001/api/UserRegistration`
2. একই username/email দিয়ে আবার registration করলে 409 duplicate response দেখুন।
3. `POST http://localhost:5001/api/Login` থেকে JWT accessToken নিন।
4. CustomerService Swagger-এ Authorize button চাপুন এবং token paste করুন।
5. `GET http://localhost:5002/api/customer?page=1&pageSize=10&search=dhaka` call করুন।
6. `PUT http://localhost:5001/api/changePassword` call করুন। Request username এবং JWT username same হতে হবে।

## Troubleshooting

### Port already in use

যদি 5001, 5002 অথবা 4200 port busy থাকে, সংশ্লিষ্ট process close করুন। এই project fixed port ব্যবহার করে, তাই port free করা সবচেয়ে সহজ।

### Swagger 404

সঠিক URL ব্যবহার করুন:

- `http://localhost:5001/swagger`
- `http://localhost:5002/swagger`

HTTPS URL ব্যবহার করলে local redirect issue হতে পারে; এই project HTTP-only local profile দিয়ে করা হয়েছে।

### SQL connection failure

- SQL Server Express install আছে কিনা দেখুন।
- instance name `.\SQLEXPRESS` কিনা verify করুন।
- Windows Authentication enabled কিনা check করুন।
- SQL Server service running কিনা দেখুন।

### CORS error

Angular অবশ্যই `http://localhost:4200` বা `http://127.0.0.1:4200` থেকে চালান। অন্য port ব্যবহার করলে API CORS policy update করতে হবে।

### 401 Unauthorized

- Login করে new token নিন।
- Swagger Authorize button-এ token paste করার সময় শুধু token দিলেই হবে।
- Token expired হলে আবার login করুন।
- change password request-এর userName token-এর userName-এর সাথে match করতে হবে।
