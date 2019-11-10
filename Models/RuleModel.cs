using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaylistAPI.Models
{
    [Table("RULES")]
    public class Rule : BaseModel
    {
        [Column("PROPERTY_ID"), Required]
        public int PropertyId { get; set; }

        [Column("COMPARATOR_ID"), Required]
        public int ComparatorId { get; set; }
    }
}