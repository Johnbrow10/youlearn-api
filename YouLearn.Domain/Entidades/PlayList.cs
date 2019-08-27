using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using YouLearn.Domain.Entidades.Base;
using YouLearn.Domain.Enum;
using YouLearn.Domain.Resources;

namespace YouLearn.Domain.Entidades
{
    public class PlayList : EntidadeBase
    {
        protected PlayList() { }

        public PlayList(string nome, Usuario usuario)
        {
            Nome = nome;
            Usuario = usuario;

            new AddNotifications<PlayList>(this).IfNullOrInvalidLength(x =>
            x.Nome, 2, 100, MSG.X0_OBRIGATORIA_E_DEVE_CONTER_ENTRE_X1_E_X2_CARACTERES.ToFormat("2", " 100"));

            AddNotifications(usuario);
        }

        public string Nome { get; private set; }
        public Usuario Usuario { get; private set; }
        public EnumStatus Status { get; private set; }
    }
}
