using System;
using System.Text;

namespace MyPass.Core
{
    public static class PasswordGenerator
    {
        public static string Generate(
            int length = 12,
            bool includeSpecialChars = true,
            bool onlyUpperCase = false)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            const string special = "!@#$%ˆ&*(){}[];";
            var chars = includeSpecialChars ? (valid + special) : valid;

            var res = new StringBuilder();
            var rnd = new Random();

            while (0 < length--)
                res.Append(chars[rnd.Next(chars.Length)]);


            return onlyUpperCase ? res.ToString().ToUpper() : res.ToString();
        }
    }
}
