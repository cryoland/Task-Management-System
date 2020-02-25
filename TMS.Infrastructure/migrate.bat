@echo off
dotnet ef migrations add Init --startup-project ../TMS.WebUI-test/TMS.WebUI-test.csproj --context TMS.Infrastructure.Persistence.ApplicationDbContext -o Persistence/Migrations