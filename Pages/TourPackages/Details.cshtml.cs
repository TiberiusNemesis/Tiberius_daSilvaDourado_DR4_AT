using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Models;
using TravelAgency.Data;

namespace TravelAgency.Pages.TourPackages
{
    public class DetailsModel : PageModel
    {
        private readonly TravelAgencyContext _context;

        public DetailsModel(TravelAgencyContext context)
        {
            _context = context;
        }

        public Models.TourPackage TourPackage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            TourPackage = await _context.TourPackages
                .Include(p => p.Bookings)
                .Include(p => p.Destinations)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (TourPackage == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}