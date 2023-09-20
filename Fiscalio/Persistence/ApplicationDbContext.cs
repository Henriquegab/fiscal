using Fiscalio.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiscalio.Persistence
{
    public class AppDbContext : DbContext
    {
        private static AppDbContext _instance;
        private static readonly object _lock = new object();

        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<NotaFiscal> NotaFiscais { get; set; }
        public DbSet<Item> Itens { get; set; }

        
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

                e.HasMany(nf => nf.Itens)
                .WithOne()
                .HasForeignKey(item => item.IdNota)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);



            });

            modelBuilder.Entity<Item>(e =>
            {
                e.HasKey(de => de.IdItem);

                


            });



        }

            

        
    }
}