namespace Mouse.NET.Auth.Models
{
    public class HashSalt
    {
        public string Hash { get; set; }

        public byte[] Salt { get; set; }
    }
}
