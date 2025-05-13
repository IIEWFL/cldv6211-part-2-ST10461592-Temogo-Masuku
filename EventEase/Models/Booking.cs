using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventEase.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? EventId { get; set; }

    public int? VenueId { get; set; }

    [Required(ErrorMessage = "Booking Date is required")]
    public string BookingDate { get; set; } = null!;
   

    public virtual Event? Event { get; set; }

    public virtual Venue? Venue { get; set; }
}
