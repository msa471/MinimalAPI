using Microsoft.EntityFrameworkCore;
using MinimalAPI.Models;

public class UserDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.;Database=Users;Trusted_Connection=True;MultipleActiveResultSets=true");
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<User>().HasNoKey();
        modelBuilder.Entity<Actor_Movie>().HasKey(am => new
        { 
            am.ActorId,
            am.MovieId
        });

        modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Movie).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.MovieId);
        modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Actor).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.ActorId);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Actor> Actors { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Actor_Movie> Actors_Movies { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Producer> Producers { get; set; }
}