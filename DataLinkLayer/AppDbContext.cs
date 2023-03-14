using Microsoft.EntityFrameworkCore;
using MinimalAPI.Models;
using System.Security.Cryptography.X509Certificates;

namespace MinimalAPI.DataLinkLayer
{
    public class AppDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext>options): base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var sqlString = configuration["ConnectionStrings:DefaultConnectionString"];
            optionsBuilder.UseSqlServer(sqlString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasNoKey();
            base.OnModelCreating(modelBuilder);
        }


        }
    }

