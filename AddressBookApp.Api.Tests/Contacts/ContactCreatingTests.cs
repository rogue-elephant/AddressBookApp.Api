using System;
using AddressBookApp.Api.DataAccess.DomainModels;
using AddressBookApp.DataAccess.Validation;
using Xunit;

namespace AddressBookApp.Tests
{
    /// <summary>
    /// Tests focused on logic around creating a Contact entity within the data context.
    /// </summary>
    public class ContactCreatingTests : DataAccessTestBase
    {        
        [Fact]
        public void Fact_CreatingContactShouldGenerateTimestamps()
        {
            // Arrange
            var contact = new Contact("John", "Smith", DateTime.Parse("01/01/1985"), "john.smith@whateverhappenedtogeocities.com");

            // Act
            dbContext.Contacts.Add(contact);
            dbContext.SaveChanges();

            // Assert
            Assert.NotNull(contact.InsertedUtc);
            Assert.NotNull(contact.UpdatedUtc);
        }

        [Fact]
        public void Fact_CreatingContactWithSameEmailShouldFail()
        {
            // Arrange
            var oldContact = new Contact("John", "Smith", DateTime.Parse("01/01/1985"), "john.smith@whateverhappenedtogeocities.com");
            dbContext.Contacts.Add(oldContact);
            int oldChanges = dbContext.SaveChanges();

            // Act
            var newContact = new Contact("Jane", "Doe", DateTime.Parse("01/01/1992"), "john.smith@whateverhappenedtogeocities.com");
            int? newChanges = null;
            var actualError = Assert.Throws<InvalidOperationException>(() =>
            {
                dbContext.Contacts.Add(newContact);
                newChanges = dbContext.SaveChanges();
            });

            // Assert
            Assert.True(oldChanges == 1);
            Assert.True(newChanges == null || newChanges.Value == 0);
            Assert.Contains("the same key value for {'Email'} is already being tracked", actualError.Message);
        }
    }
}
