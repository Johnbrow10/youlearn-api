using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using YouLearn.Api.Controllers.Base;
using YouLearn.Api.Seguranca;
using YouLearn.Domain.Argumentos.Usuario;
using YouLearn.Domain.Interfaces.Serviços;
using YouLearn.Infra.Transacoes;

namespace YouLearn.Api.Controllers
{
    public class UsuarioController : BaseController
    {
        private readonly IServiceUsuario _serviceUsuario;

        public UsuarioController(IUnitOfWork unit, IServiceUsuario serviceUsuario) : base(unit)
        {
            _serviceUsuario = serviceUsuario;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/usuario/Adicionar")]

        public async Task<IActionResult> Adicionar([FromBody]AdicionarUsuarioRequest request)
        {
            try
            {
                var response = _serviceUsuario.AdicionarUsuario(request);
                return await ResponseAsync(response, _serviceUsuario);
            }
            catch (Exception ex)
            {
                return ResponseException(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/v1/Usuario/Autenticar")]

        public object Auntenticar(
            [FromBody]AutenticarUsuarioRequest request,
            [FromServices]SigningConfiguracao signingConfiguracao,
            [FromServices]TokenConfiguracao tokenConfiguracao)
        {

            bool credenciaisValidas = false;
            AutenticarUsuarioResponse response = _serviceUsuario.AutenticarUsuario(request);

            credenciaisValidas = response != null;

            if (credenciaisValidas)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(response.Id.ToString(), "Id"),
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            // captura a resposta "Id e nome" da api coloca em objecto e serializa em um Json para se ler depois de pegar o Token.
                        new Claim("Usuario",JsonConvert.SerializeObject(response))
                    });

                // Controle de data de expiracao do Tken
                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(tokenConfiguracao.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfiguracao.Issuer,
                    Audience = tokenConfiguracao.Audience,
                    SigningCredentials = signingConfiguracao.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                return new
                {
                    authenticated = true,
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    message = "OK",
                    primeiroNome = response.PrimeiroNome
                };
            
            }
            // Credenciais Invalidas
            else
            {
                return new
                {
                    authenticated = false,
                    _serviceUsuario.Notifications
                };
            }

           
        }
    }
}
