using Domain.Models.Abstract;

namespace Domain.Models
{
    public class Admin : AuthenticableClient
    {
        public int Hierarchy { get; init; }

    }
}
