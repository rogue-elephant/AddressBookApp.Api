using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBookApp.Api.DataAccess.DomainModels;
using AddressBookApp.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AddressBookApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private static readonly Contact[] Contacts = new[]
        {
            new Contact
            {
                FirstName = "John",
                Surname = "Smith",
                DateOfBirth = DateTime.Parse("1/1/1975"),
                Email = "foo@bar.com"
            }
        };

        private readonly ILogger<ContactsController> _logger;
        private readonly AddressBookDataContext _dbContext;

        public ContactsController(ILogger<ContactsController> logger, AddressBookDataContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            return Contacts;
        }

        [HttpGet()]
        [Route("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByEmail(string email)
        {
            var foundContact = _dbContext.Contacts.Find(email);

            return Ok(foundContact);
        }

        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
            _dbContext.Contacts.Add(contact);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByEmail), new { email = contact.Email }, contact);
        }
    }
}
