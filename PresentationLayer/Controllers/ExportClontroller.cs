using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        private readonly ExportService _service;

        public ExportController(ExportService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            await _service.Export("info");
            var filepath = Path.Combine("~", "info.csv");
            return File(filepath, "text/csv", "info.csv");
        }
    }
}
