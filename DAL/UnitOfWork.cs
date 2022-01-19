using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationContex db; // = new ApplicationContex();
        private BookRepository bookRepository;
        private ClientRepository clientRepository;
        public UnitOfWork(DbContextOptions options)
        {
            db = new ApplicationContex(options);
        }

        public BookRepository Books
        {
            get
            {
                if (bookRepository == null)
                    bookRepository = new BookRepository(db);
                return bookRepository;
            }
        }

        public ClientRepository Clients
        {
            get
            {
                if (clientRepository == null)
                    clientRepository = new ClientRepository(db);
                return clientRepository;
            }
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
