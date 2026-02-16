using System;

public class ConsoleLoggerObserver
{
    public void Subscribe(ResultPublisher publisher)
    {
        publisher.ResultCalculated += OnResultCalculated;
    }

    private void OnResultCalculated(double result, string operationName)
    {
        Console.WriteLine($"Operation: {operationName}, Result: {result}");
    }
}
