using System;

namespace IndependentWork23
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("==================================================");
            Console.WriteLine("1. ТЕСТУВАННЯ ПАТЕРНУ ADAPTER");
            Console.WriteLine("==================================================");
            
            OldUserProfileService oldService = new OldUserProfileService();
            IUserProfileLoader adapter = new UserProfileAdapter(oldService);
     
            string adaptedData = adapter.GetUserProfileData(42);
            Console.WriteLine($"Клієнт отримав результат: {adaptedData}\n");

            Console.WriteLine("==================================================");
            Console.WriteLine("2. ТЕСТУВАННЯ ПАТЕРНУ FACADE");
            Console.WriteLine("==================================================");
            
            UserFacade userFacade = new UserFacade();
            string secureResult = userFacade.GetSecureUserProfile(101, "valid_token_secret");
            Console.WriteLine($"Клієнт отримав від фасаду захищений профіль:\n{secureResult}\n");

            Console.WriteLine("==================================================");
            Console.WriteLine("3. ТЕСТУВАННЯ ПАТЕРНУ PROXY (LAZY LOADING)");
            Console.WriteLine("==================================================");

            IUserSession sessionProxy = new LazyUserSessionProxy("admin_user");
            Console.WriteLine("[Main] Проксі створено. Робимо паузу в коді (об'єкт у пам'яті ще легкий)...");
            
            Console.WriteLine("[Main] Вперше викликаємо метод, що потребує реальних даних:");
            sessionProxy.DisplaySessionDetails(); 
            Console.WriteLine("[Main] Викликаємо метод вдруге (об'єкт вже створено, ініціалізації не буде):");
            sessionProxy.DisplaySessionDetails();

            Console.WriteLine("==================================================");
            Console.WriteLine("Програму завершено.");
            Console.ReadLine();
        }
    }
}