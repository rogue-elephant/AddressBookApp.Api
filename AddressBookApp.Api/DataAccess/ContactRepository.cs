using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBookApp.DataAccess.DomainModels;
using AddressBookApp.DataAccess.Validation;
using Microsoft.EntityFrameworkCore;

namespace AddressBookApp.DataAccess
{
    public class ContactRepository
    {
        private readonly AddressBookDataContext _dbContext;
        private readonly ContactValidator _validator = new ContactValidator();
        public ContactRepository(AddressBookDataContext dbContext) => _dbContext = dbContext;
        public async Task<int> AddContact(Contact contact)
        {
            await _validator.ValidateAsync(contact);
            await _dbContext.Contacts.AddAsync(contact);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Contact>> GetContacts() => await _dbContext.Contacts.ToListAsync();
        public async Task<Contact> GetContactByEmail(string email) => await _dbContext.Contacts.FindAsync(email);
    }
}