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

        public IEnumerable<CompleteSong> GetAll(int page, int entriesPerPage, HttpRequest request)
        {
            page = page != 0 ? page : 1;
            entriesPerPage = entriesPerPage != 0 ? entriesPerPage : int.MaxValue;
            try
            {
                var songSet = Context.ArquireDbSet<Song>();
                var songPropertySet = Context.ArquireDbSet<SongProperty>();
                var propertySet = Context.ArquireDbSet<Property>();
                var songs = (from song in songSet orderby song.Id select song)
                    .Skip(entriesPerPage * (page - 1))
                    .Take(entriesPerPage)
                    .ToDictionary(item => item.Id);
                var songIds = songs.Select(item => item.Key);

                var properties = (from property in songPropertySet
                                  join p in propertySet on property.PropertyId equals p.Id
                                  where songIds.Contains(property.SongId)
                                  select new CompleteSongProperty
                                  {
                                      Id = property.Id,
                                      Creation = property.Creation,
                                      LastModification = property.LastModification,
                                      PropertyId = p.Id,
                                      Name = p.Name,
                                      Type = p.Type,
                                      Description = p.Description,
                                      SongId = property.SongId,
                                      Value = property.Value
                                  }).ToList();
                

                List<CompleteSong> result = new List<CompleteSong>();
                foreach (var songPair in songs)
                {
                    string contentType = new FileExtensionContentTypeProvider().TryGetContentType(songPair.Value.Url, out contentType) ?
                        contentType : "application/octet-stream";

                    CompleteSong completeSong = new CompleteSong()
                    {
                        Song = songPair.Value,
                        Type = contentType,
                        Properties = properties.Where(item => item.SongId == songPair.Value.Id)
                    };
                    
                    if (request != null)
                        completeSong.RemoteUrl = $"{request.Scheme}://{request.Host}/Song/GetFile?id={songPair.Value.Id}";
                    result.Add(completeSong);
                }
                return result;
            }
            catch { return null; }
        }

        public AmplitudeJSSong GetAmplitudeJSSong(int id, HttpRequest request)
        {
            try
            {
                var song = base.Get(id);
                if (song != null)
                {
                    var songPropertySet = Context.ArquireDbSet<SongProperty>();
                    var propertySet = Context.ArquireDbSet<Property>();

                    var properties = (from property in songPropertySet
                                      join p in propertySet on property.PropertyId equals p.Id
                                      where property.SongId == id
                                      select new CompleteSongProperty
                                      {
                                          Name = p.Name,
                                          Value = property.Value
                                      }).ToDictionary(item => item.Name);

                    return new AmplitudeJSSong()
                    {
                        Name = properties["NAME"].Value,
                        Artist = properties["ARTIST"].Value,
                        Album = properties["ALBUM"].Value,
                        Url = $"{request.Scheme}://{request.Host}/Song/GetFile?id={id}",
                        Cover_art_url = $"{request.Scheme}://{request.Host}/Song/GetCoverArt?id={id}",
                        Lyrics = properties["LYRICS"].Value
                    };
                }
                return null;
            }
            catch { return null; }
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
                case "HAS_ARTWORK":
                    result.Value = (file.Tag.Pictures.Length > 0).ToString();
                    break;
                case "NAME":
                    result.Value = file.Tag.Title != null ? file.Tag.Title : Path.GetFileNameWithoutExtension(file.Name);
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
                case "LYRICS":
                    result.Value = file.Tag.Lyrics;
                    break;
                case "MIME":
                    result.Value = file.MimeType;
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

                var properties = (from property in songPropertySet
                                  join p in propertySet on property.PropertyId equals p.Id
                                  orderby property.Id
                                  where property.SongId == id && p.Name == "NAME"
                                  select new CompleteSongProperty
                                  {
                                      Id = property.Id,
                                      Creation = property.Creation,
                                      LastModification = property.LastModification,
                                      PropertyId = p.Id,
                                      Name = p.Name,
                                      Type = p.Type,
                                      Description = p.Description,
                                      SongId = property.SongId,
                                      Value = property.Value
                                  }).ToList();

                string contentType = new FileExtensionContentTypeProvider().TryGetContentType(model.Url, out contentType) ?
                    contentType : "application/octet-stream";

                CompleteSong result = new CompleteSong() { Song = model, Type = contentType, File = new StreamReader(model.Url).BaseStream, Properties = properties};
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