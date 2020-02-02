using System.Linq;
using Microsoft.EntityFrameworkCore;
using PlaylistAPI.Models;

namespace PlaylistAPI
{
    public class PlaylistContext : DbContext
    {
        public PlaylistContext(DbContextOptions<PlaylistContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Comparator> Comparators { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<SongProperty> SongProperties { get; set; }
        public DbSet<PlaylistRule> PlaylistRules { get; set; }
        public DbSet<HardCodedEntry> HardCodedEntries { get; set; }
        public DbSet<TModel> ArquireDbSet<TModel>() where TModel : BaseModel
        {
            var dbSet = (from prop in this.GetType().GetProperties()
                where prop.PropertyType.Equals(typeof(DbSet<TModel>)) select prop.GetValue(this)).FirstOrDefault();
            
            return dbSet as DbSet<TModel>;
        }
    }
}