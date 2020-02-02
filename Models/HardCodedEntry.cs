using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaylistAPI.Models
{
    [Table("HARDCODED_ENTRIES")]
    public class HardCodedEntry : BaseModel
    {
        [Column("PLAYLIST_ID"), Required]
        public int PlaylistId { get; set; }

        [Column("SONG_ID"), Required]
        public int SongId { get; set; }
    }
}