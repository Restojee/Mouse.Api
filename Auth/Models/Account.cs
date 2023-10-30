using Mouse.NET.Users.Models;

namespace Mouse.NET.Auth.Models
{
    public class Account
    {
        public string AccessToken { get; set; }
        
        public string RefreshToken { get; set; }

        public User User { get; set; }
    }
}
