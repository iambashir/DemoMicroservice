@echo off
cd /d "%~dp0"
dotnet run --project src\CustomerService\CustomerService.Api\CustomerService.Api.csproj --launch-profile http
pause
