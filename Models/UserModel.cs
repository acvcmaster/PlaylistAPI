using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaylistAPI.Models
{
    [Table("USERS")]
    public class User : BaseModel
    {
        [Column("NAME"), Required, StringLength(255)]
        public string Name { get; set; }

        [Column("EMAIL"), StringLength(255)]
        public string Email { get; set; }

        [Column("PASSWORD"), Required, StringLength(255)]
        public string Password { get; set; }
    }
}