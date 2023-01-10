namespace Service.Abstraction.AdministrativeServiceAbstractions.AdminDTOs;

public class ReadAdminDTO
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public int Hierarchy { get; set; }
    public bool IsActive { get; set; }
}