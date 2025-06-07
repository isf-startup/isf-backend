using Microsoft.EntityFrameworkCore;
using ISF.Data.Model; 

namespace ISF.Data.Model
{
    public class ISFDbContext : DbContext
    {
        public ISFDbContext(DbContextOptions<ISFDbContext> options) : base(options) { }

        // Örnek DbSet — buraya tüm entity’lerini eklemelisin
        public DbSet<Sentence> Sentences { get; set; }
        public DbSet<UserData> UserDatas { get; set; }
        public DbSet<SoundModel> SoundModels { get; set; }
        public DbSet<FavoriteSentence> FavoriteSentences { get; set; }
        public DbSet<FavoriteSound> FavoriteSounds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Fluent API konfigürasyonları gerekiyorsa buraya ekleyebiliriz
        }
    }
}
