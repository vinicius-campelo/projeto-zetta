using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MeetGroupAPI.Models
{
    public class ApiDbContext : DbContext
    {
        private readonly IConfiguration _config;


        public virtual DbSet<Equipamento> Equipamento { get; set; }
        public virtual DbSet<Reserva> Reserva { get; set; }
        public virtual DbSet<Sala> Sala { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        public ApiDbContext(IConfiguration config)
        {
            _config = config;
        }

        public ApiDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .UseNpgsql(_config.GetConnectionString("PostgresDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiDbContext).Assembly);
        }
    }
}
