using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using YouLearn.Domain.Interfaces.Serviços.Base;
using YouLearn.Infra.Transacoes;

namespace YouLearn.Api.Controllers.Base
{
    public class BaseController : Controller
    {
        private readonly IUnitOfWork _unit;
        private IserviceBase _serviceBase;

        public BaseController(IUnitOfWork unitOfWork)
        {
            _unit = unitOfWork;
        }

        public async Task<IActionResult> ResponseAsync(object result, IserviceBase serviceBase)
        {
            _serviceBase = serviceBase;

            if (!serviceBase.Notifications.Any())
            {

                try
                {
                    _unit.Commit();
                    return  Ok(result);
                }

                catch (Exception ex)
                {
                    // Aqui devo logar erro
                    return BadRequest($"Houve um Problema interno com o servidor do Cliente. Entre em Contato{ex.Message}");
                }

            }

            else
            {
                // erros se a requisicao nao foi bem sucedida
                return BadRequest(new { errors = serviceBase.Notifications });

            }

        }

        public IActionResult ResponseException(Exception ex)
        {
            return BadRequest(new { errors = ex.Message, exception = ex.ToString() });
        }

        protected override void Dispose (bool disposing)
        {
            // realiza o dispose no servico para que possa ser zerada as notificações
            if (_serviceBase != null)
            {
                _serviceBase.Dispose();
            }

            base.Dispose(disposing);
               
        }
    }
}
