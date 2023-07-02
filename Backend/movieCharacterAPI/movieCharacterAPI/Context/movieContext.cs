using Microsoft.EntityFrameworkCore;
using movieCharacterAPI.Models;

namespace movieCharacterAPI.Context
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions options) : base(options)
        {  }
        // dbSets for each entity
        public DbSet<MovieEntity> Movies { get; set; }
        public DbSet<FranchiseEntity> Franchises { get; set; }
        public DbSet<CharacterEntity> Characters { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<FranchiseEntity>(entity =>
            {
                // OnModelClient overrides default databasecreation with Fluent API 
                //Set primary key
                entity.HasKey(x => x.FranchiseId);
                //Adding properties with character lenght limitation
                entity.Property(x => x.Name).HasMaxLength(50);
                entity.Property(x => x.Description).HasMaxLength(300);
                //Setting relationships for Franchise
                entity.HasMany(x => x.Movies)
                .WithOne(e => e.Franchise)
                .HasForeignKey(e => e.FranchiseId).IsRequired(false);
                //setforeign key, nullable
            });
            modelBuilder.Entity<MovieEntity>(entity =>
            {
                entity.HasKey(x => x.MovieId);
                entity.Property(x => x.Title).HasMaxLength(50);
                entity.Property(x => x.Genre).HasMaxLength(50);
                entity.Property(x => x.ReleaseYear).HasMaxLength(5);
                entity.Property(x => x.Director).HasMaxLength(50);
                entity.Property(x => x.Picture).HasMaxLength(100);
                entity.Property(x => x.Trailer).HasMaxLength(100);
                entity.HasMany(x => x.Character)
                .WithMany(e => e.Movies);
                entity.HasOne(movie => movie.Franchise)
                .WithMany(francise => francise.Movies)
                .HasForeignKey(movie => movie.FranchiseId).IsRequired(false);
            });
            modelBuilder.Entity<CharacterEntity>(entity =>
            {
                entity.HasKey(x => x.CharacterId);
                entity.Property(x => x.Name).HasMaxLength(50);
                entity.Property(x => x.Alias).HasMaxLength(50);
                entity.Property(x => x.Gender).HasMaxLength(10);
                entity.Property(x => x.Picture).HasMaxLength(100);
                entity.HasMany(x => x.Movies)
                .WithMany(e => e.Character);
            });
        
        }
       
     

    }
}



