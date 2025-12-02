using Polly;
using Polly.CircuitBreaker;
using Polly.Timeout;
using System;
using System.Net.Http;
using System.Threading;

namespace IndependentWork13
{
    class Program
    {
        // Лічильники для імітації помилок
        private static int _apiCallAttempts = 0;
        private static int _dbCallAttempts = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("--- Scenario 1: External API Call with Retry ---");
            // Проблема: Зовнішній API може тимчасово повертати помилки
            // Політика: Retry з експоненційною затримкою
            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Retry {retryCount} after {timeSpan.TotalSeconds}s due to: {exception.Message}");
                });

            try
            {
                string result = retryPolicy.Execute(() => CallExternalApi("https://api.example.com/data"));
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Final Result: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Operation failed after all retries: {ex.Message}");
            }

            Console.WriteLine("\n--- Scenario 2: Database Access with Circuit Breaker ---");
            // Проблема: База даних може тимчасово бути недоступною
            // Політика: Circuit Breaker (3 невдачі відкривають "переривник" на 5 секунд)
            var circuitBreakerPolicy = Policy
                .Handle<Exception>()
                .CircuitBreaker(3, TimeSpan.FromSeconds(5),
                (exception, duration) =>
                {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Circuit opened for {duration.TotalSeconds}s due to: {exception.Message}");
                },
                () =>
                {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Circuit closed. Resuming operations.");
                });

            for (int i = 0; i < 6; i++)
            {
                try
                {
                    circuitBreakerPolicy.Execute(() => CallDatabase());
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Database call failed: {ex.Message}");
                }
                Thread.Sleep(1000);
            }

            Console.WriteLine("\n--- Scenario 3: Operation with Timeout ---");
            // Проблема: Деякі операції можуть виконуватися занадто довго
            // Політика: Timeout 2 секунди
            var timeoutPolicy = Policy
                .Timeout(2, TimeoutStrategy.Pessimistic,
                onTimeout: (context, timespan, task) =>
                {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Operation timed out after {timespan.TotalSeconds}s");
                });

            try
            {
                timeoutPolicy.Execute(() => LongRunningOperation(5));
            }
            catch (TimeoutRejectedException)
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Timeout exception caught in main");
            }

            Console.WriteLine("\n--- End of Scenarios ---");
        }

        // Сценарій 1: Зовнішній API
        public static string CallExternalApi(string url)
        {
            _apiCallAttempts++;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Attempt {_apiCallAttempts}: Calling API {url}...");
            if (_apiCallAttempts <= 2) // Імітуємо 2 невдачі
            {
                throw new HttpRequestException($"API call failed for {url}");
            }
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] API call to {url} successful!");
            return "Data from API";
        }

        // Сценарій 2: База даних
        public static void CallDatabase()
        {
            _dbCallAttempts++;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Database attempt {_dbCallAttempts}");
            if (_dbCallAttempts % 2 != 0) // Імітуємо помилку для кожного непарного виклику
            {
                throw new Exception("Temporary database connection error");
            }
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Database call successful");
        }

        // Сценарій 3: Довга операція
        public static void LongRunningOperation(int seconds)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Starting long operation ({seconds}s)...");
            Thread.Sleep(seconds * 1000);
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Operation completed");
        }
    }
}
