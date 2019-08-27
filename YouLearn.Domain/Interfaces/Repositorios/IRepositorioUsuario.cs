using System;
using YouLearn.Domain.Entidades;

namespace YouLearn.Domain.Interfaces.Repositorios
{
    // IRepositorio e o metodo ou 'contrato' que vai pegar o dado na entidade para verificar nos argumentos.
    public interface IRepositorioUsuario
    {
        Usuario Obter(Guid Id);
        
        //string email se der erro
        Usuario Obter(string email, string senha);

        void Salvar(Usuario usuario);
        bool Existe(string email);
    }
}
