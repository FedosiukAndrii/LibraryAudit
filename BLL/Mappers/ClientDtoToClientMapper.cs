using AutoMapper;
using BLL.Entities;
using DAL.Entities;

namespace BLL.Mappers
{
    public static class ClientDtoToClientMapper
    {
        public static Client Map(ClientDTO clientDTO)
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<ClientDTO, Client>()
               );
            var mapper = new Mapper(config);
            var client = mapper.Map<Client>(clientDTO);
            return client;
        }
    }
}
