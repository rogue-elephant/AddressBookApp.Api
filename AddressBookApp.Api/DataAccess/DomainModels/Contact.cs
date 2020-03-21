using System;
using System.ComponentModel.DataAnnotations;
using AddressBookApp.DataAccess;

namespace AddressBookApp.Api.DataAccess.DomainModels
{
    /// <summary>
    /// Represents a Contact that can appear within an address book.
    /// </summary>
    public class Contact : EntityBase
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Key]
        public string Email { get; set; }
        

        public Contact(string firstName, string surname, DateTime dateOfBirth, string email) =>
        (this.FirstName, this.Surname, this.DateOfBirth, this.Email) =
        (firstName, surname, dateOfBirth, email);

        public Contact() {}
    }
}
