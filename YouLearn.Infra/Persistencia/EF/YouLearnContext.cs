using Microsoft.EntityFrameworkCore;
using prmToolkit.NotificationPattern;
using YouLearn.Domain.Entidades;
using YouLearn.Domain.ValueObjects;
using YouLearn.Infra.Persistencia.EF.Map;
using YouLearn.Shared;

namespace YouLearn.Infra.Persistencia.EF
{
    public class YouLearnContext : DbContext
    {
        public DbSet <Canal> Canais { get; set; }
        public DbSet <PlayList> PlayLists { get; set; }
        public DbSet <Video> Videos { get; set; }
        public DbSet <Usuario> Usuarios { get; set; }
       

        //Confirmaçao de uma conexao com o Banco
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuracao.ConnectionString);
            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ignorar Classes
            modelBuilder.Ignore<Notification>();
            modelBuilder.Ignore<Nome>();
            modelBuilder.Ignore<Email>();

            // Aplicar Configuracoes 
            modelBuilder.ApplyConfiguration(new MapCanal());
            modelBuilder.ApplyConfiguration(new MapPlaylist());
            modelBuilder.ApplyConfiguration(new MapUsuario());
            modelBuilder.ApplyConfiguration(new MapVideo());
            //modelBuilder.ApplyConfiguration(new MapFavorito());

            base.OnModelCreating(modelBuilder);
        }

    }
}
