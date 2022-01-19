using AutoMapper;
using BLL.Entities;
using DAL.Entities;

namespace BLL.Mappers
{
    public static class BookDtoToBookMapper
    {
        public static Book Map(BookDTO bookDTO)
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<BookDTO, Book>()
               );
            var mapper = new Mapper(config);
            var book = mapper.Map<Book>(bookDTO);
            return book;
        }
    }
}
