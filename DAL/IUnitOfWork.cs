using DAL.Entities;
using DAL.Repositories;

namespace DAL
{
    public interface IUnitOfWork
    {
        public  IRepository<Book> Books { get; }

        public IRepository<Client> Clients { get; }
    }
}
