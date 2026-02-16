using System.Collections.Generic;

public class HistoryLoggerObserver
{
    private List<string> _history = new List<string>();

    public void Subscribe(ResultPublisher publisher)
    {
        publisher.ResultCalculated += OnResultCalculated;
    }

    private void OnResultCalculated(double result, string operationName)
    {
        _history.Add($"Operation: {operationName}, Result: {result}");
    }

    public void PrintHistory()
    {
        foreach (var record in _history)
        {
            System.Console.WriteLine(record);
        }
    }
}
