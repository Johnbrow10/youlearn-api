using System;

namespace YouLearn.Domain.Argumentos.Usuario
{
    public class AutenticarUsuarioResponse
    {
        public Guid Id { get; set; }
        public string PrimeiroNome { get; set; }

        public static explicit operator AutenticarUsuarioResponse(Entidades.Usuario entidade)
        {
            return new AutenticarUsuarioResponse
            {
                Id = entidade.Id,
                PrimeiroNome = entidade.Nome.PrimeiroNome
            };
        }
    }
}
