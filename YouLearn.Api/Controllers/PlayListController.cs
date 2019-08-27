using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouLearn.Api.Controllers.Base;
using YouLearn.Domain.Argumentos.PlayList;
using YouLearn.Domain.Argumentos.Usuario;
using YouLearn.Domain.Interfaces.Serviços;
using YouLearn.Infra.Transacoes;

namespace YouLearn.Api.Controllers
{
    public class PlayListController : BaseController
    {
        private readonly IServicePlayList _servicePlaylist;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PlayListController(IHttpContextAccessor httpContextAccessor, IUnitOfWork unit, IServicePlayList servicePlayList) : base(unit)
        {
            _servicePlaylist = servicePlayList;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("api/v1/PlayList/Listar")]

        public async Task<IActionResult> Listar()
        {

            try
            {
                // String capturada do claim feita no usuario contoller que tem os dados da requisicao.
                string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioClaims);
                // Esse autenticaar converte o objeto serializado em um Json podendo usar o nome e id do usuario.

                var response = _servicePlaylist.Listar(usuarioResponse.Id);

                return await ResponseAsync(response, _servicePlaylist);
            }

            catch (Exception ex)
            {
                return ResponseException(ex);

            }

        }

        [HttpPost]
        [Route("api/v1/PlayList/Adicionar")]
        public async Task<IActionResult> Adicionar([FromBody]AdicionarPlayListRequest request)
        {
            try
            {
                // String capturada do claim feita no usuario contoller que tem os dados da requisicao.
                string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioClaims);
                // Esse autenticaar converte o objeto serializado em um Json podendo usar o nome e id do usuario.

                var response = _servicePlaylist.AdicionarPlayList(request, usuarioResponse.Id);

                return await ResponseAsync(response, _servicePlaylist);

            }
            catch (Exception ex)
            {

                return ResponseException(ex);
            }
        }

        [HttpDelete]
        [Route("api/v1/PlayList/Excluir/{id:Guid}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            try
            {
                var response = _servicePlaylist.ExcluirPlayList(id);

                return await ResponseAsync(response, _servicePlaylist);
            }
            catch (Exception ex)
            {

                return ResponseException(ex);
            }
        }

    }
}
