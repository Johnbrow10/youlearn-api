using prmToolkit.NotificationPattern;
using YouLearn.Domain.Entidades.Base;
using YouLearn.Domain.ValueObjects;
using YouLearn.Domain.Extensao;
using System.Collections.Generic;

namespace YouLearn.Domain.Entidades
{
    public class Usuario : EntidadeBase

    {
        protected Usuario() { }

        public Usuario(Email email, string senha)
        {
            Email = email;
            Senha = senha;

            //Criptografa a Senha
            Senha = Senha.ConvertToMD5();

            AddNotifications(email);
        }

        public Usuario(Nome nome, Email email, string senha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;

            new AddNotifications<Usuario>(this).IfNullOrInvalidLength(x => x.Senha, 3, 32);

            //Criptografa a Senha
            Senha = Senha.ConvertToMD5();

            AddNotifications(nome, email);

        }

        public Nome Nome { get; private set; }
        public Email Email { get; private set; }
        public string Senha { get; private set; }

        
    }
}
