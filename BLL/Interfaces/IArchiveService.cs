using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IArchiveService
    {
        public Task<bool> ArchiveBook(int id);
    }
}
