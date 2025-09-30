using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public int TourPackageId { get; set; }
        public TourPackage TourPackage { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        public event Action<string> BookingCreated;

        public void OnBookingCreated(string message)
        {
            BookingCreated?.Invoke(message);
        }
    }
}