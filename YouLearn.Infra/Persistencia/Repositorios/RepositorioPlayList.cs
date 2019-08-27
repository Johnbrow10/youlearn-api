using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YouLearn.Domain.Entidades;
using YouLearn.Domain.Interfaces.Repositorios;
using YouLearn.Infra.Persistencia.EF;

namespace YouLearn.Infra.Persistencia.Repositorios
{
    public class RepositorioPlayList : IRepositorioPlayList
    {
        private readonly YouLearnContext _context;

        public RepositorioPlayList(YouLearnContext context)
        {
            _context = context;
        }

        public PlayList Adicionar(PlayList playlist)
        {
            _context.PlayLists.Add(playlist);
            return playlist;
        }

        public void Excluir(PlayList playlist)
        {
            _context.PlayLists.Remove(playlist);
        }

        public IEnumerable<PlayList> Listar(Guid idUsuario)
        {
            return _context.PlayLists.Where(x => x.Usuario.Id == idUsuario).ToList();
        }

        public PlayList Obter(Guid idPlayList)
        {
            return _context.PlayLists.FirstOrDefault(x => x.Id == idPlayList);
        }
    }
}
