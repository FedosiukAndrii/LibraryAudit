using BLL.Entities;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IClientService : IService<ClientDTO>
    {
        public Task<ClientDTO> GetByEmail(string email);
    }
}
