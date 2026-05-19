using System;
using IndependentWork20.Interfaces;

namespace IndependentWork20.Strategies
{
    public class UpdateSongMetadataStrategy : IDataProcessorStrategy
    {
        public void Process(string data)
        {
            Console.WriteLine($"[STRATEGY] Оновлення метаданих треку (теги, авторські права): '{data}'");
        }
    }
}