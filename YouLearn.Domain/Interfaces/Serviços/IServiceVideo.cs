using System;
using System.Collections.Generic;
using System.Text;
using YouLearn.Domain.Argumentos.Video;
using YouLearn.Domain.Interfaces.Serviços.Base;

namespace YouLearn.Domain.Interfaces.Serviços
{
    public interface IServiceVideo : IserviceBase
    {
        AdicionarVideoResponse AdicionarVideo(AdicionarVideoRequest request, Guid idusuario);

        // Listar Pelas Tags 
        IEnumerable<VideoResponse> Listar(string Tags);
        // Listar Pelo Id das PlayLists
        IEnumerable<VideoResponse> Listar(Guid idPlayList);
    }
}
