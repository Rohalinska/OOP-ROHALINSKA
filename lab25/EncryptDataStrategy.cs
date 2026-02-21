public class EncryptDataStrategy : IDataProcessorStrategy
{
    public string Process(string data)
    {
        return $"[Encrypted] {data}";
    }
}