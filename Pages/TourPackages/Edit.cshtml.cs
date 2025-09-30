using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using TravelAgency.Models;
using TravelAgency.Services;

namespace TravelAgency.Pages.TourPackages
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly TourPackageService _tourPackageService;

        public EditModel(TourPackageService tourPackageService)
        {
            _tourPackageService = tourPackageService;
        }

        [BindProperty]
        public Models.TourPackage TourPackage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            TourPackage = await _tourPackageService.GetByIdAsync(id);

            if (TourPackage == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _tourPackageService.UpdateAsync(TourPackage);

            return RedirectToPage("Index");
        }
    }
}