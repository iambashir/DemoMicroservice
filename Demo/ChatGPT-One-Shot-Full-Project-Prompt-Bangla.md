# ChatGPT One-Shot Full Project Prompt

নিচের সম্পূর্ণ prompt-টি ChatGPT-কে দাও:

```text
তুমি একজন senior .NET ও Angular full-stack developer। নিচে দেওয়া practical exam project-এর সম্পূর্ণ, compile-ready solution আমাকে একটি response-এই লিখে দাও। আমাকে কোনো প্রশ্ন করবে না, কোনো phase-এ থামবে না এবং আমার confirmation-এর জন্য অপেক্ষা করবে না। প্রথমে full project tree, তারপর project creation commands, তারপর tree-এর ক্রম অনুযায়ী প্রতিটি প্রয়োজনীয় file-এর সম্পূর্ণ code দেবে। আমি folder/file তৈরি করে শুধু code copy করব।

OUTPUT RULES — অবশ্যই মানবে:
1. সব explanation বাংলায় হবে; code, command, namespace, identifier ও UI text English-এ হবে।
2. একটি response-এই পুরো উত্তর দেবে। Phase, `DONE`, confirmation বা follow-up ব্যবহার করবে না।
3. কোনো code-এ `...`, pseudo-code, TODO, placeholder implementation, “same as above”, “existing code রাখুন” বা omitted section থাকবে না।
4. প্রথমে একটি code block-এ complete folder/file tree দেবে। `bin`, `obj`, `.angular`, `node_modules` tree-তে দেবে না।
5. তারপর exact Windows PowerShell scaffold/reference commands দেবে।
6. তারপর প্রতিটি file-এর আগে `File: relative/path` heading এবং নিচে language-tag-সহ code block-এ সেই file-এর সম্পূর্ণ content দেবে।
7. সব `.csproj`, backend source/configuration, Angular source/environment এবং প্রয়োজনীয় root configuration দিতে হবে। CLI নিজে generate করে এমন অপরিবর্তিত binary বা dependency content দেবে না।
8. সব namespace, using, project reference, package version, route, JSON property ও Angular import cross-check করে compile-compatible রাখবে।
9. উত্তরের শেষে build/run commands, Visual Studio startup instructions, test sequence ও short troubleshooting দেবে। অপ্রয়োজনীয় theory দিয়ে output বড় করবে না; code বাদও দেবে না।

TECHNOLOGY ও LOCAL CONFIGURATION:
- Windows PowerShell
- ASP.NET Core 8 Web API, C#, target framework `net8.0`
- Entity Framework Core SQL Server `8.0.8`
- `Microsoft.AspNetCore.Authentication.JwtBearer` `8.0.8`
- `Swashbuckle.AspNetCore` `6.6.2`
- SQL Server Express: `.\SQLEXPRESS`
- Angular CLI/Angular `17.3.x`, module-based application, SCSS
- Node.js 20.x
- Docker নয়
- Angular URL: `http://localhost:4200`
- UserService: `http://localhost:5001`
- CustomerService: `http://localhost:5002`
- User database: `YourID_UserService`
- Customer database: `YourID_CustomerService`

SOLUTION ও PROJECT STRUCTURE:

CustomerManagementSystem/
  CustomerManagementSystem.sln
  src/
    UserService/
      UserService.Domain/
      UserService.Application/
      UserService.Infrastructure/
      UserService.Api/
    CustomerService/
      CustomerService.Domain/
      CustomerService.Application/
      CustomerService.Infrastructure/
      CustomerService.Api/
  client/

এখানে মোট আটটি backend project থাকবে। শুধু `UserService.Api` এবং `CustomerService.Api` runnable Web API; অন্য ছয়টি class library। Dependency direction:
- Domain: কোনো project reference নয়
- Application: শুধু নিজ service-এর Domain
- Infrastructure: নিজ service-এর Application + Domain
- Api: নিজ service-এর Application + Infrastructure
- একটি microservice অন্য microservice-এর project বা database reference করবে না; দুটিই independently deployable হবে।

