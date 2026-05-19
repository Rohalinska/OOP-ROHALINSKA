using System;
using System.Collections.Generic;
using Xunit;
using IndependentWork21;

namespace IndependentWork21.Tests
{
    public class MusicSystemTests : IDisposable
    {
        private readonly MusicAppEngine _engine;

        public MusicSystemTests()
        {
            // Отримуємо синглтон перед кожним тестом
            _engine = MusicAppEngine.Instance;
            _engine.Reset();
        }

        public void Dispose()
        {
            // Очищаємо стан після кожного тесту
            _engine.Reset();
        }

        // ==========================================
        // ПОЗИТИВНІ СЦЕНАРІЇ (3 тести)
        // ==========================================

        [Fact]
        public void Scenario1_FactoryAndStrategy_ShouldCorrectlyProcessData()
        {
            // Arrange
            var factory = new AddSongFactory();
            _engine.SetStrategyViaFactory(factory);

            // Act
            _engine.Execute("Miley Cyrus - Flowers");

            // Assert
            Assert.Single(_engine.ExecutionHistory);
            Assert.Equal("ADDED: Miley Cyrus - Flowers", _engine.ExecutionHistory[0]);
        }

        [Fact]
        public void Scenario2_SingletonState_ShouldBeStableAndRetainHistory()
        {
            // Arrange
            _engine.SetStrategyViaFactory(new AddSongFactory());
            _engine.Execute("Song A");

            // Act: Отримуємо той самий інстанс через Singleton в іншому місці коду
            var secondaryReference = MusicAppEngine.Instance;
            secondaryReference.Execute("Song B");

            // Assert
            Assert.Same(_engine, secondaryReference); // Перевірка, що це один і той самий об'єкт
            Assert.Equal(2, _engine.ExecutionHistory.Count);
            Assert.Equal("ADDED: Song B", _engine.ExecutionHistory[1]);
        }

        [Fact]
        public void Scenario3_ObserverAndRuntimeStrategySwitch_ShouldNotifySubscribersCorrectly()
        {
            // Arrange
            var receivedNotifications = new List<string>();
            _engine.Publisher.OnSongProcessed += (msg) => receivedNotifications.Add(msg);

            // Act: Крок 1 - Додавання
            _engine.SetStrategyViaFactory(new AddSongFactory());
            _engine.Execute("Track 1");

            // Act: Крок 2 - Зміна стратегії в рантаймі на архівування
            _engine.SetStrategyViaFactory(new ArchiveSongFactory());
            _engine.Execute("Track 2");

            // Assert
            Assert.Equal(2, receivedNotifications.Count);
            Assert.Equal("ADDED: Track 1", receivedNotifications[0]);
            Assert.Equal("ARCHIVED: Track 2", receivedNotifications[1]);
        }

        // ==========================================
        // НЕГАТИВНІ / ГРАНИЧНІ СЦЕНАРІЇ (2 тести)
        // ==========================================

        [Fact]
        public void Scenario4_ExecuteWithoutStrategy_ShouldThrowInvalidOperationException()
        {
            // Arrange (Стратегію не встановлено взагалі)

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _engine.Execute("Valid Song"));
            Assert.Equal("Стратегію обробки не встановлено!", exception.Message);
        }

        [Fact]
        public void Scenario5_ExecuteWithEmptyData_ShouldThrowArgumentException()
        {
            // Arrange
            _engine.SetStrategyViaFactory(new AddSongFactory());

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _engine.Execute(""));
            Assert.Throws<ArgumentException>(() => _engine.Execute("   "));
        }
    }
}