using AutoMapper;
using BLL.Entities;
using DAL.Entities;
using System.Collections.Generic;

namespace BLL.Mappers
{
    public static class BookToBookDtoMapper
    {
        public static BookDTO Map(Book book)
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<Book, BookDTO>()
               );
            var mapper = new Mapper(config);
            var bookDTO = mapper.Map<BookDTO>(book);
            return bookDTO;
        }
        public static IEnumerable<BookDTO> Map(IEnumerable<Book> books)
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<Book, BookDTO>()
               );
            var mapper = new Mapper(config);
            var bookDTOs = mapper.Map<IEnumerable<BookDTO>>(books);
            return bookDTOs;
        }
    }
}
