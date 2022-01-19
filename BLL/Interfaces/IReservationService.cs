using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IReservationService
    {
        public Task<bool> MakeReservation(int bookId, int clientId);
        public Task<bool> CancelReservation(int bookId, int clientId);
    }
}
