using Fiscalio.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiscalio.Persistence
{
    public class AppDbContext : DbContext
    {
        private static AppDbContext _instance;
        private static readonly object _lock = new object();

        // Construtor privado para impedir a criação de instâncias externas
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<NotaFiscal> NotaFiscais { get; set; }
        public DbSet<Item> Itens { get; set; }

        // Método para obter a instância Singleton do contexto
        public static AppDbContext GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        // Configure a string de conexão aqui
                        var connectionString = "server=localhost;user=root;password=;database=fiscal";
                        var serverVersion = new MySqlServerVersion(new Version(10, 4, 27));
                        var options = new DbContextOptionsBuilder<AppDbContext>()
                            .UseMySql(connectionString, serverVersion)
                            .Options;

                        _instance = new AppDbContext(options);
                    }
                }
            }
            return _instance;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<NotaFiscal>(e =>
            {
                e.HasKey(de => de.IdNota);

                e.Property(de => de.Emissor).HasMaxLength(150).HasColumnType("text");



            });

            modelBuilder.Entity<Item>(e =>
            {
                e.HasKey(de => de.IdItem);

                e.HasOne<NotaFiscal>().WithMany().HasForeignKey(de => de.IdNota);


            });



        }

            

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var connectionString = "server=localhost;user=root;password=;database=fiscal";

            var serverVersion = new MySqlServerVersion(new Version(10, 4, 27));
            optionsBuilder.UseMySql(connectionString, serverVersion);
        }
        */
    }
}