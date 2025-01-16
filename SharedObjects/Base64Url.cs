using System;
using System.Text;

namespace SharedObjects
{
    public static class Base64Url
    {
        public static string Encode(string text)
        {
            // Convert the input text to Base64 string
            string base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));

            // Use StringBuilder for efficient string manipulations
            var sb = new StringBuilder(base64);
            sb.Replace('+', '-');
            sb.Replace('/', '_');

            // Remove trailing '=' characters
            while (sb.Length > 0 && sb[sb.Length - 1] == '=')
            {
                sb.Length--;
            }

            return sb.ToString();
        }

        public static string Decode(string text)
        {
            // Use StringBuilder for input text manipulations
            var sb = new StringBuilder(text);
            sb.Replace('-', '+');
            sb.Replace('_', '/');

            // Add padding as needed to make the length a multiple of 4
            int padding = 4 - sb.Length % 4;
            if (padding < 4)
            {
                sb.Append(new string('=', padding));
            }

            // Decode the Base64 string
            return Encoding.UTF8.GetString(Convert.FromBase64String(sb.ToString()));
        }
    }
}