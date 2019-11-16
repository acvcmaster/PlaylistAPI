namespace PlaylistAPI.Models
{
    public class CompleteSongProperty : BaseModel
    {
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public int SongId { get; set; }
    }
}