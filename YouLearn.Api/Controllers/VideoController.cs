using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using YouLearn.Api.Controllers.Base;
using YouLearn.Domain.Argumentos.Usuario;
using YouLearn.Domain.Argumentos.Video;
using YouLearn.Domain.Interfaces.Serviços;
using YouLearn.Infra.Transacoes;

namespace YouLearn.Api.Controllers
{
    public class VideoController : BaseController
    {
        private readonly IServiceVideo _serviceVideo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VideoController(IServiceVideo serviceVideo, IUnitOfWork unit, IHttpContextAccessor httpContextAccessor) :base(unit)
        {
            _serviceVideo = serviceVideo;
            _httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/v1/Video/Listar/{tags}")]
        public async Task<IActionResult> Listar(string tags)
        {
            try
            {
                var response = _serviceVideo.Listar(tags);
                return await ResponseAsync(response, _serviceVideo);
            }
            catch (Exception ex)
            {
                return ResponseException(ex);
               
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/v1/Video/Listar/{idPlayList:Guid}")]
        public async Task<IActionResult> Listar(Guid idPlayList)
        {
            try
            {
                var response = _serviceVideo.Listar(idPlayList);
                return await ResponseAsync(response, _serviceVideo);
            }
            catch (Exception ex)
            {
                return ResponseException(ex);
               
            }
        }

        [Authorize("Bearer")]
        [HttpPost]
        [Route("api/v1/Video/Adicionar")]
        public async Task<IActionResult> Adicionar([FromBody]AdicionarVideoRequest request)
        {
            try
            {
                // String capturada do claim feita no usuario controller que tem os dados da requisicao.
                string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioClaims);
                // Esse autenticaar converte o objeto serializado em um Json podendo usar o nome e id do usuario.

                // variavel para adicionar os campos em uma response 
                var response = _serviceVideo.AdicionarVideo(request, usuarioResponse.Id);

                // entao retorna as respostas para o end point da api
                return await ResponseAsync(response, _serviceVideo);
            }
            catch (Exception ex)
            {

                return ResponseException(ex);
            }
        }
    }
}
