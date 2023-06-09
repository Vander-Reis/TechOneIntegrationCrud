using Microsoft.EntityFrameworkCore;
using PersonApi.Domain;

namespace PersonApi.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            // Garantir que o banco de dados seja criado e as migrações sejam aplicadas
            Database.EnsureCreated();
            Database.Migrate();
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Pessoa>(entity =>
        //    {
        //        entity.HasKey(e => e.ID);
        //        entity.Property(e => e.Nome).IsRequired();
        //        entity.Property(e => e.Email).IsRequired();
        //        entity.Property(e => e.Telefone).IsRequired();
        //    });

        //    base.OnModelCreating(modelBuilder);
        //}

        public void CriarTabelaPessoas()
        {
            if (!Database.GetPendingMigrations().Any())
            {
                Database.ExecuteSqlRaw("CREATE TABLE IF NOT EXISTS Pessoas (ID INTEGER PRIMARY KEY AUTOINCREMENT, Nome TEXT NOT NULL, Email TEXT NOT NULL, Telefone TEXT NOT NULL)");
            }
        }
    }
}
