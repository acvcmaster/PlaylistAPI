using System;
using System.Collections.Generic;
using System.Linq;
using PlaylistAPI.Models;

namespace PlaylistAPI.Business
{
    public class SongBusiness : BaseBusiness<Song>
    {
        public SongBusiness() : base(null)
        {
        }

        public override Song Insert(Song model)
        {
            Uri url;
            bool urlSuccess = Uri.TryCreate(model.Url, UriKind.Absolute, out url) && url.Scheme == Uri.UriSchemeFile;
            if (urlSuccess)
            {
                var song = base.Insert(model);
                if (song == null)
                    return null;

                try
                {
                    var file = TagLib.File.Create(song.Url);
                    var propertySet = Context.ArquireDbSet<Property>();
                    var songPropertySet = Context.ArquireDbSet<SongProperty>();
                    var properties = (from item in propertySet select item).ToList();
                    var songProperties = new List<SongProperty>();

                    foreach (var property in properties)
                        songProperties.Add(GetSongProperty(song, property, file));

                    songPropertySet.AddRange(songProperties);
                    Context.SaveChanges();
                    return song;
                }
                catch { return null; }
            }
            return null;
        }

        private SongProperty GetSongProperty(Song song, Property property, TagLib.File file)
        {
            SongProperty result = new SongProperty { SongId = song.Id, PropertyId = property.Id };

            switch (property.Name)
            {
                case "ALBUM":
                    result.Value = file.Tag.Album;
                    break;
                case "ALBUM_ARTIST":
                    result.Value = file.Tag.FirstAlbumArtist;
                    break;
                case "ARTIST":
                    result.Value = file.Tag.FirstPerformer;
                    break;
                case "BIT_RATE":
                    result.Value = file.Properties.AudioBitrate.ToString();
                    break;
                case "COMPOSER":
                    result.Value = file.Tag.FirstComposer;
                    break;
                case "DATE_ADDED":
                    result.Value = DateTime.Now.ToString();
                    break;
                case "DESCRIPTION":
                    result.Value = file.Tag.Description;
                    break;
                case "DISC_NUMBER":
                    result.Value = file.Tag.Disc.ToString();
                    break;
                case "GENRE":
                    result.Value = file.Tag.FirstGenre;
                    break;
                case "NAME":
                    result.Value = file.Tag.Title;
                    break;
                case "SAMPLE_RATE":
                    result.Value = file.Properties.AudioSampleRate.ToString();
                    break;
                case "TRACK_NUMER":
                    result.Value = file.Tag.Track.ToString();
                    break;
                case "YEAR":
                    result.Value = file.Tag.Year.ToString();
                    break;
            }
            return result;
        }
    }
}