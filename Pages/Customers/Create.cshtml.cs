using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using TravelAgency.Models;
using TravelAgency.Services;

namespace TravelAgency.Pages.Customers
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly CustomerService _customerService;

        public CreateModel(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public void OnGet()
        {
            Customer = new Customer();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _customerService.CreateAsync(Customer);

            return RedirectToPage("Index");
        }
    }
}