using System;

namespace IndependentWork20.Observers
{
    public class MusicLibraryObserver
    {
        public void OnDataProcessed(string data)
        {
            Console.WriteLine($"[OBSERVER - MusicLibrary] Отримано! Синхронізуємо глобальний каталог медіатеки для: '{data}'");
        }
    }
}