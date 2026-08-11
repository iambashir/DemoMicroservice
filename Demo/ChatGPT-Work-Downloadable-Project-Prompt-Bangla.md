# ChatGPT Work Prompt: Downloadable Full Project

ChatGPT-তে **Work** mode নির্বাচন করে নিচের সম্পূর্ণ prompt-টি পাঠাও:

```text
তুমি এই task-এ একজন senior .NET 8 এবং Angular 17 full-stack engineer হিসেবে কাজ করবে। আমার জন্য একটি সম্পূর্ণ, compile-ready, downloadable “Customer Management System” project তৈরি করো। Chat-এ শুধু code snippets বা tutorial লিখে থেমে যাবে না। তোমার writable workspace-এ আসল folder ও file তৈরি করবে, সম্ভব হলে terminal/tool ব্যবহার করে dependencies restore ও build করবে, সব detected error ঠিক করবে, তারপর পুরো project-টি ZIP file হিসেবে আমাকে download করার জন্য attach/deliver করবে।

আমাকে implementation-এর মাঝে confirmation question করবে না। Reasonable technical decision নিজে নেবে এবং task শেষ না হওয়া পর্যন্ত কাজ চালিয়ে যাবে। সব user-facing explanation ও documentation বাংলায় লিখবে; source code, namespace, identifier, command এবং application UI text English-এ রাখবে।

FINAL DELIVERABLES:
1. `CustomerManagementSystem.zip` — download-ready complete source project।
2. ZIP-এর root-এর ভেতর একটিমাত্র `CustomerManagementSystem` folder থাকবে।
3. Root folder-এ `CustomerManagementSystem.sln`, `README.md`, `.gitignore`, `src` এবং `client` থাকবে।
4. `README.md`-এ বাংলায় prerequisites, architecture, database, build/run, Visual Studio/VS Code instructions, API test flow এবং troubleshooting থাকবে।
5. `DELIVERY-REPORT.md`-এ complete file tree, implementation checklist, restore/build results, যেসব command চালিয়েছ, এবং কোনো unverified item থাকলে তার সৎ কারণ থাকবে।
6. Chat-এর final response-এ ZIP download attachment/link, project tree-এর short summary, verification result এবং exact first-run steps দেবে। Full source code chat-এ repeat করবে না, কারণ source ZIP-এর ভেতরে থাকবে।

PACKAGING RULES:
- ZIP-এ source/configuration/documentation থাকবে।
- `node_modules`, `.angular`, `bin`, `obj`, `.vs`, test-result cache, log, database file, user secret, generated build output এবং অন্য temporary file include করবে না।
- কোনো empty required file, TODO, pseudo-code, `...`, placeholder implementation, missing namespace বা “implement later” থাকবে না।
- absolute machine path ব্যবহার করবে না। Windows-compatible relative path ও line ending ব্যবহার করবে।
- ZIP বানানোর আগে archive-এর file list inspect করে নিশ্চিত করবে যে `.sln`, আটটি `.csproj`, দুই API-এর configuration এবং Angular source সত্যিই আছে।
- ZIP extraction-এর পরে user যেন folder relocate করেও build করতে পারে।

TARGET ENVIRONMENT:
- Windows 10/11, PowerShell
- Visual Studio 2022 দিয়ে backend open/run করা হবে
- VS Code দিয়ে Angular frontend open/run করা হবে
- .NET SDK 8.x; সব backend project target `net8.0`
- Node.js 20.x, npm 10.x, Angular CLI/Angular 17.3.x
- SQL Server Express instance: `.\SQLEXPRESS`
- Docker প্রয়োজন নেই
- UserService URL: `http://localhost:5001`
- CustomerService URL: `http://localhost:5002`
- Angular URL: `http://localhost:4200`
- User database: `YourID_UserService`
- Customer database: `YourID_CustomerService`

REQUIRED PROJECT STRUCTURE:

CustomerManagementSystem/
  CustomerManagementSystem.sln
  README.md
  DELIVERY-REPORT.md
  .gitignore
  src/
    UserService/
      UserService.Domain/
        UserService.Domain.csproj
      UserService.Application/
        UserService.Application.csproj
      UserService.Infrastructure/
        UserService.Infrastructure.csproj
      UserService.Api/
        UserService.Api.csproj
    CustomerService/
      CustomerService.Domain/
        CustomerService.Domain.csproj
      CustomerService.Application/
        CustomerService.Application.csproj
      CustomerService.Infrastructure/
        CustomerService.Infrastructure.csproj
      CustomerService.Api/
        CustomerService.Api.csproj
  client/
    package.json
    angular.json
    src/

