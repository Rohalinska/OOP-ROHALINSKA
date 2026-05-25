namespace IndependentWork23
{
    public class AuthService
    {
        public bool Authenticate(string token)
        {
            Console.WriteLine("[AuthService] Перевірка токена доступу...");
            return !string.IsNullOrEmpty(token);
        }
    }

    public class ProfileService
    {
        public string GetProfile(int userId)
        {
            Console.WriteLine($"[ProfileService] Запит базових даних для користувача #{userId}...");
            return $"User Profile #{userId} (Email: john.doe@example.com)";
        }
    }

    public class DataProtectionService
    {
        public string Encrypt(string data)
        {
            Console.WriteLine("[DataProtectionService] Шифрування конфіденційних даних профілю...");
            return $"AES_ENCRYPTED({data})";
        }
    }

    public class UserFacade
    {
        private readonly AuthService _authService;
        private readonly ProfileService _profileService;
        private readonly DataProtectionService _cryptoService;

        public UserFacade()
        {
            _authService = new AuthService();
            _profileService = new ProfileService();
            _cryptoService = new DataProtectionService();
        }

        public string GetSecureUserProfile(int userId, string authToken)
        {
            Console.WriteLine(" [Facade] Початок виконання комплексного процесу ");
            
            if (!_authService.Authenticate(authToken))
            {
                return "Помилка безпеки: Неавторизований доступ!";
            }

            string profile = _profileService.GetProfile(userId);
            string secureProfile = _cryptoService.Encrypt(profile);

            Console.WriteLine(" [Facade] Процес завершено успішно ");
            return secureProfile;
        }
    }
}