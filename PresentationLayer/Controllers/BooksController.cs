using BLL.Entities;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly IBookService _bookService;
        private readonly IArchiveService _archiveService;
        public BooksController(IBookService bookService, IArchiveService archiveService)
        {
            _bookService = bookService;
            _archiveService = archiveService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(bool sorted = false, bool available = false, bool reserved = false)
        {
            List<BookDTO> books;
            if (sorted)
                books = await _bookService.GetAvailableBooks();
            else if(available)
                books = await _bookService.GetAvailableBooks();
            else if(reserved)
                books = await _bookService.GetReservedBooks();
            else
                books = (await _bookService.GetAllAsync()).ToList();
            if (books == null)
                return NoContent();
            else
                return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> Get(int id)
        {
            BookDTO book = await _bookService.GetByIdAsync(id);
            if (book == null)
                return NotFound();
            else
                return book;
        }

        [HttpPost]
        public async Task<ActionResult<BookDTO>> Post(BookDTO book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            await _bookService.CreateAsync(book);
            return Ok(book);
        }

        [HttpPut]
        public async Task<ActionResult<BookDTO>> Put(BookDTO book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            if (!(await _bookService.GetAllAsync()).Any(b => b.Id == book.Id))
            {
                return NotFound();
            }
            await _bookService.UpdateAsync(book);
            return Ok(book);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookDTO>> Put(int id)
        {
            bool result = await _archiveService.ArchiveBook(id);
            if (!result)
                return NotFound();
            else
                return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteAsync(id);
            return Ok();
        }
    }
}
