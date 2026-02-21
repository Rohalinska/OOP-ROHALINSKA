using System.IO;

public class FileLogger : ILogger
{
    private readonly string _filePath = "log.txt";

    public void Log(string message)
    {
        File.AppendAllText(_filePath, $"[File Logger] {message}\n");
    }
}