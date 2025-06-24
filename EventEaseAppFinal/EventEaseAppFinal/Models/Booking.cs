namespace EventEaseAppFinal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Booking")]
    public partial class Booking
    {
        public int BookingID { get; set; }

        public int EventID { get; set; }

        public int VenueID { get; set; }

        public DateTime? BookingDate { get; set; }

        public virtual Event Event { get; set; }

        public virtual Venue Venue { get; set; }
    }
}
