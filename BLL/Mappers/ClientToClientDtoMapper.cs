using AutoMapper;
using BLL.Entities;
using DAL.Entities;
using System.Collections.Generic;

namespace BLL.Mappers
{
    public static class ClientToClientDtoMapper
    {
        public static ClientDTO Map(Client client)
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<Client, ClientDTO>()
               );
            var mapper = new Mapper(config);
            var clientDTO = mapper.Map<ClientDTO>(client);
            return clientDTO;
        }

        public static IEnumerable<ClientDTO> Map(IEnumerable<Client> clients)
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<Client, ClientDTO>()
               );
            var mapper = new Mapper(config);
            var clientDTOs = mapper.Map<IEnumerable<ClientDTO>>(clients);
            return clientDTOs;
        }
    }
}
