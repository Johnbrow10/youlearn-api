using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using YouLearn.Domain.Argumentos.Canal;
using YouLearn.Domain.Argumentos.Usuario;
using YouLearn.Domain.Interfaces.Serviços;
using YouLearn.Infra.Transacoes;

namespace YouLearn.Api.Controllers
{
    public class CanalController : Base.BaseController
    {
        private readonly IServiceCanal _serviceCanal;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CanalController(IHttpContextAccessor httpContextAccessor, IUnitOfWork unit, IServiceCanal serviceCanal) :base(unit)
        {
            _serviceCanal = serviceCanal;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("api/v1/Canal/Listar")] 

        public async Task<IActionResult> Listar()
        {

            try
            {
                // String capturada do claim feita no usuario contoller que tem os dados da requisicao.
                string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioClaims);
                // Esse autenticaar converte o objeto serializado em um Json podendo usar o nome e id do usuario.

                var response = _serviceCanal.Listar(usuarioResponse.Id);

                return await ResponseAsync(response, _serviceCanal);
            }

            catch (Exception ex)
            {
                return ResponseException(ex);
                
            }

        }

        [Authorize("Bearer")]
        [HttpPost]
        [Route("api/v1/Canal/Adicionar")]
        public async Task<IActionResult> Adicionar([FromBody]AdicionarCanalRequest request)
        {
            try
            {
                // String capturada do claim feita no usuario contoller que tem os dados da requisicao.
                string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioClaims);
                // Esse autenticaar converte o objeto serializado em um Json podendo usar o nome e id do usuario.

                var response = _serviceCanal.AdicionarCanal(request, usuarioResponse.Id);

                return await ResponseAsync(response, _serviceCanal);

            }
            catch (Exception ex)
            {

                return ResponseException(ex);
            }
        }

        [Authorize("Bearer")]
        [HttpDelete]
        [Route("api/v1/Canal/Excluir/{idCanal:Guid}")]
        public async Task<IActionResult> Excluir(Guid idCanal)
        {
            try
            {
                var response = _serviceCanal.ExcluirCanal(idCanal);

                return await ResponseAsync(response, _serviceCanal);
            }
            catch (Exception ex)
            {

                return ResponseException(ex);
            }
        }


    }
}
