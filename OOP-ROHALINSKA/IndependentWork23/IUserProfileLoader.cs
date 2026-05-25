using System;

namespace IndependentWork23
{
    public interface IUserProfileLoader
    {
        string GetUserProfileData(int userId);
    }

    public class OldUserProfileService
    {
        public string LoadUser(int id)
        {
            return $"ID:{id};Name:John Doe;Role:PremiumUser";
        }
    }

    public class UserProfileAdapter : IUserProfileLoader
    {
        private readonly OldUserProfileService _oldService;

        public UserProfileAdapter(OldUserProfileService oldService)
        {
            _oldService = oldService;
        }

        public string GetUserProfileData(int userId)
        {
            Console.WriteLine("[Adapter] Перенаправлення запиту до OldUserProfileService...");
            string rawData = _oldService.LoadUser(userId);
            return $"[Адаптовані дані] -> {rawData}";
        }
    }
}