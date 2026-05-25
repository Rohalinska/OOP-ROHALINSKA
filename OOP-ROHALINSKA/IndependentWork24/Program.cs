using System;
using System.Collections.Generic;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("==================================================");
Console.WriteLine("=== ЗАПУСК ІНТЕГРАЦІЙНОГО РІШЕННЯ (ВАРИАНТ 14) ===");
Console.WriteLine("==================================================");

Chapter book = new Chapter("Програмування на C# для профі");
Chapter ch1 = new Chapter("Вступ до структурних патернів");
ch1.Add(new Page(1, "Привіт світ! Це перша сторінка нашої лабораторної роботи.")); 
ch1.Add(new Page(2, "Патерн Компонувальник будує ієрархічні дерева об'єктів.")); 
book.Add(ch1);

WordLimitDecorator decoratedBook = new WordLimitDecorator(book, 10);
CachedWordCountProxy proxy = new CachedWordCountProxy(decoratedBook);

Console.WriteLine("\n--- КЕЙС 1: Перший виклик ---");
Console.WriteLine($"Кількість слів у книзі: {proxy.GetWordCount()}");
Console.WriteLine($"Запит обслуговано з кешу проксі: {(proxy.IsCacheHit ? "ТАК" : "НІ")}");

Console.WriteLine("\n--- КЕЙС 2: Другий виклик ---");
Console.WriteLine($"Кількість слів у книзі: {proxy.GetWordCount()}");
Console.WriteLine($"Запит обслуговано з кешу проксі: {(proxy.IsCacheHit ? "ТАК" : "НІ")}");

Console.WriteLine("\n--- КЕЙС 3: Відображення повної структури ---");
proxy.Display();

IntegrationTests.RunAllTests();

public interface IBookComponent
{
    int GetWordCount();
    void Display(int indent = 0);
}

public class Page : IBookComponent
{
    public int PageNumber { get; private set; }
    public string Content { get; set; }

    public Page(int pageNumber, string content)
    {
        PageNumber = pageNumber;
        Content = content ?? string.Empty;
    }

    public int GetWordCount()
    {
        if (string.IsNullOrWhiteSpace(Content)) return 0;
        return Content.Split(new[] { ' ', '\r', '\n', '.', ',' }, StringSplitOptions.RemoveEmptyEntries).Length;
    }

    public void Display(int indent = 0)
    {
        Console.WriteLine($"{new string(' ', indent)}📄 Сторінка {PageNumber} (Слів: {GetWordCount()})");
    }
}

public class Chapter : IBookComponent
{
    public string Title { get; private set; }
    private readonly List<IBookComponent> _children = new List<IBookComponent>();

    public Chapter(string title) { Title = title; }

    public void Add(IBookComponent component) => _children.Add(component);
    public void Remove(IBookComponent component) => _children.Remove(component);

    public int GetWordCount()
    {
        int total = 0;
        foreach (var child in _children) total += child.GetWordCount();
        return total;
    }

    public void Display(int indent = 0)
    {
        Console.WriteLine($"{new string(' ', indent)}📁 Розділ: \"{Title}\"");
        foreach (var child in _children) child.Display(indent + 4);
    }
}

public class WordLimitDecorator : IBookComponent
{
    private readonly IBookComponent _component;
    public int MaxWords { get; private set; }

    public WordLimitDecorator(IBookComponent component, int maxWords)
    {
        _component = component ?? throw new ArgumentNullException(nameof(component));
        if (maxWords < 0) throw new ArgumentException("Ліміт не може бути меншим за нуль.");
        MaxWords = maxWords;
    }

    public int GetWordCount() => _component.GetWordCount();
    public bool IsLimitExceeded() => GetWordCount() > MaxWords;

    public void Display(int indent = 0)
    {
        if (IsLimitExceeded())
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{new string(' ', indent)}⚠️ [ПОПЕРЕДЖЕННЯ ДЕКОРАТОРА: Ліміт у {MaxWords} слів ПЕРЕВИЩЕНО!]");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine($"{new string(' ', indent)}✅ [ДЕКОРАТОР: Ліміт слів в нормі (менше {MaxWords})]");
        }
        _component.Display(indent);
    }
}

