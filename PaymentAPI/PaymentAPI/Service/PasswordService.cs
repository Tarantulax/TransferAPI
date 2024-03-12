using System.Security.Cryptography;
using System.Text;

namespace PaymentAPI.Service
{
    public interface IPasswordService
    {
        bool VerifyPasswordHash(string password, string storedHash);
        string CreateMD5Hash(string password);
    }
    public class PasswordService : IPasswordService
    {
        public bool VerifyPasswordHash(string password, string storedHash)
        {
            string hashPassword = CreateMD5Hash(password);
            return hashPassword.ToLower() == storedHash.ToLower();
        }

        public string CreateMD5Hash(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
