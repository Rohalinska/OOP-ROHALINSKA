public class SystemMonitor
{
    private readonly ILogger _logger;
    private readonly INotifier _notifier;
    private readonly IReportGenerator _reportGenerator;

    public SystemMonitor(ILogger logger, INotifier notifier, IReportGenerator reportGenerator)
    {
        _logger = logger;
        _notifier = notifier;
        _reportGenerator = reportGenerator;
    }

    public void LogEvent(string message)
    {
        _logger.Log(message);
    }

    public void SendAlert(string message)
    {
        _notifier.Send(message);
    }

    public void GenerateSystemReport()
    {
        _reportGenerator.Generate();
    }
}
