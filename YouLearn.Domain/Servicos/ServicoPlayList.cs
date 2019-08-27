using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using YouLearn.Domain.Argumentos.Base;
using YouLearn.Domain.Argumentos.PlayList;
using YouLearn.Domain.Entidades;
using YouLearn.Domain.Interfaces.Repositorios;
using YouLearn.Domain.Interfaces.Serviços;
using YouLearn.Domain.Resources;

namespace YouLearn.Domain.Servicos
{
    public class ServicoPlayList : Notifiable, IServicePlayList
    {

        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IRepositorioPlayList _repositorioPlayList;
        private readonly IRepositorioVideo _repositorioVideo;

        public ServicoPlayList(IRepositorioUsuario repositorioUsuario, IRepositorioPlayList repositorioPlayList, IRepositorioVideo repositorioVideo)
        {
            _repositorioUsuario = repositorioUsuario;
            _repositorioPlayList = repositorioPlayList;
            _repositorioVideo = repositorioVideo;
        }

        public PlayListResponse AdicionarPlayList(AdicionarPlayListRequest request, Guid idUsuario)
        {
            Usuario usuario = _repositorioUsuario.Obter(idUsuario);

            PlayList playlist = new PlayList(request.Nome, usuario);

            AddNotifications(playlist);

            if (this.IsInvalid()) return null;

            playlist = _repositorioPlayList.Adicionar(playlist);

            return (PlayListResponse)playlist;

        }

        public Argumentos.Base.Response ExcluirPlayList(Guid idPlayList)
        {
            bool existe = _repositorioVideo.ExistePlayListAssociada(idPlayList);
          
            if (existe)
            {
                AddNotification("PlayList", MSG.NAO_E_POSSIVEL_EXCLUIR_UMA_X0_ASSOCIADA_A_UM_X1.ToFormat("PlayList", "Video"));
                return null;
            }

            PlayList playlist = _repositorioPlayList.Obter(idPlayList);

            if (playlist == null)
            {
                AddNotification("PlayList", MSG.DADOS_NAO_ENCONTRADOS);
            }

            if (this.IsInvalid()) return null;

            _repositorioPlayList.Excluir(playlist);

            return new Argumentos.Base.Response() { Mensagem = MSG.OPERACAO_REALIZADA_COM_SUCESSO };

        }

        public IEnumerable<PlayListResponse> Listar(Guid idUsuario)
        {
            IEnumerable<PlayList> PlayListCollection = _repositorioPlayList.Listar(idUsuario);

            var response = PlayListCollection.ToList().Select(entidade => (PlayListResponse)entidade);

            return response;
        }
    }
}
