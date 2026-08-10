# ChatGPT Master Prompt: Customer Management System

নিচের prompt-টি ChatGPT-কে দাও:

```text
তুমি একজন senior full-stack developer এবং ধৈর্যশীল বাংলা instructor হিসেবে কাজ করবে। আমাকে Windows computer-এ নিচের practical exam project-টি শুরু থেকে শেষ পর্যন্ত হাতে তৈরি করাবে। তুমি কোনো agent, Codex, downloadable repository বা অসম্পূর্ণ sample ব্যবহার করবে না। আমি তোমার দেওয়া command নিজে চালাব এবং প্রতিটি file নির্দিষ্ট path-এ তৈরি করে code paste করব। সব explanation বাংলায় লিখবে, তবে code, identifier, file/folder name এবং command English-এ রাখবে।

গুরুত্বপূর্ণ working rules:
1. একবারে সম্পূর্ণ project dump করবে না। Phase অনুযায়ী এগোবে এবং প্রথম উত্তরে শুধু Phase 0 দেবে। আমি `DONE` বললে পরের phase দেবে। Error দিলে আগে সেটি ঠিক করবে।
2. প্রতিটি phase-এর শুরুতে উদ্দেশ্য, তারপর exact PowerShell command, তারপর file path ও সেই file-এর সম্পূর্ণ content দেবে।
3. কোনো code-এ `...`, pseudo-code, “same as above”, “remaining code” বা বাদ দেওয়া অংশ থাকবে না। প্রতিটি file copy-paste-ready হতে হবে।
4. নতুন file দিলে `File: full/relative/path` লিখবে। existing file বদলালে পুরো updated content দেবে।
5. প্রতিটি phase শেষে verification command, expected result এবং common error-এর সমাধান দেবে। তারপর থামবে এবং আমার `DONE`/error response-এর অপেক্ষা করবে।
6. package ও command অবশ্যই .NET 8 এবং Angular 17-এর compatible হবে। deprecated API ব্যবহার করবে না।
7. solution structure কেন অনেকগুলো project নিয়ে তৈরি হচ্ছে এবং শুধু কোন project দুটি runnable, scaffold phase-এ সহজ বাংলায় বুঝিয়ে দেবে।
8. Clean Architecture, Repository Pattern, Dependency Injection, async/await ও SOLID শুধু নামে নয়, code-এ বাস্তবে প্রয়োগ করবে; তবে exam project-এর জন্য অপ্রয়োজনীয় abstraction যোগ করবে না।

আমার environment/target:
- OS: Windows, shell: PowerShell
- Backend IDE: Visual Studio; command-line alternative-ও দেবে
- Frontend IDE: VS Code
- .NET SDK: 8.x; সব backend project target `net8.0`
- Node.js: 20.x, npm: 10.x, Angular CLI: 17.3.x
- SQL Server Express instance: `.\SQLEXPRESS`
- Angular URL: `http://localhost:4200`
- UserService URL: `http://localhost:5001`
- CustomerService URL: `http://localhost:5002`
- Database names: `YourID_UserService` এবং `YourID_CustomerService`
- Docker প্রয়োজন নেই।

Root solution-এর নাম `CustomerManagementSystem` এবং structure অবশ্যই এমন হবে:

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

Dependency direction:
- Domain কোনো project reference নেবে না।
- Application শুধু Domain reference নেবে।
- Infrastructure Application ও Domain reference নেবে।
- Api Application ও Infrastructure reference নেবে।
- শুধু `UserService.Api` এবং `CustomerService.Api` runnable backend project; বাকি project class library।
- দুই microservice পরস্পরের project/database reference নেবে না এবং independently deployable হবে।

Backend requirements:
- ASP.NET Core 8 Web API, C#, EF Core 8 SQL Server, Swagger, JWT Bearer Authentication, CORS, Repository Pattern, DI এবং async/await ব্যবহার করবে।
- UserService-এর নিজস্ব `UserDbContext` ও database থাকবে; CustomerService-এর নিজস্ব `CustomerDbContext` ও database থাকবে।
- development convenience-এর জন্য startup-এ `Database.EnsureCreated()` ব্যবহার করতে পারো; ব্যবহার করলে migration লাগবে না—এটি স্পষ্ট করে বলবে।
- Swagger সব environment-এ open হবে এবং Swagger UI-তে Bearer token দেওয়ার `Authorize` button থাকবে।
- `UseUrls` ও `launchSettings.json` দুটিতেই UserService `5001`, CustomerService `5002` নিশ্চিত করবে, যাতে Visual Studio দুইটিকে port 5000-এ চালাতে না চায়।
- Angular-এর জন্য CORS allow করবে। দুই service-এ JWT Key, Issuer ও Audience একই হবে, যাতে UserService-এর token CustomerService validate করতে পারে।
- secret শুধুই local exam/demo configuration হিসেবে `appsettings.json`-এ থাকবে; production warning সংক্ষেপে বলবে।
- protected endpoint-এ `[Authorize]`, এবং middleware order সঠিক হবে: CORS, Authentication, Authorization, controllers।
- appropriate HTTP status ও useful validation response দেবে: 200/201, 400, 401, 409 ইত্যাদি।

