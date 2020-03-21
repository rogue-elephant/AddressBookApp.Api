using System;
using AddressBookApp.Api.Models.DomainModels;
using Xunit;

namespace AddressBookApp.DataAccess.Tests
{
    public class ContactTests : DataAccessTestBase
    {
        #region THEORY
        #endregion
        #region FACT
        [Fact]
        public void Fact_CreatingContactShouldGenerateTimestamps ()
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
        public void Fact_UpdatingContactShouldUpdateUpdatedTimestamp ()
        {
            // Arrange
            var contact = new Contact("John", "Smith", DateTime.Parse("01/01/1985"), "john.smith@whateverhappenedtogeocities.com");
            dbContext.Contact.Add(contact);
            dbContext.SaveChanges();
            var initialUpdateTimeStamp = contact.UpdatedUtc;

            // Act
            contact.Surname = "Doe";
            dbContext.Contact.Update(contact);
            dbContext.SaveChanges();

            // Assert
            Assert.True(contact.UpdatedUtc > initialUpdateTimeStamp);
        }
        #endregion
    }
}
