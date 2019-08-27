using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouLearn.Domain.Entidades;

namespace YouLearn.Infra.Persistencia.EF.Map
{
    public class MapVideo : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            // Nome da Tabela no Banco
            builder.ToTable("Video");

            // Chaves estrangeiras
            builder.HasOne(x => x.UsuarioSugeriu).WithMany().HasForeignKey("IdUsuario");
            builder.HasOne(x => x.Canal).WithMany().HasForeignKey("IdCanal");
            builder.HasOne(x => x.PlayList).WithMany().HasForeignKey("IdPlayList");

            // Chave Primaria
            builder.HasKey(x => x.Id);

            // Propriedades da Tabela
            builder.Property(x => x.Titulo).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Descricao).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Tags).HasMaxLength(50).IsRequired();
            builder.Property(x => x.OrdemNaPlayList);
            builder.Property(x => x.IdVideoYoutube).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Status);


        }
    }
}
