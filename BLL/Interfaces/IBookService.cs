using BLL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBookService : IService<BookDTO>
    {
        public Task<List<BookDTO>> GetReservedBooks();

        public Task<List<BookDTO>> GetAvailableBooks();

        public Task<List<BookDTO>> GetAllBooksAlphOrdered();

        public Task<List<BookDTO>> GetClientsBooks(int clientId);

    }
}
