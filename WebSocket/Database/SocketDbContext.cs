using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using WebSocket.Database.Entity;

namespace WebSocket.Database
{
    public class SocketDbContext : DbContext
    {
        public SocketDbContext()
        {
            
        }

        public SocketDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        {
            var connectionString = "Server=127.0.0.1;Port=3306;Database=Socket;User Id=root;Password=vantrong;Allow Zero Datetime=true;Convert Zero Datetime=true;Old Guids=true";

            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(11,5,2)));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}
