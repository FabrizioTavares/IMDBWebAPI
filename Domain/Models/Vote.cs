using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Vote
    {
        public virtual Movie Movie { get; set; } = default!;
        [Key, Column(Order = 0)]
        public int MovieId { get; set; }
        public virtual User User { get; set; } = default!;
        [Key, Column(Order = 1)]
        public int UserId { get; set; }
        public int Rating { get; set; }
    }
}
