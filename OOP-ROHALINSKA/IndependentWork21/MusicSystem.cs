using System;
using System.Collections.Generic;

namespace IndependentWork21
{
    // === STRATEGY ===
    public interface IMusicStrategy
    {
        string Process(string songTitle);
    }

    public class AddSongStrategy : IMusicStrategy
    {
        public string Process(string songTitle) => $"ADDED: {songTitle}";
    }

    public class ArchiveSongStrategy : IMusicStrategy
    {
        public string Process(string songTitle) => $"ARCHIVED: {songTitle}";
    }

    // === FACTORY METHOD ===
    public abstract class MusicProcessorFactory
    {
        public abstract IMusicStrategy CreateStrategy();
    }

    public class AddSongFactory : MusicProcessorFactory
    {
        public override IMusicStrategy CreateStrategy() => new AddSongStrategy();
    }

    public class ArchiveSongFactory : MusicProcessorFactory
    {
        public override IMusicStrategy CreateStrategy() => new ArchiveSongStrategy();
    }

    // === OBSERVER (Суб'єкт сповіщення) ===
    public class MusicPublisher
    {
        public event Action<string>? OnSongProcessed;

        public void Notify(string result)
        {
            OnSongProcessed?.Invoke(result);
        }
    }

    // === SINGLETON + CONTEXT (Серце системи) ===
    public class MusicAppEngine
    {
        private static MusicAppEngine? _instance;
        private static readonly object _lock = new object();

        // ВИПРАВЛЕНО: Прибрано подвійні дужки
        public MusicPublisher Publisher { get; } = new MusicPublisher();
        private IMusicStrategy? _currentStrategy;

        // ВИПРАВЛЕНО: Прибрано подвійні дужки
        public List<string> ExecutionHistory { get; } = new List<string>();

        private MusicAppEngine() { }

        public static MusicAppEngine Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new MusicAppEngine();
                        }
                    }
                }
                return _instance;
            }
        }

        public void SetStrategyViaFactory(MusicProcessorFactory factory)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory), "Фабрика не може бути null!");
            _currentStrategy = factory.CreateStrategy();
        }

        public void Execute(string songTitle)
        {
            if (_currentStrategy == null)
                throw new InvalidOperationException("Стратегію обробки не встановлено!");

            if (string.IsNullOrWhiteSpace(songTitle))
                throw new ArgumentException("Назва пісні не може бути порожньою!", nameof(songTitle));

            string result = _currentStrategy.Process(songTitle);
            ExecutionHistory.Add(result);
            
            // Сповіщаємо підписників (Observer)
            Publisher.Notify(result);
        }

        // Метод для очищення стану між тестами
        public void Reset()
        {
            _currentStrategy = null;
            ExecutionHistory.Clear();
        }
    }
}