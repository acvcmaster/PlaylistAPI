using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaylistAPI.Models
{
    public class BaseModel
    {
        [Key, Column("ID")]
        public int Id { get; set; }
        
        [Column("CREATION")]
        public DateTime Creation { get; set; }

        [Column("LAST_MODIFICATION")]
        public DateTime? LastModification { get; set; }
    }
}