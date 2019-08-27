using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouLearn.Domain.Entidades;

namespace YouLearn.Infra.Persistencia.EF.Map
{
    public class MapPlaylist : IEntityTypeConfiguration<PlayList>
    {
        public void Configure(EntityTypeBuilder<PlayList> builder)
        {
            // Nome da Tabela
            builder.ToTable("PlayList");

            // Chave Estrangeira
            builder.HasOne(x => x.Usuario).WithMany().HasForeignKey("IdUsuario");

            // Chave Primaria 
            builder.HasKey(x => x.Id);

            // Propriedades
            builder.Property(x => x.Nome).HasMaxLength(50).IsRequired();
        }
    }
}
