using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using PlaylistAPI.Models;

namespace PlaylistAPI.Business
{
    public class PlaylistBusiness : BaseBusiness<Playlist>
    {
        public PlaylistBusiness() : base(null)
        {
        }

        public IQueryable<Playlist> GetAllFromUser(int Id)
        {
            var playlistSet = Context.ArquireDbSet<Playlist>();
            return playlistSet.Where(item => item.OwnerID == Id);
        }

        public CompleteSong GetCompleteSong(int id, HttpRequest request)
        {
            try
            {
                var songSet = Context.ArquireDbSet<Song>();
                Song model = (from song in songSet where song.Id == id select song).FirstOrDefault();

                if (!File.Exists(model.Url))
                    return null;

                var songPropertySet = Context.ArquireDbSet<SongProperty>();
                var propertySet = Context.ArquireDbSet<Property>();

                var properties = (from property in songPropertySet
                    join p in propertySet on property.PropertyId equals p.Id
                    where property.SongId == model.Id
                    orderby property.Id
                    select new CompleteSongProperty {
                        Id = property.Id,
                        Creation = property.Creation,
                        LastModification = property.LastModification,
                        Name = p.Name,
                        Type = p.Type,
                        Description = p.Description,
                        Value = property.Value
                    });

                string contentType = new FileExtensionContentTypeProvider().TryGetContentType(model.Url, out contentType) ?
                    contentType : "application/octet-stream";
                
                CompleteSong result = new CompleteSong() { Song = model, Type = contentType, RemoteUrl = $"{request.Scheme}://{request.Host}/Song/GetFile?id={model.Id}", Properties = properties.ToList() };
                return result;
            }
            catch { return null; }
        }
    }
}