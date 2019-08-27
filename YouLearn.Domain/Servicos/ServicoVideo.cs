using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YouLearn.Domain.Argumentos.Video;
using YouLearn.Domain.Entidades;
using YouLearn.Domain.Interfaces.Repositorios;
using YouLearn.Domain.Interfaces.Serviços;
using YouLearn.Domain.Resources;

namespace YouLearn.Domain.Servicos
{
    public class ServicoVideo : Notifiable, IServiceVideo
    {

        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IRepositorioCanal _repositorioCanal;
        private readonly IRepositorioPlayList _repositorioPlayList;
        private readonly IRepositorioVideo _repositorioVideo;

        public ServicoVideo(IRepositorioUsuario repositorioUsuario, IRepositorioCanal repositorioCanal, IRepositorioPlayList repositorioPlayList, IRepositorioVideo repositorioVideo)
        {
            _repositorioUsuario = repositorioUsuario;
            _repositorioCanal = repositorioCanal;
            _repositorioPlayList = repositorioPlayList;
            _repositorioVideo = repositorioVideo;
        }

        public AdicionarVideoResponse AdicionarVideo(AdicionarVideoRequest request, Guid idUsuario)
        {

            if (request == null)
            {
                AddNotification("AdicionarVideoRequest", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("AdicionarVideoRequest"));
                return null;
            }

            Usuario usuario = _repositorioUsuario.Obter(idUsuario);
            if (usuario == null)
            {
                AddNotification("Usuario", MSG.X0_NAO_INFORMADO.ToFormat("Usuario"));
                return null;
            }

            Canal canal = _repositorioCanal.Obter(request.IdCanal);
            if (canal == null)
            {
                AddNotification("Canal", MSG.X0_NAO_INFORMADO.ToFormat("Canal"));
                return null;
            }

            PlayList playList = null;
            if (request.IdPlayList != Guid.Empty)
            {

                playList = _repositorioPlayList.Obter(request.IdPlayList);

                if (playList == null)
                {
                    AddNotification("PlayList", MSG.X0_NAO_INFORMADO.ToFormat("playList"));
                    return null;
                }

            }

            var video = new Video(canal, playList, request.Titulo, request.Descricao, request.Tags, request.OrdemNaPlayList, request.IdVideoYoutube, usuario);

            AddNotifications(video);

            if (this.IsInvalid())
            {
                return null;
            }

            _repositorioVideo.Adicionar(video);

            return new AdicionarVideoResponse(video.Id);
        }

        // Listar por Tags
        public IEnumerable<VideoResponse>Listar(string tags)
        {
            IEnumerable<Video> videoCollection = _repositorioVideo.Listar(tags);
            var response = videoCollection.ToList().Select(entidade => (VideoResponse)entidade);
            return response;
        }

        //Listar por Id das playLists.
        public IEnumerable<VideoResponse> Listar(Guid idPlayList)
        {
            IEnumerable<Video> videoCollection = _repositorioVideo.Listar(idPlayList);
            var response = videoCollection.ToList().Select(entidade => (VideoResponse)entidade);
            return response;
        }
    }
}
