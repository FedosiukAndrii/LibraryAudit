using BLL.Entities;
using BLL.Interfaces;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ArchiveService : IArchiveService
    {
        private readonly IBookService bookService;
        public ArchiveService(IBookService service)
        {
            bookService = service;
        }
        public async Task<bool> ArchiveBook(int id)
        {
            BookDTO book = await bookService.GetByIdAsync(id);
            if (book == null || book.IsArchived)
                return false;
            book.IsArchived = true;
            book.IsReserved = false;
            book.ClientId = null;
            await bookService.UpdateAsync(book);
            return true;
        }
    }
}
