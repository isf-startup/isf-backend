using Microsoft.EntityFrameworkCore;
using ISF.Data.Model; 

namespace ISF.Data.Model
{
    public class ISFDbContext : DbContext
    {
        public ISFDbContext(DbContextOptions<ISFDbContext> options) : base(options) { }

        // Örnek DbSet — buraya tüm entity’lerini eklemelisin
        public DbSet<Sentence> Sentences { get; set; }
        public DbSet<SoundData> SoundDatas { get; set; }
        public DbSet<UserData> UserDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Fluent API konfigürasyonları gerekiyorsa buraya ekleyebilirsin.
        }
    }
}
