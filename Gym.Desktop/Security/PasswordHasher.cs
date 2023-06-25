using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Desktop.Security;

public class PasswordHasher
{
    public static (string PasswordHash, string Salt) Hashing(string Password)
    {
        string salt = GenerateSalt();
        string hash = BCrypt.Net.BCrypt.HashPassword(Password + salt);
        return (PasswordHash: hash, Salt: salt);
    }

    public static bool Verify(string password, string passwordHash, string salt)
    {
        return BCrypt.Net.BCrypt.Verify(password + salt, passwordHash);
    }

    public static string GenerateSalt() => Guid.NewGuid().ToString();
}
