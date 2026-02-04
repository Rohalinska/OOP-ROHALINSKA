class Program
{
    static void Main()
    {
        ILogger logger = new ConsoleLogger();
        INotifier notifier = new EmailNotifier();
        IReportGenerator reportGenerator = new FileReportGenerator();

        SystemMonitor monitor = new SystemMonitor(logger, notifier, reportGenerator);

        monitor.LogEvent("System started");
        monitor.SendAlert("CPU overload detected");
        monitor.GenerateSystemReport();
    }
}
