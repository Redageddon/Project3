using System.Security.Cryptography;
using System.Text;

namespace API.Services;

public class PasswordHasher
{
    public string HashPassword(string password)
    {
        byte[] hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

        return Convert.ToBase64String(hashedBytes);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        string hashOfInput = this.HashPassword(password);

        return hashOfInput == hashedPassword;
    }
}
