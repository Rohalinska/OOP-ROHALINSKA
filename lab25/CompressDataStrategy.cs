public class CompressDataStrategy : IDataProcessorStrategy
{
    public string Process(string data)
    {
        return $"[Compressed] {data}";
    }
}