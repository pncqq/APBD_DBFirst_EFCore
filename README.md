# 📄 APBD_DBFirst_EFCore

Aplikacja webowa zbudowana w podejściu **Database First** z wykorzystaniem **Entity Framework Core**. Projekt opracowany w ramach zajęć **APBD (Aplikacje Baz Danych)**.

## 📂 Zawartość repozytorium

- `Controllers/` – kontrolery obsługujące żądania HTTP
- `DAL/` – wygenerowany model bazy danych (Database First)
- `Repositories/` – logika dostępu do danych
- `Models/`, `Enums/` – dodatkowe modele i typy pomocnicze
- `Program.cs` – konfiguracja aplikacji i pipeline
- `appsettings.json`, `appsettings.Development.json` – konfiguracja środowiska
- `WebApplication1.sln`, `WebApplication1.csproj` – pliki projektu

## ⚙️ Technologie

- ASP.NET Core Web API
- Entity Framework Core (Database First)
- SQL Server

## 🧬 Funkcjonalności

- Import struktury bazy danych do kodu za pomocą `Scaffold-DbContext`
- Wzorzec Repository do oddzielenia warstwy danych
- Prosty zestaw endpointów REST do pracy z encjami z bazy danych

## 🚀 Jak uruchomić

1. Otwórz projekt w Visual Studio (`WebApplication1.sln`)
2. Skonfiguruj connection string w `appsettings.json`
3. Uruchom projekt (`dotnet run` lub F5)

## 📈 Możliwe rozszerzenia

- Dodanie DTOs i mapowania (np. AutoMapper)
- Walidacja danych wejściowych (ModelState)
- Swagger UI do testowania endpointów

## 👨‍💼 Autor
**Filip Michalski**  
Projekt wykonany w ramach kursu APBD jako implementacja architektury EF Core w podejściu Database First.
