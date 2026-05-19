using System;
using IndependentWork20.Contexts;
using IndependentWork20.Publishers;
using IndependentWork20.Observers;
using IndependentWork20.Strategies;

namespace IndependentWork20
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Самостійна робота №20 | Варіант 14 (Музичні дані) ===");

            // 1. Налаштовуємо систему сповіщень (Observer)
            DataPublisher publisher = new DataPublisher();
            MusicLibraryObserver libraryObserver = new MusicLibraryObserver();
            PlaylistUpdateObserver playlistObserver = new PlaylistUpdateObserver();

            // Підписуємо наші компоненти на подію видавця
            publisher.DataProcessed += libraryObserver.OnDataProcessed;
            publisher.DataProcessed += playlistObserver.OnDataProcessed;

            // 2. Створюємо контекст обробки та запускаємо Етап 1 (Додавання)
            string song1 = "Imagine Dragons - Believer";
            Console.WriteLine(" Етап 1: Додавання пісні ");
            DataContext context = new DataContext(new AddSongStrategy());
            context.ExecuteProcessing(song1);
            publisher.PublishDataProcessed(song1);

            // 3. Змінюємо поведінку на льоту — Етап 2 (Оновлення метаданих)
            string song2 = "The Weeknd - Blinding Lights (Remix)";
            Console.WriteLine(" Етап 2: Оновлення інформації про трек ");
            context.SetStrategy(new UpdateSongMetadataStrategy());
            context.ExecuteProcessing(song2);
            publisher.PublishDataProcessed(song2);

            // 4. Змінюємо поведінку знову — Етап 3 (Видалення)
            string song3 = "Unknown Artist - Track 05";
            Console.WriteLine("Етап 3: Видалення треку з бази ");
            context.SetStrategy(new RemoveSongStrategy());
            context.ExecuteProcessing(song3);
            publisher.PublishDataProcessed(song3);

            Console.WriteLine("Роботу завершено успішно.");
            Console.ReadKey();
        }
    }
}