using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using TravelAgency.Models;
using TravelAgency.Services;

namespace TravelAgency.Pages.TourPackages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly TourPackageService _tourPackageService;

        public IndexModel(TourPackageService tourPackageService)
        {
            _tourPackageService = tourPackageService;
        }

        public List<Models.TourPackage> TourPackages { get; set; }

        public async Task OnGetAsync()
        {
            TourPackages = await _tourPackageService.GetAllAsync();
        }
    }
}