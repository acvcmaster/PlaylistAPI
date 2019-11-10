using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaylistAPI.Models
{
    [Table("SONG_PROPERTIES")]
    public class SongProperty : BaseModel
    {
        [Column("NAME"), Required, StringLength(255)]
        public string Name { get; set; }

        [Column("SONG_ID"), Required]
        public int SongId { get; set; }

        [Column("VALUE")]
        public string Value { get; set; }
    }
}