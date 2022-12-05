namespace Domain.DTOs.AdminDTOs
{
    public class ReadAdminDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public int Hierarchy { get; private set; }
    }
}
