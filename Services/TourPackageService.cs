using TravelAgency.Data;
using TravelAgency.Models;
using Microsoft.EntityFrameworkCore;

namespace TravelAgency.Services
{
    public class TourPackageService
    {
        private readonly TravelAgencyContext _context;

        public TourPackageService(TravelAgencyContext context)
        {
            _context = context;
        }

        public async Task<List<TourPackage>> GetAllAsync()
        {
            return await _context.TourPackages
                .Include(p => p.Bookings)
                .Include(p => p.Destinations)
                .ToListAsync();
        }

        public async Task<TourPackage> GetByIdAsync(int id)
        {
            return await _context.TourPackages
                .Include(p => p.Bookings)
                .Include(p => p.Destinations)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<TourPackage> CreateAsync(TourPackage package)
        {
            _context.TourPackages.Add(package);
            await _context.SaveChangesAsync();
            return package;
        }

        public async Task<TourPackage> UpdateAsync(TourPackage package)
        {
            _context.TourPackages.Update(package);
            await _context.SaveChangesAsync();
            return package;
        }

        public async Task DeleteAsync(int id)
        {
            var package = await _context.TourPackages.FindAsync(id);
            if (package != null)
            {
                _context.TourPackages.Remove(package);
                await _context.SaveChangesAsync();
            }
        }
    }
}