# Delivery Report

## Implementation Checklist

- [x] Root folder `CustomerManagementSystem`
- [x] Solution file `CustomerManagementSystem.sln`
- [x] Eight backend projects
- [x] UserService clean architecture layers
- [x] CustomerService clean architecture layers
- [x] EF Core SQL Server `8.0.8` references
- [x] JWT Bearer `8.0.8` and Swagger `6.6.2` references
- [x] User registration endpoint
- [x] Login endpoint with JWT and refresh token
- [x] Authorized change password endpoint
- [x] Authorized customer paging/search endpoint
- [x] Startup database auto-create
- [x] 26 customer seed records
- [x] Angular 17 module-based app
- [x] Registration, Login, Change Password, Customers pages
- [x] Auth guard and HTTP interceptor
- [x] Responsive professional styling
- [x] Convenience BAT scripts
- [x] ZIP excludes `node_modules`, `.angular`, `bin`, `obj`, `dist`, `.vs`, logs, and temporary output

## Commands Run In This Workspace

```bash
node generate-project.mjs
dotnet --info
node -v
npm -v
npm install --cache /tmp/npm-cache
npm run build
find CustomerManagementSystem -name '*.csproj' | wc -l
find CustomerManagementSystem -name '*.csproj' | sort
rg static-audit-pattern CustomerManagementSystem
zip -r CustomerManagementSystem.zip CustomerManagementSystem -x excluded-build-and-cache-folders
unzip -l CustomerManagementSystem.zip
```

## Verification Results

- Backend restore/build: not verified in this workspace because `dotnet` command is not installed.
- SQL Server runtime smoke test: not verified because this Linux workspace has no SQL Server Express instance.
- Angular install: passed with `npm install --cache /tmp/npm-cache`.
- Angular build: passed with `npm run build`, bundle generation completed with 0 errors.
- Backend project count: passed, exactly 8 `.csproj` files found.
- Static source audit: passed after excluding generated dependency/build folders.
- ZIP file-list inspection: performed before final delivery.

## User PC Verification Commands

```powershell
dotnet restore CustomerManagementSystem.sln
dotnet build CustomerManagementSystem.sln
dotnet run --project src\UserService\UserService.Api\UserService.Api.csproj --launch-profile http
dotnet run --project src\CustomerService\CustomerService.Api\CustomerService.Api.csproj --launch-profile http
cd client
npm install
npm start
```

## Complete File Tree

```text
CustomerManagementSystem/
  .gitignore
  CustomerManagementSystem.sln
  DELIVERY-REPORT.md
  README.md
  Run-Angular.bat
  Run-CustomerService.bat
  Run-UserService.bat
  client/
    angular.json
    package.json
    tsconfig.app.json
    tsconfig.json
    src/
      index.html
      main.ts
      styles.scss
      app/
        app-routing.module.ts
        app.component.html
        app.component.scss
        app.component.ts
        app.module.ts
        guards/
          auth.guard.ts
        interceptors/
          auth.interceptor.ts
        models/
          auth.models.ts
          customer.models.ts
        pages/
          change-password/
            change-password.component.html
            change-password.component.scss
            change-password.component.ts
          customers/
            customers.component.html
            customers.component.scss
            customers.component.ts
          login/
            login.component.html
            login.component.scss
            login.component.ts
          register/
            register.component.html
            register.component.scss
            register.component.ts
        services/
          auth.service.ts
          customer.service.ts
      environments/
        environment.prod.ts
        environment.ts
  src/
    CustomerService/
      CustomerService.Api/
        CustomerService.Api.csproj
        Program.cs
        appsettings.json
        Controllers/
          CustomerController.cs
        Properties/
          launchSettings.json
      CustomerService.Application/
        CustomerService.Application.csproj
        DTOs/
          CustomerDtos.cs
        Interfaces/
          ICustomerQueryService.cs
          ICustomerRepository.cs
        Services/
          CustomerQueryService.cs
      CustomerService.Domain/
        CustomerService.Domain.csproj
        Entities/
          Customer.cs
      CustomerService.Infrastructure/
        CustomerService.Infrastructure.csproj
        DependencyInjection.cs
        Persistence/
          CustomerDbContext.cs
          CustomerSeeder.cs
        Repositories/
          CustomerRepository.cs
    UserService/
      UserService.Api/
        UserService.Api.csproj
        Program.cs
        appsettings.json
        Controllers/
          ChangePasswordController.cs
          LoginController.cs
          UserRegistrationController.cs
        Properties/
          launchSettings.json
        Services/
          JwtTokenService.cs
      UserService.Application/
        UserService.Application.csproj
        DTOs/
          UserDtos.cs
        Interfaces/
          IJwtTokenService.cs
          IPasswordHasher.cs
          IUserAuthService.cs
          IUserRepository.cs
        Services/
          UserAuthService.cs
      UserService.Domain/
        UserService.Domain.csproj
        Entities/
          AppUser.cs
      UserService.Infrastructure/
        UserService.Infrastructure.csproj
        DependencyInjection.cs
        Persistence/
          UserDbContext.cs
        Repositories/
          UserRepository.cs
        Security/
          Pbkdf2PasswordHasher.cs
```
