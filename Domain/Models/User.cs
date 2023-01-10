using Domain.Models.Abstract;

namespace Domain.Models;

public class User : AuthenticableClient
{
    public List<Vote> Votes { get; set; } = new List<Vote>();
}