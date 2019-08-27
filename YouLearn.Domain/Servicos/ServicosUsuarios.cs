using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using YouLearn.Domain.Argumentos.Usuario;
using YouLearn.Domain.Entidades;
using YouLearn.Domain.Interfaces.Repositorios;
using YouLearn.Domain.Interfaces.Serviços;
using YouLearn.Domain.Resources;
using YouLearn.Domain.ValueObjects;

namespace YouLearn.Domain.Servicos
{
    public class ServicosUsuarios : Notifiable, IServiceUsuario
    {

        // Dependencias Do ServicoUsuario
        private readonly IRepositorioUsuario _repositorioUsuario;

        //Construtor
        public ServicosUsuarios(IRepositorioUsuario repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario;
        }


        public AdicionarUsuarioResponse AdicionarUsuario(AdicionarUsuarioRequest request)
        {
            if (request == null)
            {
                //Verifica se tem algum dado na requisicao
                AddNotification("AdicionarUsuarioRequest", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("AdicionarUsuarioRequest"));
                return null;
            }

            //Cria Objetos Com Valore,s
            Nome nome = new Nome(request.PrimeiroNome, request.UltimoNome);
            Email email = new Email(request.Email);

            //Cria Entidade no Banco.
            Usuario usuario = new Usuario(nome, email, request.Senha);

            //Verifica noticacoes das Verificações de Erros.
            AddNotifications(nome, email, usuario);


            if (this.IsInvalid()) { return null; }

            // Depois das validaçoes pode se salvar No Banco de Dados
            _repositorioUsuario.Salvar(usuario);

            return new AdicionarUsuarioResponse(usuario.Id);

        }


        public AutenticarUsuarioResponse AutenticarUsuario(AutenticarUsuarioRequest request)
        {
            if (request == null)
            {
                AddNotification("AutenticarUsuarioRequest", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("AutenticarUsuarioRequest"));
                return null;
            }

            Email email = new Email(request.Email);
            Usuario usuario =  new Usuario(email, request.Senha);

            AddNotifications(usuario);

            if (this.IsInvalid()) return null;

            usuario = _repositorioUsuario.Obter(usuario.Email.Endereco, usuario.Senha);

            if (usuario == null)
            {
                AddNotification("Usuario", MSG.DADOS_NAO_ENCONTRADOS);
                return null;
            }

            var response = (AutenticarUsuarioResponse)usuario;
            

            return response;

        }
    }
}


