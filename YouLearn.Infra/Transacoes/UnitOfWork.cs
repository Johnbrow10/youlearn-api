using YouLearn.Infra.Persistencia.EF;

namespace YouLearn.Infra.Transacoes
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly YouLearnContext _context;

        public UnitOfWork(YouLearnContext context)
        {
            _context = context;
        }

        // Classe que permite salva da api, depois de todas as verificacoes.
        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
