using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using TravelAgency.Models;
using TravelAgency.Delegates;
using TravelAgency.Services;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Pages.TourPackages
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly TourPackageService _tourPackageService;

        public CreateModel(TourPackageService tourPackageService)
        {
            _tourPackageService = tourPackageService;
        }

        [BindProperty]
        public Models.TourPackage TourPackage { get; set; }

        public decimal DiscountedPrice { get; set; }
        public decimal TotalBookingValue { get; set; }

        public void OnGet()
        {
            TourPackage = new Models.TourPackage
            {
                StartDate = DateTime.Today.AddDays(30),
                MaxCapacity = 10,
                Price = 0
            };
            DiscountedPrice = 0;
            TotalBookingValue = 0;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                CalculateValues();
                return Page();
            }

            var calculateDiscount = new CalculateDelegate(DiscountCalculator.ApplyTenPercentDiscount);
            DiscountedPrice = calculateDiscount(TourPackage.Price);

            TotalBookingValue = BookingService.CalculateTotalValue(TourPackage.MaxCapacity, TourPackage.Price);

            await _tourPackageService.CreateAsync(TourPackage);

            return RedirectToPage("Index");
        }

        private void CalculateValues()
        {
            if (TourPackage?.Price > 0)
            {
                var calculateDiscount = new CalculateDelegate(DiscountCalculator.ApplyTenPercentDiscount);
                DiscountedPrice = calculateDiscount(TourPackage.Price);

                TotalBookingValue = BookingService.CalculateTotalValue(TourPackage.MaxCapacity, TourPackage.Price);
            }
        }
    }
}