public class CachedWordCountProxy : IBookComponent
{
    private readonly IBookComponent _realComponent;
    private int? _cachedWordCount = null;
    public bool IsCacheHit { get; private set; } = false;

    public CachedWordCountProxy(IBookComponent realComponent) { _realComponent = realComponent; }

    public int GetWordCount()
    {
        if (_cachedWordCount == null)
        {
            IsCacheHit = false;
            _cachedWordCount = _realComponent.GetWordCount();
        }
        else
        {
            IsCacheHit = true;
        }
        return _cachedWordCount.Value;
    }

    public void Display(int indent = 0)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"{new string(' ', indent)}⚡ [Proxy: Контроль доступу та аналіз кешу активовано]");
        Console.ResetColor();
        _realComponent.Display(indent);
    }
}

public static class IntegrationTests
{
    public static void RunAllTests()
    {
        Console.WriteLine("\n==================================================");
        Console.WriteLine("ВИКОНАННЯ НАБОРУ ІНТЕГРАЦІЙНИХ ТЕСТІВ");
        Console.WriteLine("==================================================");

        try
        {
            Test_Composite_WordCountCalculation();
            Test_Decorator_LimitNotExceeded();
            Test_Decorator_LimitExceeded_BorderCase();
            Test_Proxy_CachingBehavior();
            Test_Decorator_InvalidMaxWords_ThrowsException();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n🎉 УСІ 5 ІНТЕГРАЦІЙНИХ ТЕСТІВ УСПІШНО ПРОЙДЕНО!");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ ТЕСТ ПРОВАЛЕНО: {ex.Message}");
            Console.ResetColor();
        }
    }

    static void Test_Composite_WordCountCalculation()
    {
        Chapter root = new Chapter("Тестовий Корінь");
        root.Add(new Page(1, "One Two Three"));
        root.Add(new Page(2, "Four Five"));
        if (root.GetWordCount() != 5) throw new Exception("Тест 1 провалено");
        Console.WriteLine("✅ Тест 1: Рекурсивний підрахунок Composite — Ок.");
    }

    static void Test_Decorator_LimitNotExceeded()
    {
        Page page = new Page(1, "Я люблю архітектуру");
        WordLimitDecorator decorator = new WordLimitDecorator(page, 10);
        if (decorator.IsLimitExceeded()) throw new Exception("Тест 2 провалено");
        Console.WriteLine("✅ Тест 2: Декоратор (Нормальний об'єм) — Ок.");
    }

    static void Test_Decorator_LimitExceeded_BorderCase()
    {
        Page page = new Page(1, "One Two Three Four");
        WordLimitDecorator decorator = new WordLimitDecorator(page, 3);
        if (!decorator.IsLimitExceeded()) throw new Exception("Тест 3 провалено");
        Console.WriteLine("✅ Тест 3: Граничний кейс перевищення ліміту — Ок.");
    }

    static void Test_Proxy_CachingBehavior()
    {
        Page page = new Page(1, "Hello Csharp Pattern World");
        CachedWordCountProxy proxy = new CachedWordCountProxy(page);
        proxy.GetWordCount();
        bool firstRequestHit = proxy.IsCacheHit;
        proxy.GetWordCount();
        bool secondRequestHit = proxy.IsCacheHit;
        if (firstRequestHit || !secondRequestHit) throw new Exception("Тест 4 провалено");
        Console.WriteLine("✅ Тест 4: Валідація кешування Proxy (Швидкість) — Ок.");
    }

    static void Test_Decorator_InvalidMaxWords_ThrowsException()
    {
        Page page = new Page(1, "Текст");
        try
        {
            WordLimitDecorator decorator = new WordLimitDecorator(page, -5);
            throw new Exception("Тест 5 провалено");
        }
        catch (ArgumentException)
        {
            Console.WriteLine("✅ Тест 5: Обробка виняткових ситуацій декоратора (Exception) — Ок.");
        }
    }
}