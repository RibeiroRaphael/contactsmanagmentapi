using Contactsmanagment.Models;
using Contactsmanagment.Models.Dtos;
using Contactsmanagment.Services;
using Microsoft.AspNetCore.Mvc;

namespace Contactsmanagment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _service;

        public ContactsController(IContactService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {
            var result = await _service.Create(contact);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var contact = await _service.GetById(id);
            if (contact == null) return NotFound();
            return Ok(contact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateContactDto contact)
        {
            await _service.Update(id, contact);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}