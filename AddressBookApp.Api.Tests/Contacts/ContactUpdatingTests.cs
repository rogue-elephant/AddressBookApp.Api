using System;
using Xunit;
using Models = AddressBookApp.DataAccess.DomainModels;

namespace AddressBookApp.Tests.Contact
{
    /// <summary>
    /// Tests focused on logic around updating a Contact entity within the data context.
    /// </summary>
    public class ContactUpdatingTests : DataAccessTestBase
    {
        [Fact]
        public void Fact_UpdatingContactShouldUpdateUpdatedTimestamp ()
        {
            // Arrange
            var contact = new Models.Contact("John", "Smith", DateTime.Parse("01/01/1985"), "john.smith@whateverhappenedtogeocities.com");
            dbContext.Contacts.Add(contact);
            dbContext.SaveChanges();
            var initialUpdateTimeStamp = contact.UpdatedUtc;

            // Act
            contact.Surname = "Doe";
            dbContext.Contacts.Update(contact);
            dbContext.SaveChanges();

            // Assert
            Assert.True(contact.UpdatedUtc > initialUpdateTimeStamp);
        }
    }
}
