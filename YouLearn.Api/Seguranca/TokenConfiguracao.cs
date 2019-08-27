using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouLearn.Api.Seguranca
{
    public class TokenConfiguracao
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }

    }
}