এটি দুইটি independent microservice এবং একটি Angular frontend। মোট আটটি backend project থাকবে। শুধু `UserService.Api` ও `CustomerService.Api` runnable Web API; অন্য ছয়টি class library।

CLEAN ARCHITECTURE DEPENDENCIES:
- প্রতিটি Domain project-এর কোনো project reference থাকবে না।
- প্রতিটি Application project শুধু একই service-এর Domain reference করবে।
- প্রতিটি Infrastructure project একই service-এর Application ও Domain reference করবে।
- প্রতিটি Api project একই service-এর Application ও Infrastructure reference করবে।
- UserService ও CustomerService পরস্পরের project, DbContext বা database reference করবে না।
- Clean Architecture, Repository Pattern, Dependency Injection, async/await এবং SOLID বাস্তবে ব্যবহার করবে। Exam-size solution রাখবে; অপ্রয়োজনীয় CQRS, MediatR বা message broker যোগ করবে না।

BACKEND PACKAGES:
- ASP.NET Core 8 Web API এবং C#
- Entity Framework Core SQL Server `8.0.8`
- `Microsoft.EntityFrameworkCore.Design` `8.0.8` যেখানে প্রয়োজন
- `Microsoft.AspNetCore.Authentication.JwtBearer` `8.0.8`
- `Swashbuckle.AspNetCore` `6.6.2`
- Nullable reference types ও implicit usings enabled থাকবে।

DATABASE RULES:
- UserService-এর নিজস্ব `UserDbContext` এবং connection string থাকবে:
  `Server=.\SQLEXPRESS;Database=YourID_UserService;Trusted_Connection=True;TrustServerCertificate=True`
- CustomerService-এর নিজস্ব `CustomerDbContext` এবং connection string থাকবে:
  `Server=.\SQLEXPRESS;Database=YourID_CustomerService;Trusted_Connection=True;TrustServerCertificate=True`
- Local exam convenience-এর জন্য startup-এ `Database.EnsureCreated()`/async equivalent ব্যবহার করবে, তাই first run-এ database auto-create হবে এবং migration command প্রয়োজন হবে না।
- Username ও email case-insensitive unique হবে এবং database unique index থাকবে।
- Customer database first create হলে অন্তত 25টি realistic customer seed হবে, যাতে paging/Load More কয়েকবার test করা যায়।

SECURITY ও API BEHAVIOR:
- Password কখনো plain text-এ store করবে না। Random per-user salt সহ `Rfc2898DeriveBytes.Pbkdf2`, SHA256, strong iteration count এবং fixed-time comparison ব্যবহার করবে; hash/salt Base64 string হিসেবে রাখবে।
- Login success-এ HS256 signed JWT generate করবে। JWT-তে user id, username এবং full name claim থাকবে; expiry 3600 seconds।
- Cryptographically secure random refresh token return করবে; refresh endpoint এই task-এ প্রয়োজন নেই।
- দুই service-এর local `appsettings.json`-এ একই demo JWT key, issuer ও audience থাকবে, যাতে UserService-এর token CustomerService validate করতে পারে। Key 32 bytes-এর বেশি হবে। README-তে বলবে production-এ secret source control-এ রাখা যাবে না।
- Swagger সব environment-এ available হবে এবং JWT Bearer `Authorize` button থাকবে।
- UserService সবসময় `http://localhost:5001`, CustomerService সবসময় `http://localhost:5002`-এ run করবে। `Program.cs`-এর `UseUrls` এবং `Properties/launchSettings.json` উভয় জায়গায় এটি enforce করবে, যাতে দুই service default port 5000 নেওয়ার চেষ্টা না করে।
- HTTP-only local profile ব্যবহার করবে এবং এমন HTTPS redirect রাখবে না যা local Swagger/API call ভেঙে দিতে পারে।
- CORS-এ `http://localhost:4200` এবং `http://127.0.0.1:4200` allow করবে।
- middleware order ঠিক থাকবে: Swagger, CORS, Authentication, Authorization, MapControllers।
- protected endpoint-এ `[Authorize]` থাকবে। Validation/business case অনুযায়ী 400, duplicate-এ 409, invalid login/token-এ 401, registration success-এ 201 এবং অন্য success-এ 200 দেবে। JSON camelCase হবে।

USER SERVICE EXACT ENDPOINTS:

