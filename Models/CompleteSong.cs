using System.Collections.Generic;
using System.IO;

namespace PlaylistAPI.Models
{
    public class CompleteSong
    {
        public Stream File { get; set; }
        public string Type { get; set; }
        public Song Song { get; set; }
        public string RemoteUrl { get; set; }
        public IEnumerable<CompleteSongProperty> Properties;
    }
}