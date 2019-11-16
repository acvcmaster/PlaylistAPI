namespace PlaylistAPI.Models
{
    public class PlaylistFile
    {
        public byte[] Data { get; set; }
        public Playlist Playlist { get; set; }
    }
}