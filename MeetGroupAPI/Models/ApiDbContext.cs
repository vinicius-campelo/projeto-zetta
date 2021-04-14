using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MeetGroupAPI.Models
{
    public class ApiDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public ApiDbContext(IConfiguration config)
        {
            _config = config;
        }

        public ApiDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Equipamento> Equipamento { get; set; }
        public virtual DbSet<Reserva> Reserva { get; set; }
        public virtual DbSet<Sala> Sala { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        public static string connectionString = "host=172.17.0.1;port=5432;database=meetgroupdb;username=postgres;password=postgres;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql(connectionString);

            //optionsBuilder
            //.UseNpgsql(_config.GetConnectionString("PostgresDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiDbContext).Assembly);
        }
    }
}