1. `POST /api/UserRegistration`
Request:
{
  "fullName": "John Smith",
  "email": "john@example.com",
  "mobile": "01712345678",
  "userName": "johnsmith",
  "password": "Password@123",
  "confirmPassword": "Password@123"
}
Success:
{
  "success": true,
  "message": "User registered successfully."
}
সব field required; email valid; password minimum 8 characters; password ও confirmPassword match; username ও email unique।

2. `POST /api/Login`
Request:
{
  "userName": "johnsmith",
  "password": "Password@123"
}
Success:
{
  "success": true,
  "accessToken": "JWT_TOKEN",
  "refreshToken": "RANDOM_SECURE_TOKEN",
  "userName": "johnsmith",
  "fullName": "John Smith",
  "expiresIn": 3600
}
Invalid credentials-এ 401 দেবে।

3. `[Authorize] PUT /api/changePassword`
Request:
{
  "userName": "johnsmith",
  "oldPassword": "Password@123",
  "newPassword": "NewPassword@123",
  "confirmPassword": "NewPassword@123"
}
Success:
{
  "success": true,
  "message": "Password changed successfully."
}
JWT claim-এর username ও request username মিলতে হবে। Old password verify করবে; new password minimum 8 characters এবং confirmation match করে নতুন salt/hash save করবে।

CUSTOMER SERVICE EXACT ENDPOINT:

4. `[Authorize] GET /api/customer?page=1&pageSize=20&search=abc`
Response shape:
{
  "totalRecords": 1250,
  "page": 1,
  "pageSize": 20,
  "data": [
    {
      "customerId": 1,
      "customerName": "ABC Traders",
      "contactPerson": "Rahim Uddin",
      "mobile": "01812345678",
      "email": "abc@gmail.com",
      "address": "Dhaka",
      "status": true
    }
  ]
}
- `page >= 1`; `pageSize` 1 থেকে 100 validate করবে।
- Search customerName, contactPerson, mobile, email ও address-এ কাজ করবে।
- `OrderBy(customerId)`-এর পরে `Skip/Take` করবে।
- filtered totalRecords, requested page/pageSize এবং data return করবে।

ANGULAR FRONTEND:
- Angular 17.3.x module-based app হবে: `standalone: false`, `AppModule`, `AppRoutingModule`, SCSS।
- চারটি responsive UI/component থাকবে: Registration, Login, Change Password, Customers।
- Routes: `/register`, `/login`, `/change-password`, `/customers`; empty route login-এ redirect; wildcard redirect থাকবে।
- Reactive Forms দিয়ে required, valid email, minLength(8), password match এবং field-level errors দেখাবে।
- typed models/interfaces, `AuthService`, `CustomerService`, Auth Guard এবং HTTP interceptor থাকবে।
- environment file-এ `userApiUrl: 'http://localhost:5001'` ও `customerApiUrl: 'http://localhost:5002'` থাকবে।
- Login success-এ accessToken, refreshToken, userName ও fullName localStorage-এ save করে `/customers`-এ navigate করবে।
- Interceptor Bearer token attach করবে। Protected API থেকে 401 এলে session clear করে login-এ পাঠাবে।
- Guard দিয়ে customers ও change-password routes protect করবে।
- Navbar logged-out অবস্থায় Register/Login এবং logged-in অবস্থায় Customers/Change Password/Logout দেখাবে।
- Customers page প্রথমে `page=1&pageSize=10` load করবে এবং visible CSS spinner দেখাবে।
- Search করলে page/list reset হবে। `Load More`-এ next 10 append হবে। সব data এলে button hide হবে। Loading অবস্থায় duplicate request হবে না; failed request-এ page state ঠিক থাকবে।
- Table-এ ID, Customer Name, Contact Person, Mobile, Email, Address এবং Active/Inactive status থাকবে। Mobile-এ usable হবে। Loading, success, error ও empty states থাকবে।
- কোনো external UI/CSS library প্রয়োজন নেই; clean professional styling করবে।

REQUIRED SOURCE FILE COVERAGE:
- আটটি correct `.csproj` এবং সব ProjectReference/PackageReference
- Domain entities
- Application DTOs, interfaces ও services
- Infrastructure DbContexts/configuration/seed, repositories ও password hasher
- API controllers, JWT generator/configuration, `Program.cs`, `appsettings.json`, `launchSettings.json`
- Angular `package.json`, `angular.json`, `tsconfig*.json`, `src/main.ts`, `index.html`, environments, modules, routing, root component, models/services/guard/interceptor, চার page component এবং global/component styles
- default WeatherForecast files থাকবে না; `.spec.ts` files প্রয়োজন নেই
- optional convenience হিসেবে `Run-UserService.bat`, `Run-CustomerService.bat` ও `Run-Angular.bat` দিতে পারো, তবে project এগুলোর ওপর নির্ভর করবে না।

