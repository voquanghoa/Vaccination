using System.ComponentModel.DataAnnotations;

namespace AspDataModel.Models
{
    public abstract class IdBase
    {
        [Key]
        public int Id { get; set; }
    }
}