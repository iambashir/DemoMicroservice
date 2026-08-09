@echo off
cd /d "%~dp0"
start "UserService" cmd /k Run-UserService.bat
start "CustomerService" cmd /k Run-CustomerService.bat
start "Angular Client" cmd /k Run-Angular.bat
