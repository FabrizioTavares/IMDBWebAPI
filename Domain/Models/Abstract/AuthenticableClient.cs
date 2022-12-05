namespace Domain.Models.Abstract
{
    public class AuthenticableClient : IdentifiableEntity
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
