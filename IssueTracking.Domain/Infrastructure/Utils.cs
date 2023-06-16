using System.Security.Cryptography;
using System.Text;

namespace IssueTracking.Domain.Infrastructure
{
    public static class Utils
    {
        public static string Hash(this string text)
        {
            using (var hashProvider = SHA1.Create())
            {
                StringBuilder strBuilder = new StringBuilder();

                foreach (byte b in hashProvider.ComputeHash(Encoding.UTF8.GetBytes(text)))
                {
                    strBuilder.Append(b.ToString("x2"));
                }

                return strBuilder.ToString();
            }
        }
    }
}