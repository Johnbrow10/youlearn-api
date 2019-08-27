using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouLearn.Api.Controllers.Base;
using YouLearn.Infra.Transacoes;

namespace YouLearn.Api.Controllers
{
    public class UtilController : BaseController
    {
        public UtilController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public string Get()
        {
            return "Seja Bem Vindo ao YouLearn";
        }

        [HttpGet]
        [Route("Versao")]
        public string Versao()
        {
            return "Versao 0.0.1";
        }

    }
}
