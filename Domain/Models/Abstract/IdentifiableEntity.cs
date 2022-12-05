using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Abstract
{
    public abstract class IdentifiableEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
