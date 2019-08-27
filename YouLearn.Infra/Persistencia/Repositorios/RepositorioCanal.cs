using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YouLearn.Domain.Entidades;
using YouLearn.Domain.Interfaces.Repositorios;
using YouLearn.Infra.Persistencia.EF;

namespace YouLearn.Infra.Persistencia.Repositorios
{
    public class RepositorioCanal : IRepositorioCanal
    {
        private readonly YouLearnContext _context;

        public RepositorioCanal(YouLearnContext context)
        {
            _context = context;
        }

        public Canal Adicionar(Canal canal)
        {
            _context.Canais.Add(canal);
            return canal;
        }

        public void Excluir(Canal canal)
        {
            _context.Canais.Remove(canal);
        }

        public IEnumerable<Canal> Listar(Guid idUsuario)
        {
            return _context.Canais.Where(x => x.Usuario.Id == idUsuario).ToList();
        }

        public Canal Obter(Guid IdCanal)
        {
            return _context.Canais.FirstOrDefault(x => x.Id == IdCanal);
        }
    }
}
