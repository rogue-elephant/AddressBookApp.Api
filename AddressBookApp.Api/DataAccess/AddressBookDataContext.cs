using System;
using System.Linq;
using AddressBookApp.Api.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace AddressBookApp.DataAccess
{
    public class AddressBookDataContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public AddressBookDataContext(DbContextOptions<AddressBookDataContext> options) : base(options) { }

        public override int SaveChanges()
        {
            foreach (var insertedOrUpdatedEntry in ChangeTracker
                .Entries()
                .Where(entry => entry.Entity is EntityBase &&
                    (
                        entry.State == EntityState.Added
                        || entry.State == EntityState.Modified
                    )
                ))
            {
                // All insert/updates will update the updated timestamp
                ((EntityBase)insertedOrUpdatedEntry.Entity).UpdatedUtc = DateTime.UtcNow;

                // Inserted timestamp will be created when an entity is added
                if (insertedOrUpdatedEntry.State == EntityState.Added)
                    ((EntityBase)insertedOrUpdatedEntry.Entity).InsertedUtc = DateTime.UtcNow;
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Contact>(contact =>
            {
                contact.ToTable("Contacts")
                .HasKey(key => key.Email);
            });
        }
    }
}