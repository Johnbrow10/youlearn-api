using System;
using System.Collections.Generic;
using YouLearn.Domain.Entidades;

namespace YouLearn.Domain.Interfaces.Repositorios
{
    // IRepositorio e o metodo ou 'contrato' que vai pegar o dado na entidade para verificar nos argumentos.
    public interface IRepositorioCanal
    {

        IEnumerable<Canal> Listar(Guid idUsuario);

        Canal Obter(Guid Id);

        Canal Adicionar(Canal canal);

        void Excluir(Canal idCanal);
        
     
    }
}
