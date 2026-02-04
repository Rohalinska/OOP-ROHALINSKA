using System;

public class FileReportGenerator : IReportGenerator
{
    public void Generate()
    {
        Console.WriteLine("Report saved to file.");
    }
}
