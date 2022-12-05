using Domain.Models.Abstract;

namespace Domain.Models
{
    public class User : AuthenticableClient
    {
        public IEnumerable<Vote>? Votes { get; set; }
    }
}
