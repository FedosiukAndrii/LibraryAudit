using BLL.Entities;
using BLL.Interfaces;
using BLL.Mappers;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork repositories;
        public ClientService(IUnitOfWork unit)
        {
            repositories = unit;
        }

        public async Task<IEnumerable<ClientDTO>> GetAllAsync()
        {
            return ClientToClientDtoMapper.Map(await repositories.Clients.GetAllAsync());
        }

        public async Task<ClientDTO> GetByIdAsync(int? id)
        {
            if (id < 0 || id is null)
                throw new ArgumentNullException("Incorrect Id");
            var book = ClientToClientDtoMapper.Map(await repositories.Clients.GetByIdAsync((int)id));
            if (book is null)
                throw new Exception("Book is not found");
            return book;
        }

        public async Task<bool> CreateAsync(ClientDTO clientDTO)
        {
            var client = ClientDtoToClientMapper.Map(clientDTO);
            return await repositories.Clients.CreateAsync(client);
        }

        public async Task UpdateAsync(ClientDTO clientDTO)
        {
            var clientToUpdate = (await repositories.Clients.GetAllAsync()).FirstOrDefault(client => client.Id == clientDTO.Id);
            clientToUpdate.FirstName = clientDTO.FirstName;
            clientToUpdate.LastName = clientDTO.LastName;
            clientToUpdate.Email = clientDTO.Email;
            await repositories.Clients.UpdateAsync(clientToUpdate);
        }

        public async Task DeleteAsync(int id)
        {
            await repositories.Clients.DeleteAsync(id);
        }

        public async Task<ClientDTO> GetByEmail(string email)
        {
            return ClientToClientDtoMapper.Map((await repositories.Clients.GetAllAsync()).FirstOrDefault(client => client.Email == email));
        }
    }
}
