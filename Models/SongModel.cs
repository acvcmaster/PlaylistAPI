using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaylistAPI.Models
{
    [Table("SONGS")]
    public class Song : BaseModel
    {
        [Column("URL"), Required]
        public string Url { get; set; }
    }
}