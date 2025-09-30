using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Models
{
    public class Destination
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Country { get; set; }

        public List<TourPackage> TourPackages { get; set; } = new List<TourPackage>();
    }
}