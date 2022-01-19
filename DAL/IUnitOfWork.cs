using DAL.Repositories;

namespace DAL
{
    public interface IUnitOfWork
    {
        public BookRepository Books { get; }

        public ClientRepository Clients { get; }
    }
}
