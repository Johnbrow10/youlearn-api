using System;
using System.Linq;
using YouLearn.Domain.Entidades;
using YouLearn.Domain.Interfaces.Repositorios;
using YouLearn.Domain.ValueObjects;
using YouLearn.Infra.Persistencia.EF;

namespace YouLearn.Infra.Persistencia.Repositorios
{

    public class RepositorioUsuario : IRepositorioUsuario
    {
        private readonly YouLearnContext _contexto;

        public RepositorioUsuario(YouLearnContext contexto)
        {
            _contexto = contexto;
        }


        public bool Existe(string email)
        {
            return _contexto.Usuarios.Any(x => x.Email.Endereco == email);
        }

        public Usuario Obter(Guid id)
        {
            return _contexto.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public Usuario Obter(string email, string senha)
        {
            return _contexto.Usuarios.FirstOrDefault(x => x.Email.Endereco == email && x.Senha == senha);

        }


        public void Salvar(Usuario usuario)
        {
            _contexto.Usuarios.Add(usuario);
        }
    }
}
