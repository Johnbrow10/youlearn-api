using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouLearn.Domain.Entidades;
using YouLearn.Domain.ValueObjects;

namespace YouLearn.Infra.Persistencia.EF.Map
{
    public class MapUsuario : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            // Nome da Tabela
            builder.ToTable("Usuario");

            // Chave Primaria
            builder.HasKey(x => x.Id);

            // O resto dos campos
            builder.Property(x => x.Senha).HasMaxLength(32).IsRequired();
            
            // Mapeando Objetos de Valor EntityFramework
            builder.OwnsOne<Nome>(x => x.Nome, cb => {
                cb.Property(x => x.PrimeiroNome).HasMaxLength(50).HasColumnName("PrimeiroNome").IsRequired();
                cb.Property(x => x.UltimoNome).HasMaxLength(50).HasColumnName("UltimoNome").IsRequired();
            });

            builder.OwnsOne<Email>(x => x.Email, cb => {
                cb.Property(x => x.Endereco).HasMaxLength(100).HasColumnName("Endereco").IsRequired();
            });
        }
    }
}
