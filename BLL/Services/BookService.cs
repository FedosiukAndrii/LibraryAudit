using BLL.Entities;
using BLL.Interfaces;
using BLL.Mappers;
using DAL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork repositories;
        public BookService(IUnitOfWork unit)
        {
            repositories = unit;
        }

        public async Task CreateAsync(BookDTO bookDTO)
        {
            var book = BookDtoToBookMapper.Map(bookDTO);
            await repositories.Books.CreateAsync(book);
        }

        public async Task DeleteAsync(int id)
        {
            await repositories.Books.DeleteAsync(id);
        }

        public async Task<IEnumerable<BookDTO>> GetAllAsync()
        {
            return BookToBookDtoMapper.Map(await repositories.Books.GetAllAsync());
        }

        public async Task<BookDTO> GetByIdAsync(int id)
        {
            return BookToBookDtoMapper.Map(await repositories.Books.GetByIdAsync(id));
        }

        public async Task UpdateAsync(BookDTO bookDTO)
        {
            var bookToUpdate = (await repositories.Books.GetAllAsync()).FirstOrDefault(book => book.Id == bookDTO.Id);
            bookToUpdate.Title = bookDTO.Title;
            bookToUpdate.Author = bookDTO.Author;
            bookToUpdate.ClientId = bookDTO.ClientId;
            bookToUpdate.IsArchived = bookDTO.IsArchived;
            bookToUpdate.IsReserved = bookDTO.IsReserved;
            await repositories.Books.UpdateAsync(bookToUpdate);
        }

        public async Task<List<BookDTO>> GetReservedBooks()
        {
            var books = (await repositories.Books.GetAllAsync()).Where(i => i.IsReserved && !i.IsArchived);
            return BookToBookDtoMapper.Map(books).ToList();
        }

        public async Task<List<BookDTO>> GetAvailableBooks()
        {
            var books = (await repositories.Books.GetAllAsync()).Where(i => !i.IsReserved && !i.IsArchived);
            return BookToBookDtoMapper.Map(books).ToList();
        }

        public async Task<List<BookDTO>> GetAllBooksAlphOrdered()
        {
            var books = (await repositories.Books.GetAllAsync()).Where(i => !i.IsArchived).OrderBy(i => i.Title);
            return BookToBookDtoMapper.Map(books).ToList();
        }

        public async Task<List<BookDTO>> GetClientsBooks(int clientId)
        {
            var books = (await repositories.Books.GetAllAsync()).Where(i => i.ClientId == clientId && !i.IsArchived);
            return BookToBookDtoMapper.Map(books).ToList();
        }
    }
}
