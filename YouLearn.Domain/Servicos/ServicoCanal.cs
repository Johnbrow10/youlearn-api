using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using YouLearn.Domain.Argumentos.Base;
using YouLearn.Domain.Argumentos.Canal;
using YouLearn.Domain.Entidades;
using YouLearn.Domain.Interfaces.Repositorios;
using YouLearn.Domain.Interfaces.Serviços;
using YouLearn.Domain.Resources;

namespace YouLearn.Domain.Servicos
{
    public class ServicoCanal : Notifiable, IServiceCanal
    {

        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IRepositorioCanal _repositorioCanal;
        private readonly IRepositorioVideo _repositorioVideo;

        public ServicoCanal(IRepositorioUsuario repositorioUsuario, IRepositorioCanal repositorioCanal, IRepositorioVideo repositorioVideo)
        {
            _repositorioUsuario = repositorioUsuario;
            _repositorioCanal = repositorioCanal;
            _repositorioVideo = repositorioVideo;
        }

        public CanalResponse AdicionarCanal(AdicionarCanalRequest request, Guid idUsuario)
        {
            Usuario usuario = _repositorioUsuario.Obter(idUsuario);

            Canal canal = new Canal(request.Nome, request.UrlLogo, usuario);

            AddNotifications(canal);

            if (this.IsInvalid()) return null;

            canal = _repositorioCanal.Adicionar(canal);

            return (CanalResponse)canal;
           
        }

        public Argumentos.Base.Response ExcluirCanal(Guid idCanal)
        {
            bool existe = _repositorioVideo.ExisteCanalAssociado(idCanal);
          

            if (existe)
            {
                AddNotification("Canal", MSG.NAO_E_POSSIVEL_EXCLUIR_UM_X0_ASSOCIADO_A_UM_X1.ToFormat("Canal", "Video"));
                return null;
            }

            Canal canal = _repositorioCanal.Obter(idCanal);

            if (canal == null)
            {
                AddNotification("Canal", MSG.DADOS_NAO_ENCONTRADOS);
            }

            if (this.IsInvalid()) return null;

            _repositorioCanal.Excluir(canal);

            return new Argumentos.Base.Response() { Mensagem = MSG.OPERACAO_REALIZADA_COM_SUCESSO };
        }

        public IEnumerable<CanalResponse> Listar(Guid idUsuario)
        {
            IEnumerable<Canal> CanalCollection = _repositorioCanal.Listar(idUsuario);
            var response = CanalCollection.ToList().Select(entidade => (CanalResponse)entidade);
            return response;
        }
    }
}
