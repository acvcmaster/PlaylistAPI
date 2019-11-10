using System;
using System.ComponentModel.DataAnnotations;

namespace PlaylistAPI.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public bool Active { get; set; }
    }
}