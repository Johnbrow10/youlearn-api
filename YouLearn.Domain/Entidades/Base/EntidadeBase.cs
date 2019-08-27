using prmToolkit.NotificationPattern;
using System;

namespace YouLearn.Domain.Entidades.Base
{
    //IDENTIFICADOR UNICO PARA GRAVAR EM TODAS AS ENTIDADES
    public abstract class EntidadeBase : Notifiable
    {
        public EntidadeBase()
        {
            Id = Guid.NewGuid();
        }
        public virtual Guid Id { get; set; }
    }
}
