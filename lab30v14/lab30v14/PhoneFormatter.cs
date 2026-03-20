using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace lab30vN
{
    public class PhoneFormatter
    {
        // Форматує телефон у вигляд: +380 (XX) XXX-XX-XX
        public string Format(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Phone is empty");

            // залишаємо тільки цифри
            var digits = new string(phone.Where(char.IsDigit).ToArray());

            // підтримка українських номерів
            if (digits.Length == 12 && digits.StartsWith("380"))
            {
                return $"+{digits.Substring(0, 3)} ({digits.Substring(3, 2)}) {digits.Substring(5, 3)}-{digits.Substring(8, 2)}-{digits.Substring(10, 2)}";
            }

            throw new ArgumentException("Invalid phone format");
        }

        // Перевірка валідності телефону
        public bool IsValid(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            var digits = new string(phone.Where(char.IsDigit).ToArray());

            return digits.Length == 12 && digits.StartsWith("380");
        }
    }
}