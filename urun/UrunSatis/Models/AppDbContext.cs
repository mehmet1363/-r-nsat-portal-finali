using AspNetCoreHero.ToastNotification.Notyf.Models;
using Microsoft.EntityFrameworkCore;
using NETCore.Encrypt.Extensions;

namespace urun.Models
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _config;
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config) : base(options)
        {
            _config = config;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Contact> Contacts { get; set; }


        public string MD5Hash(string pass)
        {
            var salt = _config.GetValue<string>("AppSettings:MD5Salt");
            var password = pass + salt;
            var hashed = password.MD5();
            return hashed;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<User>()
                .HasData(
                new User {Id=1,Username = "admin", Email = "admin",PasswordHash=MD5Hash("admin"),Role="admin"}
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
