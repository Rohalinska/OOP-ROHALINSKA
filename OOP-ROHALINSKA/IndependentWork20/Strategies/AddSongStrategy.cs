using System;
using IndependentWork20.Interfaces;

namespace IndependentWork20.Strategies
{
    public class AddSongStrategy : IDataProcessorStrategy
    {
        public void Process(string data)
        {
            Console.WriteLine($"[STRATEGY] Додавання нової пісні до системи: '{data}'");
        }
    }
}