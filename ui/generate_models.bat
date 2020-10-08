rmdir "./src/app/models/generated" /s /q
cd ..\src\Server.Dtos
dotnet build
cd ..\..\utils\Server.ModelsGenerator
dotnet run ts
cd ..\ui