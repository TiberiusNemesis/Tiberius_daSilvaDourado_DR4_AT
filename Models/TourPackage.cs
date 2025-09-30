using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Models
{
    public class TourPackage
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        [Range(1, 100)]
        public int MaxCapacity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        public List<Destination> Destinations { get; set; } = new List<Destination>();
        public List<Booking> Bookings { get; set; } = new List<Booking>();

        public event Action<string> CapacityReached;

        public void OnCapacityReached(string message)
        {
            CapacityReached?.Invoke(message);
        }
    }
}