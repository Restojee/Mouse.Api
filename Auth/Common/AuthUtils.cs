using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Mouse.NET.Auth.Models;
using Mouse.NET.Users.Common;

namespace Mouse.Stick.Controllers.Auth;

public class AuthUtils
{
    static public Dictionary<string, object> GetUserClaims(int userId, string username, string email)
    {
        return new Dictionary<string, object>
        {
            { UserDetails.Id, userId },
            { UserDetails.Email, email },
            { UserDetails.Username, username },
        };
    }

    static public HashSalt GetHashPassword(string password)
    {
        byte[] salt = new byte[128 / 8];

        var rngCsp = new RNGCryptoServiceProvider();

        rngCsp.GetNonZeroBytes(salt);

        var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8)
        );

        var hashSalt = new HashSalt
        {
            Hash = hash,
            Salt = salt
        };

        return hashSalt;
    }
    
    static public bool VerifyHashPassword(string hashPassword, string requestPassword, byte[] salt)
    {
        var requestHashPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: requestPassword,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8)
        );

        return requestHashPassword == hashPassword;
    }
}