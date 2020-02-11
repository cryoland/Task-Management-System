@echo off

REM Clean and build the project
dotnet clean
dotnet build /p:DebugType=Full

REM Instrument assemblies in our test project to detect hits for source files from our main project
dotnet minicover instrument --workdir ../ --assemblies TMS.Tests/**/bin/**/*.dll --sources TMS/**/*.cs --exclude-sources TMS/*.cs

REM Reset previous counters
dotnet minicover reset --workdir ../

REM Run the tests
dotnet test --no-build

REM Uninstrument assemblies in case we want to deploy
dotnet minicover uninstrument --workdir ../

REM Print the console report
dotnet minicover report --workdir ../ --threshold 70