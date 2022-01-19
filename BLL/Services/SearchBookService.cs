using BLL.Entities;
using BLL.Interfaces;
using BLL.Mappers;
using DAL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SearchBookService : ISearchBookService
    {
        private readonly IUnitOfWork repositories;

        public SearchBookService(IUnitOfWork unit)
        {
            repositories = unit;
        }
        public async Task<List<BookDTO>> SearchByName(string name)
        {
            var books = (await repositories.Books.GetAllAsync()).Where(i => i.Title.ToLower().Contains(name.ToLower()) && !i.IsReserved);
            return BookToBookDtoMapper.Map(books).ToList();
        }
        public async Task<List<BookDTO>> SearchByAuthor(string author)
        {
            var books = (await repositories.Books.GetAllAsync()).Where(i => i.Author.ToLower().Contains(author.ToLower()) && !i.IsReserved);
            return BookToBookDtoMapper.Map(books).ToList();
        }
    }
}
