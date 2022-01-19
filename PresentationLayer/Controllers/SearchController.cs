using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchBookService _service;

        public SearchController(ISearchBookService service)
        {
            _service = service;
        }

        [HttpGet("author/{author}")]
        public async Task<ActionResult> GetByAuthor(string author)
        {
            return Ok(await _service.SearchByAuthor(author));
        }

        [HttpGet("title/{title}")]
        public async Task<ActionResult> GetByTitle(string title)
        {
            return Ok(await _service.SearchByName(title));
        }
    }
}
