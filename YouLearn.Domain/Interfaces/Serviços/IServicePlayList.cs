using System;
using System.Collections.Generic;
using System.Text;
using YouLearn.Domain.Argumentos.Base;
using YouLearn.Domain.Argumentos.PlayList;
using YouLearn.Domain.Interfaces.Serviços.Base;

namespace YouLearn.Domain.Interfaces.Serviços
{
    public interface IServicePlayList : IserviceBase
    {
        IEnumerable<PlayListResponse> Listar(Guid idUsuario);
        PlayListResponse AdicionarPlayList(AdicionarPlayListRequest request, Guid idUsuario);
        Response ExcluirPlayList(Guid idPlayList);
    }
}