VERIFICATION WORKFLOW:
1. Source তৈরি শেষে solution/project references inspect করবে।
2. Environment-এ .NET 8 থাকলে root থেকে restore এবং `dotnet build CustomerManagementSystem.sln` চালাবে। Compile error হলে code fix করে আবার build করবে; 0 errors না হওয়া পর্যন্ত ZIP করবে না।
3. Node/npm থাকলে `client`-এ `npm install` এবং `npm run build` চালাবে। Angular/TypeScript/template error fix করে 0 errors নিশ্চিত করবে। `node_modules` ZIP-এ রাখবে না।
4. SQL Server না থাকলে database runtime test না করার কারণ report করবে; database/API code তবুও static cross-check করবে। SQL Server থাকলে services start করে Swagger JSON এবং endpoints smoke-test করার চেষ্টা করবে।
5. User registration → duplicate validation → login → JWT → authorized customer paging/search → change password flow যতটা environment permit করে test করবে। Test-এর পরে কোনো hard-coded demo user dependency রাখবে না।
6. `rg` বা equivalent search দিয়ে TODO, placeholder, accidental absolute path, wrong port 5000 এবং missing implementation check করবে।
7. Build tool/dependency unavailable হলে success বানিয়ে বলবে না। Exact missing tool ও PC-তে user কোন command চালাবে তা `DELIVERY-REPORT.md`-এ লিখবে।

USER'S PC RUN COMMANDS MUST WORK:
- Backend build:
  `dotnet restore CustomerManagementSystem.sln`
  `dotnet build CustomerManagementSystem.sln`
- UserService:
  `dotnet run --project src\UserService\UserService.Api\UserService.Api.csproj --launch-profile http`
- CustomerService:
  `dotnet run --project src\CustomerService\CustomerService.Api\CustomerService.Api.csproj --launch-profile http`
- Frontend:
  `cd client`
  `npm install`
  `npm start`
- Expected URLs:
  `http://localhost:5001/swagger`
  `http://localhost:5002/swagger`
  `http://localhost:4200`

README-তে Visual Studio 2022-এ `.sln` open করে `UserService.Api` ও `CustomerService.Api`-কে Multiple startup projects হিসেবে Start করার exact steps দেবে। Frontend VS Code terminal-এ চালানোর steps দেবে। Port already in use, Swagger 404, SQL connection failure, CORS এবং 401-এর সমাধানও থাকবে।

COMPLETION CHECKLIST:
- ZIP বানানোর আগে requirement-by-requirement audit করবে।
- Extracted structure expected root-এর সঙ্গে মেলে কিনা verify করবে।
- Backend ও Angular build result report করবে।
- Final response-এ শুধু কাজের summary, verification status, known limitation থাকলে সেটি, এবং `CustomerManagementSystem.zip` download attachment/link দেবে।
- ZIP artifact তৈরি ও attach না করে task complete বলবে না।

এখন project workspace-এ তৈরি করা শুরু করো, available tools দিয়ে verify করো এবং সম্পূর্ণ downloadable ZIP deliver না করা পর্যন্ত কাজ চালিয়ে যাও।
```

## Work Mode ব্যবহার

1. ChatGPT-তে **Work** mode নির্বাচন করো।
2. Cloud এবং local option দুটো থাকলে downloadable isolated project-এর জন্য Cloud ব্যবহার করা যায়; local tools/SQL Server দরকার হলে Work locally ব্যবহার করো।
3. উপরের prompt পাঠাও এবং task শেষ হওয়া পর্যন্ত Work-কে চলতে দাও।
4. ZIP পাওয়ার পরে extract করে আগে `README.md` ও `DELIVERY-REPORT.md` দেখো।

যদি Work code লিখে chat-এ থেমে যায় কিন্তু ZIP না দেয়, এই follow-up prompt পাঠাও:

```text
Chat-এ source code লেখা বন্ধ করো। ইতোমধ্যে তৈরি করা সব source file তোমার workspace-এ exact project structure অনুযায়ী materialize করো, missing file সম্পূর্ণ করো, available build checks চালিয়ে error fix করো, `node_modules/bin/obj` বাদ দিয়ে root folder-সহ `CustomerManagementSystem.zip` বানাও এবং download attachment হিসেবে দাও। ZIP attach না করা পর্যন্ত task complete বলবে না।
```
