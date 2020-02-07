using System.Collections.Generic;

namespace PlaylistAPI.Models
{
    public class AmplitudeJSPlaylist
    {
        public IEnumerable<AmplitudeJSSong> Songs { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }
}