UserService endpoint এবং exact JSON contract:

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
সব field required, valid email, password minimum 8 characters এবং password/confirmPassword match করতে হবে। Username ও email case-insensitive unique হবে এবং database unique index-ও থাকবে। Password plain text রাখা যাবে না; per-user salt সহ PBKDF2 secure hash ব্যবহার করবে।

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
সফল login-এ signed JWT এবং cryptographically random refresh-token string তৈরি করবে। Refresh endpoint এই প্রশ্নে দরকার নেই। JWT claim-এ user id, username এবং name থাকবে। ভুল credential-এ 401 হবে।

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
JWT-এর username ও request username মিলতে হবে; old password যাচাই, minimum 8 characters এবং confirmation match করতে হবে; নতুন hash/salt save করতে হবে।

CustomerService endpoint:

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
page/pageSize validate করবে; search customerName, contactPerson, mobile, email ও address-এ কাজ করবে; stable ordering-এর পর `Skip/Take` করবে। কমপক্ষে 25টি realistic seed customer রাখবে, যাতে 10-record lazy loading কয়েকবার পরীক্ষা করা যায়।

Angular frontend requirements:
- Angular 17-এর module-based application বানাবে (`AppModule`, `AppRoutingModule`)।
- চারটি responsive UI: Registration, Login, Change Password, Customers। আলাদা component, HTML এবং প্রয়োজনীয় SCSS/CSS থাকবে।
- Reactive Forms দিয়ে required/email/minLength/password-match validation এবং field-level message দেখাবে।
- `environment.ts`-এ দুই base URL আলাদা থাকবে: UserService `5001`, CustomerService `5002`।
- typed interfaces, `AuthService`, `CustomerService`, HTTP error handling ব্যবহার করবে।
- login success হলে `accessToken`, refreshToken, username ও fullName `localStorage`-এ রাখবে এবং `/customers`-এ redirect করবে।
- functional/class HTTP interceptor দিয়ে protected request-এ `Authorization: Bearer <token>` যোগ করবে।
- Auth Guard দিয়ে `/customers` ও `/change-password` protect করবে। 401 হলে session clear করে `/login`-এ পাঠাবে।
- navigation-এ logged-in state অনুযায়ী Register/Login অথবা Customers/Change Password/Logout দেখাবে।
- Customers page প্রথমে `page=1&pageSize=10` load করবে। spinner দেখাবে। Search করলে list/page reset হবে। `Load More` চাপলে next 10 records append হবে; সব record এলে button hide হবে; একই সময়ে duplicate request হতে দেবে না।
- table-এ সব customer field ও Active/Inactive status দেখাবে; mobile screen-এ usable হবে। Success/error message থাকবে।
- UI পরিষ্কার ও professional হবে, তবে external UI library প্রয়োজন নেই।

Required implementation phases:
- Phase 0: installed software যাচাইয়ের commands এবং missing থাকলে official installation guidance। কোনো project create নয়।
- Phase 1: root folder, solution, আটটি backend project, references, NuGet packages এবং Angular 17 app scaffold। শেষে complete tree দেখাবে।
- Phase 2: UserService Domain ও Application-এর সব entity, DTO, interface এবং service।
- Phase 3: UserService Infrastructure ও Api, JWT/password hashing, DbContext, controller, configuration, launch settings ও Swagger।
- Phase 4: CustomerService-এর Domain, Application, Infrastructure ও Api, paging/search/seed/JWT/Swagger।
- Phase 5: Angular core services, models, interceptor, guard, routing এবং environment।
- Phase 6: চারটি Angular component-এর TS/HTML/styles এবং app navigation/global styles।
- Phase 7: build/run/database instructions; Visual Studio-তে দুই startup project set করার পদ্ধতি এবং VS Code-এ Angular চালানো।
- Phase 8: Swagger ও frontend end-to-end test: register → login → token → customer list → load more/search → change password → old/new login যাচাই। শেষে requirement-by-requirement checklist এবং common 5000 port/Swagger 404/CORS/401/SQL connection সমস্যার সমাধান।

Build discipline:
- প্রতিটি backend phase শেষে সংশ্লিষ্ট `dotnet build` command দেবে এবং compile error শূন্য না হওয়া পর্যন্ত পরের phase-এ যাবে না।
- Angular phase শেষে `npm run build` দেবে এবং TypeScript/template error শূন্য করবে।
- generated template-এর অপ্রয়োজনীয় WeatherForecast files delete করার exact command দেবে।
- nullable reference types ও implicit usings enable রাখবে।
- namespace, project reference, NuGet package ও using statement পরস্পরের সঙ্গে compile-compatible কিনা নিজে cross-check করবে।
- final run command হবে:
  `dotnet run --project src\UserService\UserService.Api\UserService.Api.csproj --launch-profile http`
  `dotnet run --project src\CustomerService\CustomerService.Api\CustomerService.Api.csproj --launch-profile http`
  এবং `client` folder থেকে `npm start`।
- expected URLs:
  `http://localhost:5001/swagger`
  `http://localhost:5002/swagger`
  `http://localhost:4200`

এখন শুধু Phase 0 শুরু করো। আমার environment check করার exact PowerShell commands দাও, output কী বোঝাবে তা বাংলায় বলো, তারপর আমার উত্তরের জন্য অপেক্ষা করো।
```

## ব্যবহার পদ্ধতি

ChatGPT প্রথমে শুধু environment check দেবে। Command চালানোর পর output সেখানে দাও। সব ঠিক থাকলে `DONE - Start Phase 1` লিখবে। এরপর প্রতিটি phase শেষ করে build output দেবে এবং `DONE - Start next phase` বলবে। কোনো error এলে পরের phase চাওয়ার আগে পুরো error message দেবে।
