using BLL.Exceptions;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _service;

        public ReservationController(IReservationService service)
        {
            _service = service;
        }

        [HttpPut("make/{bookId}/{clientId}")]
        public async Task<ActionResult> Reserve(int bookId, int clientId)
        {
            bool result;
            try
            {
                result = await _service.MakeReservation(bookId, clientId);
            }
            catch (BookException e)
            {
                return Problem(e.Message);
            }
            catch (ClientException e)
            {
                return Problem(e.Message);
            }

            if (!result)
                return Ok("The book is archived or already reserved");
            return Ok("The book is successfuly reserved");
        }

        [HttpPut("cancel/{bookId}/{clientId}")]
        public async Task<ActionResult> Cancel(int bookId, int clientId)
        {
            bool result;
            try
            {
                result = await _service.CancelReservation(bookId, clientId);
            }
            catch (BookException e)
            {
                return Problem(e.Message);
            }
            catch (ClientException e)
            {
                return Problem(e.Message);
            }

            if (!result)
                return Ok("The book is archived or it is not reserved by you");
            return Ok("The book reservation is successfuly canceld");
        }
    }
}
