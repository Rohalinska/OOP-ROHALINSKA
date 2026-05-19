using System;

namespace IndependentWork20.Publishers
{
    public class DataPublisher
    {
        // Подія C# на базі делегату Action (це наш Observer)
        public event Action<string>? DataProcessed;

        public void PublishDataProcessed(string data)
        {
            Console.WriteLine($"[PUBLISHER] Сповіщення: Обробку треку '{data}' завершено! Надсилаємо оновлення підписникам...");
            DataProcessed?.Invoke(data);
        }
    }
}