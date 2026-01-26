# Звіт з аналізу SOLID принципів (SRP, OCP) в Open-Source проєкті

## 1. Обраний проєкт

- **Назва:** ASP.NET Core  
- **GitHub:** https://github.com/dotnet/aspnetcore  
- **Стан коду:** актуальна гілка `main` (код активно розвивається)

**ASP.NET Core** - великий open-source фреймворк на C#, який широко використовується для створення вебзастосунків та API. Проєкт має чітку модульну архітектуру, активно використовує інтерфейси та шаблони проєктування, що робить його зручним для аналізу принципів SOLID.

## 2. Аналіз SRP (Single Responsibility Principle)

### 2.1. Приклади дотримання SRP

### Клас: `Logger<T>`

**Відповідальність:**  
  Запис логів для конкретного типу `T`.

**Обґрунтування:**  
  - Клас не займається збереженням логів, форматуванням або вибором місця запису.  
  - Він лише делегує виклики провайдеру логування.  

```csharp
public class Logger<T> : ILogger<T>
{
    private readonly ILogger _logger;

    public Logger(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger(typeof(T));
    }

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception exception,
        Func<TState, Exception, string> formatter)
    {
        _logger.Log(logLevel, eventId, state, exception, formatter);
    }
}
```
**Єдина причина для зміни класу:**
зміна способу делегування логування.

### Клас: `HttpContext`

 **Відповідальність:**
- Представлення контексту одного HTTP-запиту.

**Обґрунтування:**
- Містить лише дані та сервіси, пов’язані з конкретним запитом.

- Бізнес-логіка обробки запиту винесена в middleware та контролери.

### 2.2. Приклад порушення SRP

### Клас: `Startup` 

**Множинні відповідальності:**

- Конфігурація сервісів (Dependency Injection)

- Налаштування HTTP pipeline

- Логіка середовища виконання (development / production)

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddDbContext<AppDbContext>();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
```
**Проблеми:**
Клас змінюється з багатьох причин і у великих проєктах може перетворюватися на God Object.

## 3. Аналіз OCP (Open/Closed Principle)
### 3.1. Приклади дотримання OCP
### Сценарій: `Middleware Pipeline`

**Механізм розширення:**
Middleware та делегати.

**Обґрунтування:**
Для додавання нової функціональності не потрібно змінювати існуючий код - достатньо створити новий middleware.

```csharp
public class CustomMiddleware
{
    private readonly RequestDelegate _next;

    public CustomMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);
    }
}
```

**Сценарій:** Логування (ILogger)

**Механізм розширення:**
Інтерфейс ILoggerProvider.

**Обґрунтування:**
Можна додавати нові способи логування (файл, база даних, хмарні сервіси) без зміни існуючого коду фреймворку.

### 3.2. Приклад порушення OCP
**Сценарій:** Обробка типів конфігурації через switch

```csharp
public void LoadConfig(string type)
{
    switch (type)
    {
        case "json":
            LoadJson();
            break;
        case "xml":
            LoadXml();
            break;
        case "yaml":
            LoadYaml();
            break;
    }
}
```

**Проблема:**
Додавання нового формату конфігурації вимагає зміни методу.

**Наслідки:**
Порушення принципу OCP та зниження масштабованості системи.

**Краще рішення:**
використання патернів Strategy або Factory з інтерфейсами.

## 4. Загальні висновки

ASP.NET Core загалом добре дотримується принципів SRP та OCP.
Архітектура фреймворку побудована на інтерфейсах і розширюваних компонентах, що дозволяє масштабувати систему без значних змін існуючого коду.
Основні порушення SRP та OCP зазвичай виникають у прикладному коді, а не в ядрі фреймворку.