using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AddressBookApp.DataAccess.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace AddressBookApp.DataAccess
{
    public class AddressBookDataContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public AddressBookDataContext(DbContextOptions<AddressBookDataContext> options) : base(options) { }

        private void SetDefaultEntityValues()
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
        }

        public override int SaveChanges()
        {
            this.SetDefaultEntityValues();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.SetDefaultEntityValues();
            return base.SaveChangesAsync();
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