ARCHITECTURE ও BACKEND RULES:
- Clean Architecture, Repository Pattern, Dependency Injection, async/await, SOLID বাস্তবে প্রয়োগ করবে। অপ্রয়োজনীয় CQRS/MediatR যোগ করবে না।
- প্রতিটি service-এর আলাদা DbContext, repository এবং database থাকবে।
- development/exam convenience-এর জন্য startup-এ `Database.EnsureCreated()` ব্যবহার করবে এবং Customer database প্রথমবার তৈরি হলে seed data insert হবে। EF migration প্রয়োজন হবে না।
- DTO validation-এর জন্য Data Annotations এবং প্রয়োজনীয় business validation service layer-এ থাকবে।
- Username ও email case-insensitive unique হবে; database unique index-ও থাকবে।
- password কখনো plain text নয়। built-in `Rfc2898DeriveBytes.Pbkdf2` দিয়ে random per-user salt, SHA256, যথেষ্ট iteration এবং fixed-time comparison ব্যবহার করবে; hash/salt Base64 হিসেবে রাখবে।
- JWT token HS256 signed হবে, expiry 3600 seconds। User id, username এবং full name claim থাকবে।
- দুই API-এর `appsettings.json`-এ একই local demo JWT Key, Issuer ও Audience থাকবে, যাতে UserService-এর token CustomerService validate করতে পারে। Key কমপক্ষে 32 bytes হবে।
- Refresh token cryptographically secure random string হবে; refresh endpoint দরকার নেই।
- Swagger সব environment-এ চালু থাকবে এবং JWT Bearer `Authorize` button থাকবে।
- CORS policy Angular `http://localhost:4200` ও `http://127.0.0.1:4200` allow করবে।
- `UseUrls` এবং `launchSettings.json` উভয় জায়গায় UserService `5001` ও CustomerService `5002` fix করবে, যাতে Visual Studio default 5000 ব্যবহার না করে। HTTP profile default রাখবে; local setup-এ অপ্রয়োজনীয় HTTPS redirect সমস্যা তৈরি করবে না।
- middleware order সঠিক হবে: Swagger, CORS, Authentication, Authorization, MapControllers।
- protected endpoint-এ `[Authorize]` এবং public endpoint-এ `[AllowAnonymous]`/public access থাকবে।
- validation/business error অনুযায়ী 400, duplicate-এ 409, invalid login/token-এ 401, success-এ 200 বা registration-এ 201 return করবে। Response JSON camelCase হবে।

USER SERVICE EXACT API CONTRACT:

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
Success response:
{
  "success": true,
  "message": "User registered successfully."
}
সব field required, valid email, password minimum 8 characters এবং password/confirmPassword match করতে হবে। Username ও email unique হবে।

2. `POST /api/Login`
Request:
{
  "userName": "johnsmith",
  "password": "Password@123"
}
Success response:
{
  "success": true,
  "accessToken": "JWT_TOKEN",
  "refreshToken": "RANDOM_SECURE_TOKEN",
  "userName": "johnsmith",
  "fullName": "John Smith",
  "expiresIn": 3600
}
Invalid credential-এ 401 response হবে।

3. `[Authorize] PUT /api/changePassword`
Request:
{
  "userName": "johnsmith",
  "oldPassword": "Password@123",
  "newPassword": "NewPassword@123",
  "confirmPassword": "NewPassword@123"
}
Success response:
{
  "success": true,
  "message": "Password changed successfully."
}
JWT claim-এর username ও request username অবশ্যই মিলবে। Old password verify, new password minimum 8 characters ও confirmation match করে নতুন salt/hash save করবে।

CUSTOMER SERVICE EXACT API CONTRACT:

4. `[Authorize] GET /api/customer?page=1&pageSize=20&search=abc`
Response:
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
- `page >= 1`, `pageSize` 1–100 validate করবে।
- search customerName, contactPerson, mobile, email ও address-এ কাজ করবে।
- stable `OrderBy(customerId)`-এর পরে `Skip/Take` করবে।
- response-এ filtered `totalRecords`, requested page/pageSize এবং data থাকবে।
- অন্তত 25টি realistic seed customer দেবে, যাতে 10-record Load More কয়েকবার পরীক্ষা করা যায়।

