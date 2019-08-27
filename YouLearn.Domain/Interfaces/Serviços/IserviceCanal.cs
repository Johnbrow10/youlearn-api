using System;
using System.Collections.Generic;
using YouLearn.Domain.Argumentos.Base;
using YouLearn.Domain.Argumentos.Canal;
using YouLearn.Domain.Interfaces.Serviços.Base;

namespace YouLearn.Domain.Interfaces.Serviços
{
    public interface IServiceCanal : IserviceBase
    {
        IEnumerable<CanalResponse> Listar(Guid idUsuario);
        CanalResponse AdicionarCanal(AdicionarCanalRequest request, Guid idUsuario);
        Response ExcluirCanal(Guid idCanal);
    }
}
