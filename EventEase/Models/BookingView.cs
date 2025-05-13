using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public class BookingView
    {
        [Key]
        public int BookingViewid {  get; set; }

        [Required(ErrorMessage = "Venue Name is required")]
        public string VenueName { get; set; } = null!;

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; } = null!;

        [Required(ErrorMessage = "Capacity is required")]
        public int Capacity { get; set; } 

        public string? ImageURL { get; set; }

        [Required(ErrorMessage = "Event Name is required")]
        public string EventName { get; set; } = null!;

        [Required(ErrorMessage = "Event Date is required")]
        public string? EventDate { get; set; } = null;

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = null!;
       
        public string? BookingDate { get; set; } = null;
    }
}