ANGULAR FRONTEND REQUIREMENTS:
- Angular 17 module-based app (`standalone: false`) হবে। `AppModule` ও `AppRoutingModule` থাকবে।
- চারটি responsive UI/component: User Registration, User Login, Change Password, Customers। প্রতিটির `.ts`, `.html`, প্রয়োজনীয় `.scss` file দেবে।
- route: `/register`, `/login`, `/change-password`, `/customers`; empty route `/login`-এ যাবে; wildcard redirect থাকবে।
- Reactive Forms দিয়ে required, email, minLength(8), password matching এবং field-level validation message দেখাবে।
- typed request/response/customer interfaces থাকবে।
- `environment.ts`-এ `userApiUrl: 'http://localhost:5001'` ও `customerApiUrl: 'http://localhost:5002'` থাকবে।
- `AuthService`, `CustomerService`, Auth Guard ও HTTP interceptor থাকবে।
- login success হলে accessToken, refreshToken, userName ও fullName `localStorage`-এ save করে `/customers`-এ navigate করবে।
- interceptor protected API calls-এ `Authorization: Bearer <token>` দেবে। 401 হলে stored session clear করে `/login`-এ পাঠাবে। Login/registration request-এ token দরকার নেই।
- guard দিয়ে `/customers` ও `/change-password` protect করবে।
- navbar logged-out অবস্থায় Register/Login এবং logged-in অবস্থায় Customers/Change Password/Logout দেখাবে।
- logout session clear করে login-এ পাঠাবে।
- Customers page প্রথমে `page=1&pageSize=10` fetch করবে এবং fetching-এর সময় visible CSS spinner দেখাবে।
- Search submit করলে page/list reset হবে। `Load More`-এ পরের 10 records append হবে। সব records load হলে button hide হবে। Loading অবস্থায় duplicate request বন্ধ থাকবে এবং failed Load More হলে page ভুলভাবে এগিয়ে থাকবে না।
- customer table-এ ID, Customer Name, Contact Person, Mobile, Email, Address ও Active/Inactive Status থাকবে। Mobile viewport-এ horizontal scroll বা responsive layout থাকবে।
- forms ও table পরিষ্কার professional styling পাবে; external UI/CSS package যোগ করবে না। Success/error/loading/empty state থাকবে।

FILES MUST INCLUDE:
- আটটি `.csproj` file এবং correct ProjectReference/PackageReference
- সব Domain entities
- সব Application DTOs, interfaces ও services
- সব Infrastructure DbContexts, EF configuration/seed, repositories এবং password hasher
- দুই Api-এর controllers, JWT generator/configuration, `Program.cs`, `appsettings.json`, `launchSettings.json`
- Angular-এর `package.json`, `angular.json`, `tsconfig*.json`, `src/main.ts`, `index.html`, environment, app module/routing/component, core models/services/guard/interceptor, চার page component এবং global styles
- generated logo/favicon test asset দরকার নেই; Angular default test `.spec.ts` files দরকার নেই
- README-তে architecture, prerequisite, build/run এবং demo test flow থাকবে

SCAFFOLD/BUILD/RUN:
- PowerShell command দিয়ে folders, solution, 8 projects, references, packages ও Angular app তৈরির সঠিক order দেখাবে। `dotnet new sln --format sln` সমর্থিত না হলে compatible alternative উল্লেখ করবে।
- template-এর WeatherForecast files remove করার commands দেবে।
- backend build: `dotnet build CustomerManagementSystem.sln`
- frontend build: `cd client` তারপর `npm install` ও `npm run build`
- UserService run:
  `dotnet run --project src\UserService\UserService.Api\UserService.Api.csproj --launch-profile http`
- CustomerService run:
  `dotnet run --project src\CustomerService\CustomerService.Api\CustomerService.Api.csproj --launch-profile http`
- Angular run: `cd client` তারপর `npm start`
- URLs:
  `http://localhost:5001/swagger`
  `http://localhost:5002/swagger`
  `http://localhost:4200`
- Visual Studio-তে solution খুলে UserService.Api ও CustomerService.Api-কে Multiple startup projects হিসেবে Start করার exact steps সংক্ষেপে দেবে। Angular VS Code terminal থেকে চলবে।

FINAL VERIFICATION:
শেষে concise end-to-end checklist দেবে: database auto-create → register → duplicate validation → login → JWT copy/Swagger authorize → authorized customer paging/search → Angular login/token/guard → initial 10/load more → change password → old password fails/new password succeeds। Port already in use, Swagger 404, SQL connection failure, CORS এবং 401-এর short fixes দেবে।

এখন কোনো প্রশ্ন না করে এবং কোনো intermediate confirmation না চেয়ে, উপরের নিয়ম অনুযায়ী complete tree, commands ও প্রতিটি file-এর full code একটি response-এই লেখা শুরু করো।
```

## গুরুত্বপূর্ণ নোট

এই prompt একটি response-এ full code চায়। ChatGPT-এর response-length limit-এর কারণে উত্তর কেটে গেলে শুধু নিচের continuation prompt দেবে:

```text
আগের উত্তরের শেষ সম্পূর্ণ file-এর পর থেকে continuation দাও। আগে দেওয়া কোনো file repeat করবে না। একই format-এ বাকি সব file-এর full code, build/run instructions এবং final verification শেষ করো। কোনো অংশ সংক্ষেপ করবে না।
```
