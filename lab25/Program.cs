using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(" СЦЕНАРІЙ 1: Повна інтеграція ");

        // Logger → Console
        LoggerManager.Instance.SetFactory(new ConsoleLoggerFactory());

        var context = new DataContext(new EncryptDataStrategy());
        var publisher = new DataPublisher();
        var observer = new ProcessingLoggerObserver();
        observer.Subscribe(publisher);

        string result1 = context.Execute("Test Data 1");
        publisher.PublishDataProcessed(result1);

        Console.WriteLine("\n СЦЕНАРІЙ 2: Динамічна зміна логера ");

        string result2 = context.Execute("Test Data 2");
        publisher.PublishDataProcessed(result2);

        // змінюємо фабрику
        LoggerManager.Instance.SetFactory(new FileLoggerFactory());

        string result3 = context.Execute("Test Data 3");
        publisher.PublishDataProcessed(result3);

        Console.WriteLine("Перевірте файл log.txt");

        Console.WriteLine("\n СЦЕНАРІЙ 3: Динамічна зміна стратегії ");

        // повертаємо консоль
        LoggerManager.Instance.SetFactory(new ConsoleLoggerFactory());

        string result4 = context.Execute("Test Data 4");
        publisher.PublishDataProcessed(result4);

        // змінюємо стратегію
        context.SetStrategy(new CompressDataStrategy());

        string result5 = context.Execute("Test Data 5");
        publisher.PublishDataProcessed(result5);

        Console.ReadLine();
    }
}