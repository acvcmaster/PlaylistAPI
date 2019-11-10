using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaylistAPI.Models
{
    [Table("PLAYLISTS")]
    public class Playlist : BaseModel
    {
        [Column("NAME"), Required, StringLength(255)]
        public string Name { get; set; }

        [Column("OWNER_ID"), Required]
        public int OwnerID { get; set; }

        [Column("IS_SMART"), Required]
        public bool IsSmart { get; set; }

        [Column("DISJUNCTIVE_RULES"), Required]
        public bool DisjunctiveRules { get; set; }
    }
}