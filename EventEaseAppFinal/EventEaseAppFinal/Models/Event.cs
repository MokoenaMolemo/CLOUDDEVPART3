namespace EventEaseAppFinal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Event")]
    public partial class Event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            Bookings = new HashSet<Booking>();
        }

        public int EventID { get; set; }

        [Required]
        [StringLength(255)]
        public string EventName { get; set; }

        public DateTime EventDate { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public int? VenueID { get; set; }

        public int? EventTypeID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }

        public virtual EventType EventType { get; set; }

        public virtual Venue Venue { get; set; }
    }
}
