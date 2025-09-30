using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using TravelAgency.Models;
using TravelAgency.Services;

namespace TravelAgency.Pages.Customers
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly CustomerService _customerService;

        public IndexModel(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public List<Customer> Customers { get; set; }

        public async Task OnGetAsync()
        {
            Customers = await _customerService.GetAllAsync();
        }
    }
}