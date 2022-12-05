namespace Domain.DTOs.AdminDTOs
{
    public class UpdateAdminDTO
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int? Hierarchy { get; private set; }
    }
}
