namespace IndependentWork23
{
    public interface IUserSession
    {
        void DisplaySessionDetails();
        string GetSessionData();
    }

    public class RealUserSession : IUserSession
    {
        private readonly string _username;
        private string _loadedData;

        public RealUserSession(string username)
        {
            _username = username;
            CompileHeavySessionData();
        }

        private void CompileHeavySessionData()
        {
            Console.WriteLine($"[RealSubject] Ініціалізація важкої сесії для {_username}...");
            Console.WriteLine("[RealSubject] Завантаження токенів, прав доступу та логів з БД (імітація затримки)...");
            _loadedData = $"СесіяКористувача({_username}): Active, SecureToken=XYZ123789";
        }

        public void DisplaySessionDetails()
        {
            Console.WriteLine($"[RealSubject] Відображення деталей сесії: {_loadedData}");
        }

        public string GetSessionData()
        {
            return _loadedData;
        }
    }

    public class LazyUserSessionProxy : IUserSession
    {
        private readonly string _username;
        private RealUserSession _realSession; 
        public LazyUserSessionProxy(string username)
        {
            _username = username;
            Console.WriteLine($"[Proxy] Створено легковаговий проксі-об'єкт сесії для {_username}. Об'єкт у БД ще не запитувався.");
        }

        public void DisplaySessionDetails()
        {
            if (_realSession == null)
            {
                Console.WriteLine("[Proxy] Звернення до методу! Оскільки об'єкт відсутній, створюємо RealSubject...");
                _realSession = new RealUserSession(_username);
            }
            _realSession.DisplaySessionDetails();
        }

        public string GetSessionData()
        {
            if (_realSession == null)
            {
                _realSession = new RealUserSession(_username);
            }
            return _realSession.GetSessionData();
        }
    }
}