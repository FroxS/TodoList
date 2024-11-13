
## Aplikacja TODO

Aplikacja desktopowa umożiwiająca tworzenie, zarządzanie zadań typu TODO. Aplikacja wspiera wielojęzyczność (polski oraz angielski) oraz umożliwia automatyczne dostosowanie schematu kolorystycznego (jasny/ciemny) w zależności od ustawień systemu Windows lub wybranie własnego. Moża włączyć powiadomienia, które uruchamiają się podczas uruchamiania aplikacji jeżli jakies zadanie jest na dziś.

## Instalacja

1). Wymagania: 
 - .NET Core 6.0 SDK (https://dotnet.microsoft.com/download/dotnet/6.0)

2). Najnowsza wersja do pobrania 1.0.0.5 **[TodoList.zip](https://github.com/FroxS/TodoList/releases/download/version-1.0.0.5/TodoList.zip)**

## Funkcje

- **Tworzenie i zarządzanie zadaniami**: Możliwość dodawania, edytowania, usuwania i przeglądania zadań.
- **Automatyczne powiadomienia**: Dzięki integracji z systemem powiadomień Windows, powiadomienia są wysyłane gdy aplikacja się uruchamia i jest jakieś zdania do wykoanian na dziś.
- **Wielojęzyczność**: Obsługa języka polskiego i angielskiego z automatycznym wyborem języka w sekcji opcji.
- **Dynamiczne schematy kolorystyczne**: Jasny i ciemny schemat, dostosowany do motywu systemowego Windows.

## Technologie

- **.NET Core 6.0**
- **WPF**
- **EntityFramework**

## Baza danych

- Lokalna baza **Sqlite**

## Biblioteki

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

## Wzorce projektowe

- **MVVM** - Model-View-ViewModel
- **DDD** - Domain-Driven Design

## Serwis zadań

`TaskItemService` klasa zarządzająca operacjami związanymi z elementami zadań (`TaskItem`). Rozszerza ona klasę `BaseService`, która jest generyczną klasą bazową obsługującą operacje CRUD (Create, Read, Update, Delete).

## Publiczne metody

### `GetTasksForToday()`

```csharp
public List<TaskItem> GetTasksForToday()
```
Metoda zwraca listę wszystkich zadań TaskItem, które mają być wykonane dzisiaj. Filtruje zadania, które:
 - nie są oznaczone jako ukończone
  ```csharp
 IsCompleted
 ```
 - mają dzisiejszą datę 
 ```csharp
 isToday().
 ```

 ### `AddSubItem()`

```csharp
public List<TaskItem> GetTasksForToday(TaskItem item, TaskISubtem child)
```
Metoda dodaje pozdzane `child` do zadania `item` oraz ustawia własności w rodzicu `item`

 ### `RemoveSubItem()`

```csharp
public List<TaskItem> RemoveSubItem(TaskItem item, TaskISubtem child)
```
Metoda usuwa podzadanie `child` z zadania `item`

### CRUD

 ### `AddAsync()`

```csharp
public virtual async Task<TaskItem> AddAsync(TaskItem item)
```
Metoda dodaje zadanie asynchronicznie i zwraca to zadanie `item`

 ### `Add()`

```csharp
public virtual TaskItem Add(TaskItem item)
```
Metoda dodaje zadanie synchronicznie i zwraca to zadanie `item`

 ### `Update()`

```csharp
public virtual void Update(TaskItem item)
```
Metoda aktualizuje pola w zadaniu

### `DeleteAsync()`

```csharp
public virtual async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
```
Metoda szuka zadania asynchrnonicznie w bazie po Guid `id` i usuwa jeżeli znajdzie.

### `Delete()`

```csharp
public virtual bool Delete(Guid id)
```
Metoda szuka zadania synchrnonicznie w bazie po Guid `id` i usuwa jeżeli znajdzie.

### `GetAllAsync()`

```csharp
public virtual async Task<List<TaskItem>> GetAllAsync(CancellationToken cancellationToken = default)
```
Metoda listuje wszystkie zadania w bazie asynchronicznie

### `GetAll()`

```csharp
public virtual List<TaskItem> GetAll()
```
Metoda listuje wszystkie zadania w bazie synchronicznie

### `GetByIdAsync()`

```csharp
public virtual async Task<TaskItem> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
```
Metoda wyszukuje asynchronicznie w bazie  zadanie o podanym id `Guid id` i je zwraca jeżeli znajdzie

### `GetById()`

```csharp
public virtual TaskItem GetById(Guid id)
```
Metoda wyszukuje synchronicznie w bazie  zadanie o podanym id `Guid id` i je zwraca jeżeli znajdzie

### `Exist()`

```csharp
public virtual bool Exist(Guid id)
```
Metoda sprawdza czy element o id `Guid id` istnieje

### `SaveAsync()`

```csharp
public virtual async Task<bool> SaveAsync(CancellationToken cancellationToken = default(CancellationToken))
```
Metoda zapusuje asynchronicznie niezapisane jeszcze dane do bazy dancyh

### `Save()`

```csharp
public virtual async Task<bool> SaveAsync(CancellationToken cancellationToken = default(CancellationToken))
```
Metoda zapusuje synchronicznie niezapisane jeszcze dane do bazy dancyh

## Testy NUnit

Testy uruchamiamy 

```bash
  dotnet test TodoList.Core.Tests.NUnit/TodoList.Core.Tests.NUnit.csproj
```

### `FileServiceTests`

```csharp
TestSaveFile()
```
Sprawdza czy można zapisać plik konfiguracyjny .json

```csharp
TestReadFile()
```
Sprawdza czy można odczytać plik konfiguracyjny .json

```csharp
TestDeleteFile()
```
Sprawdza czy można usunąć plik konfiguracyjny .json

### `TaskItemRepositoryTests`

```csharp
AddAsync_ShouldAddTaskItem()
```
Sprawdza czy dodawanie do bazy zadań działa poprawnie

```csharp
GetByIdAsync_ShouldReturnTaskItem()
```
Sprawdza czy dodawanie do bazy a następnie wyszukanie zadania działa poprawnie

```csharp
DeleteAsync_ShouldDeleteTaskItem()
```
Sprawdza czy usuwane zadań z bazy działa poprawnie

### `TaskItemServiceTests`

```csharp
GetTasksForToday_ShouldReturnTasksForToday()
```

Sprawdza poprawność działania metody do wyciągania zadań nieukończonych na dzisiaj

```csharp
AddAsync_ShouldAddNewTaskItem()
```

Sprawdza poprawność działania dodawanie oraz usuwania zadań z poziomu serwisu.

```csharp
AddAsync_ShouldCheckIsTaskItemToday()
```

Sprawdza czy metoda w TaskItem spradzajaca czy zadanie jest na dziś działa poprawnie









