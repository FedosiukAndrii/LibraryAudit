using BLL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISearchBookService
    {
        public Task<List<BookDTO>> SearchByName(string name);

        public Task<List<BookDTO>> SearchByAuthor(string author);
    }
}
