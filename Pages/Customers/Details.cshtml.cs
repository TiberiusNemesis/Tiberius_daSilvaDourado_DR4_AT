using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using TravelAgency.Models;
using TravelAgency.Services;

namespace TravelAgency.Pages.Customers
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly CustomerService _customerService;

        public DetailsModel(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Customer = await _customerService.GetByIdAsync(id);

            if (Customer == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}