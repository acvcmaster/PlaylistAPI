using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaylistAPI.Models
{
    [Table("USERS")]
    public class User : BaseModel
    {
        [Column("NAME"), Required]
        public string Name { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("PASSWORD"), Required]
        public string Password { get; set; }
    }
}