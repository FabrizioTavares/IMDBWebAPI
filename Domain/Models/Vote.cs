using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Vote
    {
        [Key, Column(Order = 0)]
        public virtual User Voter { get; set; } = new();
        public int VoterId { get; set; }
        [Key, Column(Order = 1)]
        public virtual Movie Movie { get; set; } = new();
        public int MovieId { get; set; }
        public int Rating { get; set; }
    }
}
