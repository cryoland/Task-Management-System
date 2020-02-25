@echo off

set datetimef=%date:~-4%-%date:~3,2%-%date:~0,2%_%time:~0,2%-%time:~3,2%-%time:~6,2%
set exec_proj=TMS.WebUI

REM Creating migrations
dotnet ef migrations add Migration_%datetimef% --startup-project ../%exec_proj%/%exec_proj%.csproj --context TMS.Infrastructure.Persistence.ApplicationDbContext -o Persistence/Migrations