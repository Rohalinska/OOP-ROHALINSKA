public class LoggerManager
{
    private static LoggerManager _instance;
    private LoggerFactory _factory;

    private LoggerManager() { }

    public static LoggerManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new LoggerManager();
            return _instance;
        }
    }

    public void SetFactory(LoggerFactory factory)
    {
        _factory = factory;
    }

    public ILogger GetLogger()
    {
        return _factory.CreateLogger();
    }
}