using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using TravelAgency.Models;
using TravelAgency.Services;

namespace TravelAgency.Pages.TourPackages
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly TourPackageService _tourPackageService;

        public DeleteModel(TourPackageService tourPackageService)
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
            if (TourPackage?.Id > 0)
            {
                await _tourPackageService.DeleteAsync(TourPackage.Id);
            }

            return RedirectToPage("Index");
        }
    }
}