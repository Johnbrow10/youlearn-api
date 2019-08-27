using System;
using YouLearn.Domain.Entidades;

namespace YouLearn.Domain.Argumentos.PlayList
{
    public class PlayListResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public static explicit operator PlayListResponse(Entidades.PlayList entidade)
        {
            return new PlayListResponse()
            {
                Id = entidade.Id,
                Nome = entidade.Nome
            };
        }
    }
}
