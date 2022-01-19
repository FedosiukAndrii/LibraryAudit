using BLL.Entities;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        readonly IClientService _clientService;

        public ClientsController(IClientService service)
        {
            _clientService = service;
        }

        [HttpGet]
        public async Task<IEnumerable<ClientDTO>> Get()
        {
            return await _clientService.GetAllAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDTO>> Get(int id)
        {
            ClientDTO client = await _clientService.GetByIdAsync(id);
            if (client == null)
                return NotFound();
            else
                return client;
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<ClientDTO>> Get(string email)
        {
            ClientDTO client = await _clientService.GetByEmail(email);
            if (client == null)
                return NotFound();
            else
                return client;
        }

        [HttpPost]
        public async Task<ActionResult<ClientDTO>> Post(ClientDTO client)
        {
            if (client == null)
                return BadRequest();
            await _clientService.CreateAsync(client);
            return Ok(client);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClientDTO>> Put(ClientDTO client)
        {
            if (client == null)
                return BadRequest();
            if (!(await _clientService.GetAllAsync()).Any(c => c.Id == client.Id))
                return NotFound();
            await _clientService.UpdateAsync(client);
            return Ok(client);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _clientService.DeleteAsync(id);
            return Ok();
        }
    }
}
