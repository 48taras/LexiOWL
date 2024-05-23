using System.Text.RegularExpressions;

namespace LexiOWL.Managers
{
    public static class ValidationUtils
    {
        public static bool IsValidUsername(string username)
        {
            return Regex.IsMatch(username, @"^[\p{L}]+$");
        }

        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
        }

        public static bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, @"^[a-zA-Z0-9]{5,50}$");
        }

    }
}
