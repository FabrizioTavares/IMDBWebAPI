namespace Service.Abstraction.AdministrativeServiceAbstractions.AdminDTOs;

public class UpdateAdminDTO
{
    public string? Username { get; set; }
    public string? NewPassword { get; set; }
    public string? CurrentPassword { get; set; }
}