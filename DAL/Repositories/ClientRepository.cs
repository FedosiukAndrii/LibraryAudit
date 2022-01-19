using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private readonly ApplicationContex contex;

        public ClientRepository(ApplicationContex applicationContex)
        {
            contex = applicationContex;
        }
        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await Task.Run(() => contex.Clients);
        }
        public async Task<Client> GetByIdAsync(int id)
        {
            return await contex.Clients.FindAsync(id);
        }

        public async Task CreateAsync(Client client)
        {
            await contex.Clients.AddAsync(client);
            await contex.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client client)
        {
            contex.Entry(client).State = EntityState.Modified;
            await contex.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Client client = await contex.Clients.FindAsync(id);
            if (client != null)
            {
                contex.Clients.Remove(client);
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
