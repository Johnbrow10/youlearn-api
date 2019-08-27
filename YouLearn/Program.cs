using System;
using YouLearn.Domain.Argumentos.Usuario;
using YouLearn.Domain.Servicos;

namespace YouLearn
{
    class Program
    {
        static void Main(string[] args)
        {

            AdicionarUsuarioRequest request = new AdicionarUsuarioRequest()
            {
                Email = "Pessoa@gmail.com",
                PrimeiroNome = "Fernando",
                UltimoNome = "Pessoa",
                Senha = "1234"
            };

            var response = new ServicosUsuarios().AdicionarUsuario(request);

             Console.ReadKey();

        }
    }
}
