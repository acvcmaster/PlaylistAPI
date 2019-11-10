using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaylistAPI.Models
{
    [Table("PLAYLIST_RULES")]
    public class PlaylistRule : BaseModel
    {
        [Column("PLAYLIST_ID"), Required]
        public int PlaylistId { get; set; }

        [Column("RULE_ID"), Required]
        public int RuleId { get; set; }

        [Column("DATA")]
        public string Data { get; set; }
    }
}