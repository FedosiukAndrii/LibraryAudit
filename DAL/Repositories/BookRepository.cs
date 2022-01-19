using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        private readonly ApplicationContex contex;

        public BookRepository(ApplicationContex applicationContex)
        {
            contex = applicationContex;
        }
        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await contex.Books.ToListAsync();
        }
        public async Task<Book> GetByIdAsync(int id)
        {
            return await contex.Books.FindAsync(id);
        }

        public async Task CreateAsync(Book book)
        {
            await contex.Books.AddAsync(book);
            await contex.SaveChangesAsync();
        }
        public async Task UpdateAsync(Book book)
        {
            contex.Entry(book).State = EntityState.Modified;
            await contex.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Book book = await contex.Books.FindAsync(id);
            if (book != null)
            {
                contex.Books.Remove(book);
                await contex.SaveChangesAsync();
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    contex.Dispose();
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
