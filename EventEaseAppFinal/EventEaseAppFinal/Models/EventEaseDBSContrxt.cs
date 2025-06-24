using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace EventEaseAppFinal.Models
{
    public partial class EventEaseDBSContrxt : DbContext
    {
        public EventEaseDBSContrxt()
            : base("name=EventEaseDBSContext")
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<Venue> Venues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
