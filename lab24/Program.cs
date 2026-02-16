using System;

class Program
{
    static void Main()
    {
        var publisher = new ResultPublisher();

        var consoleObserver = new ConsoleLoggerObserver();
        var historyObserver = new HistoryLoggerObserver();
        var thresholdObserver = new ThresholdNotifierObserver(50);

        consoleObserver.Subscribe(publisher);
        historyObserver.Subscribe(publisher);
        thresholdObserver.Subscribe(publisher);

        var processor = new NumericProcessor(new SquareOperationStrategy());

        double[] numbers = { 4, 5, 9 };

        foreach (var number in numbers)
        {
            double result = processor.Process(number);
            publisher.PublishResult(result, processor.CurrentOperationName);
        }

        processor.SetStrategy(new CubeOperationStrategy());

        foreach (var number in numbers)
        {
            double result = processor.Process(number);
            publisher.PublishResult(result, processor.CurrentOperationName);
        }

        processor.SetStrategy(new SquareRootOperationStrategy());

        foreach (var number in numbers)
        {
            double result = processor.Process(number);
            publisher.PublishResult(result, processor.CurrentOperationName);
        }

        Console.WriteLine("\nHistory:");
        historyObserver.PrintHistory();
    }
}
