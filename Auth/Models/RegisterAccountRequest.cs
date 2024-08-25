namespace Mouse.NET.Auth.Models
{
    public class RegisterAccountRequest : LoginAccountRequest
    {
        public string InviteToken { get; set; }
    }
}