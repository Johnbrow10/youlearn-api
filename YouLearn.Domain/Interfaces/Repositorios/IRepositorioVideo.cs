using System;
using System.Collections.Generic;
using System.Text;
using YouLearn.Domain.Entidades;

namespace YouLearn.Domain.Interfaces.Repositorios
{
    public interface IRepositorioVideo
    {
        void Adicionar(Video video);

        IEnumerable<Video> Listar(String tags);
        IEnumerable<Video> Listar(Guid idPlayList);
        bool ExisteCanalAssociado(Guid idCanal);
        bool ExistePlayListAssociada(Guid idPlayList);

        
    }
}
