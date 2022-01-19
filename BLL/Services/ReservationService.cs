using BLL.Entities;
using BLL.Exceptions;
using BLL.Interfaces;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IBookService _bookservice;
        private readonly IClientService _clientService;

        public ReservationService(IBookService bookservice, IClientService clientService)
        {
            _bookservice = bookservice;
            _clientService = clientService;
        }

        public async Task<bool> MakeReservation(int bookId, int clientId)
        {
            BookDTO book = await _bookservice.GetByIdAsync(bookId);
            ClientDTO client = await _clientService.GetByIdAsync(clientId);
            if (book is null)
                throw new BookException($"There is no book with such id:{bookId}");
            if (client is null)
                throw new ClientException($"There is no client with such id:{clientId}");
            if (book.IsReserved || book.IsArchived)
                return false;
            book.IsReserved = true;
            book.ClientId = clientId;
            await _bookservice.UpdateAsync(book);
            return true;
        }

        public async Task<bool> CancelReservation(int bookId, int clientId)
        {

            BookDTO book = await _bookservice.GetByIdAsync(bookId);
            ClientDTO client = await _clientService.GetByIdAsync(clientId);
            if (book is null)
                throw new BookException($"There is no book with such id:{bookId}");
            if (client is null)
                throw new ClientException($"There is no client with such id:{clientId}");
            if (!book.IsReserved || book.IsArchived)
                return false;
            if (book.ClientId != clientId)
                return false;
            book.IsReserved = false;
            book.ClientId = null;
            await _bookservice.UpdateAsync(book);
            return true;
        }
    }
}
