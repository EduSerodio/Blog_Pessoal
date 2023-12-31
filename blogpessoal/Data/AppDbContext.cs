using blogpessoal.Model;
using Microsoft.EntityFrameworkCore;

namespace blogpessoal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Postagem>().ToTable("tb_postagens");
            modelBuilder.Entity<Tema>().ToTable("tb_temas");
            modelBuilder.Entity<User>().ToTable("tb_usuarios");

            //criando o relacionamento das duas entidades a cima
            _= modelBuilder.Entity<Postagem>()
                .HasOne( _ => _.Tema )
                .WithMany( t => t.Postagem )
                .HasForeignKey("TemaId")
                .OnDelete(DeleteBehavior.Cascade); //apaga todas as postagens referente ao tema apagado 
                //MODO CASCATA
                
             // Relacionamento Postagem -> User
            _ = modelBuilder.Entity<Postagem>()
                .HasOne(_ => _.Usuario)
                .WithMany(u => u.Postagem)
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade);
        }

        // Registrar DbSet - Objeto responsável por manipular a tabela
        public DbSet<Postagem> Postagens {get; set;} = null!;
        public DbSet<Tema> Tema {get; set;} = null!;
        public DbSet<User> Users { get; set; } = null!;

        // método referente a class Auditable
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var insertedEntries = this.ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added)
                .Select(x => x.Entity);

            foreach (var insertedEntry in insertedEntries)
            {
                //Se uma propriedade da Classe Auditable estiver sendo criada. 
                if (insertedEntry is Auditable auditableEntity)
                {
                    auditableEntity.Data = DateTimeOffset.Now;
                }
            }

            var modifiedEntries = ChangeTracker.Entries()
                    .Where(x => x.State == EntityState.Modified)
                    .Select(x => x.Entity);

            foreach (var modifiedEntry in modifiedEntries)
            {
                //Se uma propriedade da Classe Auditable estiver sendo atualizada.  
                if (modifiedEntry is Auditable auditableEntity)
                {
                    auditableEntity.Data = DateTimeOffset.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}