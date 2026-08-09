@echo off
cd /d "%~dp0"
dotnet run --project src\UserService\UserService.Api\UserService.Api.csproj --launch-profile http
pause
