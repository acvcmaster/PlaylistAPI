using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaylistAPI.Models
{
    [Table("PROPERTIES")]
    public class Property : BaseModel
    {
        [Column("NAME"), Required, StringLength(255)]
        public string Name { get; set; }

        [Column("TYPE"), StringLength(25)]
        public string Type { get; set; }

        [Column("DESCRIPTION")]
        public string Description { get; set; }
    }
}