using System.Text;
using System.Security.Cryptography;

namespace PlaylistAPI.Services
{
    public interface ICryptographyService
    {
        string GetSHA256(string password);
    }
    public class CryptographyService : ICryptographyService
    {
        private SHA256 hashProvider;
        public CryptographyService()
        {
            hashProvider = SHA256.Create();
        }
        public string GetSHA256(string password)
        {
            var passwordBytes = Encoding.Unicode.GetBytes(password);
            var hashBytes = hashProvider.ComputeHash(passwordBytes);
            StringBuilder result = new StringBuilder();
            for(int i = 0; i < hashBytes.Length; i++)
            {
                result.Append(hashBytes[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}