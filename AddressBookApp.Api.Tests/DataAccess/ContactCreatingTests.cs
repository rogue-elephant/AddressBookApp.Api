using System;
using AddressBookApp.Api.Models.DomainModels;
using Xunit;

namespace AddressBookApp.DataAccess.Tests
{
    /// <summary>
    /// Tests focused on logic around creating a Contact entity within the data context.
    /// </summary>
    public class ContactCreatingTests : DataAccessTestBase
    {
        #region THEORY
        #endregion
        #region FACT
        [Fact]
        public void Fact_CreatingContactShouldGenerateTimestamps()
        {
            // Arrange
            var contact = new Contact("John", "Smith", DateTime.Parse("01/01/1985"), "john.smith@whateverhappenedtogeocities.com");

            // Act
            dbContext.Contact.Add(contact);
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
            dbContext.Contact.Add(oldContact);
            int oldChanges = dbContext.SaveChanges();

            // Act
            var newContact = new Contact("Jane", "Doe", DateTime.Parse("01/01/1992"), "john.smith@whateverhappenedtogeocities.com");
            int? newChanges = null;
            var actualError = Assert.Throws<InvalidOperationException>(() =>
            {
                dbContext.Contact.Add(newContact);
                newChanges = dbContext.SaveChanges();
            });

            // Assert
            Assert.True(oldChanges == 1);
            Assert.True(newChanges == null || newChanges.Value == 0);
            Assert.Contains("the same key value for {'Email'} is already being tracked", actualError.Message);
        }
        #endregion
    }
}
