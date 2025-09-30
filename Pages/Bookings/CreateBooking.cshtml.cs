using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using TravelAgency.Models;
using TravelAgency.Data;
using TravelAgency.Services;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Pages.Bookings
{
    [Authorize]
    public class CreateBookingModel : PageModel
    {
        private readonly TravelAgencyContext _context;

        public CreateBookingModel(TravelAgencyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BookingViewModel Booking { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
            Booking = new BookingViewModel
            {
                BookingDate = DateTime.Today
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var customer = await _context.Customers.FindAsync(Booking.CustomerId);
            if (customer == null)
            {
                ErrorMessage = "Customer not found.";
                return Page();
            }

            var package = await _context.TourPackages
                .Include(p => p.Bookings)
                .FirstOrDefaultAsync(p => p.Id == Booking.TourPackageId);

            if (package == null)
            {
                ErrorMessage = "Tour package not found.";
                return Page();
            }

            if (package.Bookings.Count >= package.MaxCapacity)
            {
                ErrorMessage = $"Maximum capacity reached for package {package.Title}.";
                return Page();
            }

            var booking = new Models.Booking
            {
                CustomerId = Booking.CustomerId,
                TourPackageId = Booking.TourPackageId,
                BookingDate = Booking.BookingDate
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            var service = new BookingService();
            var logMessage = $"Booking created - Customer: {customer.Name}, Package: {package.Title}";

            if (package.Bookings.Count + 1 >= package.MaxCapacity)
            {
                package.OnCapacityReached($"Maximum capacity reached for package {package.Title}");
            }

            return RedirectToPage("/Index");
        }
    }

    public class BookingViewModel
    {
        [Required(ErrorMessage = "Customer ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Customer ID must be greater than 0")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Tour Package ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Tour Package ID must be greater than 0")]
        public int TourPackageId { get; set; }

        [Required(ErrorMessage = "Booking Date is required")]
        public DateTime BookingDate { get; set; }
    }
}