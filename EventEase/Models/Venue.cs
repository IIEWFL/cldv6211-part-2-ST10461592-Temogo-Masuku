using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventEase.Models;

public partial class Venue
{
    public int VenueId { get; set; }

    [Required(ErrorMessage = "Venue Name is required")]
    public string VenueName { get; set; } = null!;

    [Required(ErrorMessage = "location is required")]
    public string Location { get; set; } = null!;

    [Required(ErrorMessage = "Capacity is required")]
    public string Capacity { get; set; } = null!;

    [Required(ErrorMessage = "ImageURL is required")]
    public string ImageUrl { get; set; } = null!;
   

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
