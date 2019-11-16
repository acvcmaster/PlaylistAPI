using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
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
            var song = base.Insert(model);
            if (urlSuccess)
            {
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
                catch
                {
                    if (song != null)
                        base.Delete(song.Id);
                    return null;
                }
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

        public CompleteSong GetFile(int id)
        {
            try
            {
                var songPropertySet = Context.ArquireDbSet<SongProperty>();
                var propertySet = Context.ArquireDbSet<Property>();
                Song model = base.Get(id);

                string contentType = new FileExtensionContentTypeProvider().TryGetContentType(model.Url, out contentType) ?
                    contentType : "application/octet-stream";

                CompleteSong result = new CompleteSong() { Song = model, Type = contentType, File = new StreamReader(model.Url).BaseStream };
                return result;
            }
            catch { return null; }
        }

        public IEnumerable<Song> MassInsert(string directoryUrl)
        {
            if (Directory.Exists(directoryUrl))
            {
                List<Song> insertedSongs = new List<Song>();
                var files = Directory.GetFiles(directoryUrl);
                foreach (var file in files)
                {
                    Song song = new Song() { Url = file };
                    base.Insert(song);

                    if (song == null)
                        continue;

                    try
                    {
                        var taglibFile = TagLib.File.Create(song.Url);
                        var propertySet = Context.ArquireDbSet<Property>();
                        var songPropertySet = Context.ArquireDbSet<SongProperty>();
                        var properties = (from item in propertySet select item).ToList();
                        var songProperties = new List<SongProperty>();

                        foreach (var property in properties)
                            songProperties.Add(GetSongProperty(song, property, taglibFile));

                        songPropertySet.AddRange(songProperties);
                        insertedSongs.Add(song);
                    }
                    catch
                    {
                        if (song != null)
                            base.Delete(song.Id);
                    }
                }
                Context.SaveChanges();
                return insertedSongs;
            }
            return null;
        }
    }
}