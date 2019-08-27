using System;
using System.Collections.Generic;
using YouLearn.Domain.Entidades;

namespace YouLearn.Domain.Interfaces.Repositorios
{
    // IRepositorio e o metodo ou 'contrato' que vai pegar o dado na entidade para verificar nos argumentos.
    public interface IRepositorioPlayList
    {

        IEnumerable<PlayList> Listar(Guid idUsuario);

        PlayList Obter(Guid Id);

        PlayList Adicionar(PlayList playlist);

        void Excluir(PlayList playlist);
        
     
    }
}
