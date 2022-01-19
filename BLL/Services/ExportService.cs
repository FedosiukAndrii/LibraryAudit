using BLL.Entities;
using BLL.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ExportService
    {
        readonly private IExport _export;
        private readonly IBookService _bookServise;
        public ExportService(IBookService service, IExport export)
        {
            _export = export;
            _bookServise = service;
        }
        public async Task Export(string path)
        {
            _export.Export<BookDTO>((await _bookServise.GetAllAsync()).ToList(), path);
        }
    }
}
