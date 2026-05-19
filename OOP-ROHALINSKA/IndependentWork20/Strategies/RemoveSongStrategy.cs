using System;
using IndependentWork20.Interfaces;

namespace IndependentWork20.Strategies
{
    public class RemoveSongStrategy : IDataProcessorStrategy
    {
        public void Process(string data)
        {
            Console.WriteLine($"[STRATEGY] Видалення пісні з системи: '{data}'");
        }
    }
}