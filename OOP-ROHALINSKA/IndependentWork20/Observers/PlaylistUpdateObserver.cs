using System;

namespace IndependentWork20.Observers
{
    public class PlaylistUpdateObserver
    {
        public void OnDataProcessed(string data)
        {
            Console.WriteLine($"[OBSERVER - PlaylistUpdate] Отримано! Перевіряємо та оновлюємо плейлисти користувачів для: '{data}'");
        }
    }
}