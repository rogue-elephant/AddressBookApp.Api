using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBookApp.DataAccess.DomainModels;
using AddressBookApp.DataAccess.Validation;
using AddressBookApp.Utilities;
using Microsoft.EntityFrameworkCore;

namespace AddressBookApp.DataAccess
{
    public class ContactRepository
    {
        private readonly AddressBookDataContext _dbContext;
        private readonly ContactValidator _validator = new ContactValidator();
        public ContactRepository(AddressBookDataContext dbContext) => _dbContext = dbContext;
        public async Task<ApiResponse<Contact>> AddContact(Contact contact)
        {
            var validationResult = await _validator.ValidateAsync(contact);
            if(!validationResult.IsValid)
                return new ApiResponse<Contact>(validationResult.Errors
                .Select(validationError => new ApiError(validationError.ErrorCode, validationError.ErrorMessage)).ToList());

            await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();
            return new ApiResponse<Contact>(contact);
        }

        public async Task<ApiResponse<Contact>> UpdateContact(string email, Contact contact)
        {
            var foundContact = await GetContactByEmail(email);
            if(foundContact == null)
                return null;

            foundContact.FirstName = contact.FirstName;
            foundContact.Surname = contact.Surname;
            foundContact.DateOfBirth = contact.DateOfBirth;

            var validationResult = await _validator.ValidateAsync(foundContact);
            if(!validationResult.IsValid)
                return new ApiResponse<Contact>(validationResult.Errors
                .Select(validationError => new ApiError(validationError.ErrorCode, validationError.ErrorMessage)).ToList());

            _dbContext.Contacts.Update(foundContact);
            await _dbContext.SaveChangesAsync();
            return new ApiResponse<Contact>(foundContact);
        }

        public async Task<List<Contact>> GetContacts() => await _dbContext.Contacts.ToListAsync();
        public async Task<Contact> GetContactByEmail(string email) => await _dbContext.Contacts.FirstOrDefaultAsync(contact => contact.Email == email);
    }
}