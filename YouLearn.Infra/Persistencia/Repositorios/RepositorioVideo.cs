using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YouLearn.Domain.Entidades;
using YouLearn.Domain.Interfaces.Repositorios;
using YouLearn.Infra.Persistencia.EF;

namespace YouLearn.Infra.Persistencia.Repositorios
{
    public class RepositorioVideo : IRepositorioVideo
    {
        private readonly YouLearnContext _context;

        public RepositorioVideo(YouLearnContext context)
        {
            _context = context;
        }

        public void Adicionar(Video video)
        {
            _context.Videos.Add(video);
        }

        public bool ExisteCanalAssociado(Guid idCanal)
        {
            return _context.Videos.Any(x => x.Canal.Id == idCanal);
        }

        public bool ExistePlayListAssociada(Guid idPlayList)
        {
            return _context.Videos.Any(x => x.PlayList.Id == idPlayList);
        }


        public IEnumerable<Video> Listar(string tags)
        {
            // capturar as tags com include
            var query = _context.Videos.Include(x => x.Canal).Include(x => x.PlayList).AsQueryable();
            // usando split para cada espaco pegar o dado e colocalo em uma linha e com  foreach fazend um loop pra pesquisar 
            tags.Split(' ').ToList().ForEach(tag =>
            {
                // pesquisando em tags titulo e descricao, a tag que foi capturada na linha acima
                query = query.Where(x => x.Tags.Contains(tag) || x.Titulo.Contains(tag) || x.Descricao.Contains(tag));

            });
            // e no final retornando o valor para listar todas as tags
            return query.ToList();
        }


        //listar As Playlist Trazendo dados do Canal
        public IEnumerable<Video> Listar(Guid idPlayList)
        {
            return _context.Videos.Include(x => x.Canal).Include(x => x.PlayList)
               .Where(x => x.PlayList.Id == idPlayList).ToList();
        }
    }
}
