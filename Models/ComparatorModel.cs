using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaylistAPI.Models
{
    [Table("COMPARATORS")]
    public class Comparator : BaseModel
    {
        [Column("DESCRIPTION"), StringLength(255)]
        public string Description { get; set; }

        [Column("OPERATOR"), Required, StringLength(2)]
        public string Operator { get; set; }
    }
}