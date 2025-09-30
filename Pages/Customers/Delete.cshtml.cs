using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using TravelAgency.Models;
using TravelAgency.Services;

namespace TravelAgency.Pages.Customers
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly CustomerService _customerService;

        public DeleteModel(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (Customer?.Id > 0)
            {
                await _customerService.DeleteAsync(Customer.Id);
            }

            return RedirectToPage("Index");
        }
    }
}