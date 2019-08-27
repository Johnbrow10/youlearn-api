using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouLearn.Domain.Entidades;


namespace YouLearn.Infra.Persistencia.EF.Map
{
    public class MapCanal : IEntityTypeConfiguration<Canal>
    {
        public void Configure(EntityTypeBuilder<Canal> builder)
        {
            // Nome da Tabela no Banco
            builder.ToTable("Canal");

            // Chave Estrangeira 
            builder.HasOne(x => x.Usuario).WithMany().HasForeignKey("IdUsuario");

            // Chave Primaria
            builder.HasKey(x => x.Id);

            // Propriedades do Banco
            builder.Property(x => x.Nome).HasMaxLength(50).IsRequired();
            builder.Property(x => x.UrlLogo).HasMaxLength(300).IsRequired();


        }
    }
}
