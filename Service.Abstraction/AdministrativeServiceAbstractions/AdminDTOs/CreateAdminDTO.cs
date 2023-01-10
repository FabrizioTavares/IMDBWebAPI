namespace Service.Abstraction.AdministrativeServiceAbstractions.AdminDTOs;

public class CreateAdminDTO
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int Hierarchy { get; set; }
}