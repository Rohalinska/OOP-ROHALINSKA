namespace IndependentWork24
{
    public static class IntegrationTests
    {
        public static void RunAllTests()
        {
            Console.WriteLine("ВИКОНАННЯ НАБОРУ ІНТЕГРАЦІЙНИХ ТЕСТІВ");
            try
            {
                Test_Composite_WordCountCalculation();
                Test_Decorator_LimitNotExceeded();
                Test_Decorator_LimitExceeded_NegativeOrBorderCase();
                Test_Proxy_CachingBehavior();
                Test_Decorator_InvalidMaxWords_ThrowsException();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" УСІ ТЕСТИ УСПІШНО ПРОЙДЕНО!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" ТЕСТ ПРОВАЛЕНО: {ex.Message}");
                Console.ResetColor();
            }
        }

        static void Test_Composite_WordCountCalculation()
        {
            Chapter root = new Chapter("Root");
            root.Add(new Page(1, "One Two Three"));
            root.Add(new Page(2, "Four Five"));

            int count = root.GetWordCount();
            if (count != 5) throw new Exception($"Test 1 Failed: Очікувалось 5 слів, отримано {count}");
            Console.WriteLine(" Тест 1: Підрахунок слів Composite — Пройдено.");
        }

        static void Test_Decorator_LimitNotExceeded()
        {
            Page page = new Page(1, "Тестовий текст"); 
            WordLimitDecorator decorator = new WordLimitDecorator(page, 5);

            if (decorator.IsLimitExceeded()) throw new Exception("Test 2 Failed: Ліміт не мав бути перевищений.");
            Console.WriteLine(" Тест 2: Декоратор (Нормальний об'єм) — Пройдено.");
        }

        static void Test_Decorator_LimitExceeded_NegativeOrBorderCase()
        {
            Page page = new Page(1, "One Two Three Four"); 
            WordLimitDecorator decorator = new WordLimitDecorator(page, 3); 

            if (!decorator.IsLimitExceeded()) throw new Exception("Test 3 Failed: Декоратор не зафіксував перевищення граничного ліміту.");
            Console.WriteLine(" Тест 3: Граничний кейс перевищення ліміту — Пройдено.");
        }

        static void Test_Proxy_CachingBehavior()
        {
            Page page = new Page(1, "Performance Caching Test");
            CachedWordCountProxy proxy = new CachedWordCountProxy(page);

            proxy.GetWordCount();
            bool firstHit = proxy.IsCacheHit; 

            proxy.GetWordCount();
            bool secondHit = proxy.IsCacheHit; 

            if (firstHit || !secondHit) throw new Exception("Test 4 Failed: Поведінка кешування Proxy некоректна.");
            Console.WriteLine(" Тест 4: Проксі-кешування продуктивності — Пройдено.");
        }

        static void Test_Decorator_InvalidMaxWords_ThrowsException()
        {
            Page page = new Page(1, "Valid text");
            try
            {
                WordLimitDecorator decorator = new WordLimitDecorator(page, -10); 
                throw new Exception("Test 5 Failed: Декоратор дозволив встановити від'ємний ліміт слів.");
            }
            catch (ArgumentException)
            {
                Console.WriteLine(" Тест 5: Валідація некоректних параметрів декоратора (Exception) — Пройдено.");
            }
        }
    }
}