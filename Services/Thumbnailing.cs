using System;
using System.Diagnostics;
using System.IO;
using PlaylistAPI.Models;

namespace PlaylistAPI.Services
{
    public interface IThumbnailingService
    {
        Stream GetThumbnail(Song song);
    }
    public class FfmpegThumbnailingService : IThumbnailingService
    {
        public const string THUMBNAIL_CACHE = "thumbnail_cache";

        public FfmpegThumbnailingService()
        {
            if (!Directory.Exists(THUMBNAIL_CACHE)) {
                var a = Directory.CreateDirectory(THUMBNAIL_CACHE);
            }
        }

        public Stream GetThumbnail(Song song)
        {
            try
            {
                var thumbPath = GetThumbnailPath(song);
                if (!File.Exists(thumbPath))
                {
                    var output = GetFfmpegOutput($"-i \"{song.Url}\" -hide_banner -loglevel panic -v quiet -f image2  -");
                    using (var fileStream = File.Create(thumbPath))
                    {
                        output.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                }

                return File.Open(thumbPath, FileMode.Open);
            }
            catch { return null; }
        }

        private Stream GetFfmpegOutput(string arguments)
        {
            var ffmpeg = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "ffmpeg",
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            ffmpeg.Start();
            return ffmpeg.StandardOutput.BaseStream;
        }

        private string GetThumbnailPath(Song song)
        {
            return $"{THUMBNAIL_CACHE}/{song.Id}.jpg";
        }
    }
}