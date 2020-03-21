using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressBookApp.DataAccess
{
    /// <summary>
    /// Abstract class used to setup standard properties (such as inserted/update datetime)
    /// for all entities in the data context.
    /// </summary>
    public abstract class EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime InsertedUtc { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedUtc { get; set; }
    }
}