using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBookApp.DataAccess.DomainModels;
using AddressBookApp.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AddressBookApp.Utilities;

namespace AddressBookApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ILogger<ContactsController> _logger;
        private readonly AddressBookDataContext _dbContext;
        private readonly ContactRepository _repository;

        public ContactsController(ILogger<ContactsController> logger, AddressBookDataContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _repository = new ContactRepository(_dbContext);
        }

        [HttpGet]
        public async Task<IEnumerable<Contact>> Get() => await _repository.GetContacts();

        [HttpGet()]
        [Route("{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var foundContact = await _repository.GetContactByEmail(email);
            if(foundContact == null)
                return NotFound();

            return Ok(foundContact);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Contact>>> AddContact(Contact contact)
        {

            var response = await _repository.AddContact(contact);
            if(response.Errors != null)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetByEmail), new { email = contact.Email }, contact);
        }

        [HttpPut]
        [Route("{email}")]
        public async Task<ActionResult<ApiResponse<Contact>>> UpdateContact(string email, Contact contact)
        {
            var response = await _repository.UpdateContact(email, contact);
            if(response.Errors != null)
                return BadRequest(response);

            return Ok(response.Data);
        }
    }
}
