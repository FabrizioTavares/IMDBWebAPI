namespace Domain.Models.Abstract
{
    public class AuthenticableClient : IdentifiableEntity
    {
        public string Username { get; set; } = string.Empty;
        public byte[] Password { get; set; } = Array.Empty<byte>();
        public byte[] Salt { get; set; } = Array.Empty<byte>();
        public bool IsActive { get; set; } = true;
    }
}
