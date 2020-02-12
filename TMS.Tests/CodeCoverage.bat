@echo off

REM Clean and build the project
dotnet clean
dotnet build /p:DebugType=Full

REM Instrument assemblies in our test project to detect hits for source files from our main project
minicover instrument --workdir ../ --assemblies TMS.Tests/**/bin/**/*.dll --sources TMS/**/*.cs --exclude-sources TMS/*.cs --exclude-sources TMS/**/Migrations/*cs

REM Reset previous counters
minicover reset --workdir ../

REM Run the tests
dotnet test --no-build

REM Uninstrument assemblies in case we want to deploy
minicover uninstrument --workdir ../

REM Print the console report
minicover report --workdir ../ --threshold 70