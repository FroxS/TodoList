
## Task Management App

Aplikacja desktopowa umożiwiająca tworzenie, zarządzanie zadań typu TODO. Aplikacja wspiera wielojęzyczność (polski oraz angielski) oraz umożliwia automatyczne dostosowanie schematu kolorystycznego (jasny/ciemny) w zależności od ustawień systemu Windows lub wybranie własnego. Moża włączyć powiadomienia które uruchamiają się podczas uruchamiania aplikacji jeżli jakies zadanie jest na dziś.

## Funkcje

- **Tworzenie i zarządzanie zadaniami**: Możliwość dodawania, edytowania, usuwania i przeglądania zadań.
- **Automatyczne powiadomienia**: Dzięki integracji z systemem powiadomień Windows, powiadomienia są wysyłane gdy aplikacja się uruchamia i jest jakieś zdania do wykoanian na dziś.
- **Wielojęzyczność**: Obsługa języka polskiego i angielskiego z automatycznym wyborem języka w sekcji opcji.
- **Dynamiczne schematy kolorystyczne**: Jasny i ciemny schemat, dostosowany do motywu systemowego Windows.

## Wykorzystane technologie i biblioteki

### Technologie

- **.NET Core 6.0**
- **WPF**
- **EntityFramework**

### Baza danych

- Lokalna baza **Sqlite**

### Biblioteki

- **[CommunityToolkit.WinUI.Notifications](https://www.nuget.org/packages/CommunityToolkit.WinUI.Notifications/)** (v7.1.2)
- **[Fluent.Ribbon](https://www.nuget.org/packages/Fluent.Ribbon/)** (v9.0.3)
- **[MahApps.Metro](https://www.nuget.org/packages/MahApps.Metro/)** (v2.4.9)
- **[Microsoft.Extensions.Configuration](https://www.nuget.org/packages/Microsoft.Extensions.Configuration/)** (v6.0.0)
- **[Microsoft.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/)** oraz **[Microsoft.EntityFrameworkCore.Sqlite](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Sqlite/)** (v6.0.35)
- **[Prism.Unity](https://www.nuget.org/packages/Prism.Unity/)** (v8.1.97)
- **[Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/)** (v13.0.1)
- **Testy jednostkowe**:
  - **[Microsoft.NET.Test.Sdk](https://www.nuget.org/packages/Microsoft.NET.Test.Sdk/)** (v17.0.0)
  - **[nunit](https://www.nuget.org/packages/nunit/)** (v3.13.2)
  - **[NUnit3TestAdapter](https://www.nuget.org/packages/NUnit3TestAdapter/)** (v4.2.1)

### Wzorce projektowe

- **MVVM** - Model-View-ViewModel
- **DDD** - Domain-Driven Design

## Instalacja

1. **Wymagania**: .NET Core 6.0 SDK (https://dotnet.microsoft.com/download/dotnet/6.0)
