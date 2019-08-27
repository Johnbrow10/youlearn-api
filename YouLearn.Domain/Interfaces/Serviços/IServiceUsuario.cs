using YouLearn.Domain.Argumentos.Usuario;
using YouLearn.Domain.Interfaces.Serviços.Base;

namespace YouLearn.Domain.Interfaces.Serviços
{
    public interface IServiceUsuario : IserviceBase
    {
        AdicionarUsuarioResponse AdicionarUsuario(AdicionarUsuarioRequest request);
        AutenticarUsuarioResponse AutenticarUsuario(AutenticarUsuarioRequest request);

    }
}
