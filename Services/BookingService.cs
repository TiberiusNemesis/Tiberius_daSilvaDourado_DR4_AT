using TravelAgency.Models;
using TravelAgency.Delegates;

namespace TravelAgency.Services
{
    public class BookingService
    {
        private static Action<string> _multicastLogDelegate;

        static BookingService()
        {
            _multicastLogDelegate += Logger.LogToConsole;
            _multicastLogDelegate += Logger.LogToFile;
            _multicastLogDelegate += Logger.LogToMemory;
        }

        public static Func<int, decimal, decimal> CalculateTotalValue = (size, pricePerDay) =>
        {
            return size * pricePerDay;
        };

        public void CreateBooking(Booking booking, TourPackage package)
        {
            if (package.Bookings.Count >= package.MaxCapacity)
            {
                var message = $"Maximum capacity reached for package {package.Title}";
                package.OnCapacityReached(message);
                Logger.LogToConsole(message);
                return;
            }

            package.Bookings.Add(booking);

            var logMessage = $"Booking created - Customer: {booking.CustomerId}, Package: {package.Title}";
            _multicastLogDelegate(logMessage);

            if (package.Bookings.Count == package.MaxCapacity)
            {
                var capacityMessage = $"Maximum capacity reached for package {package.Title}";
                package.OnCapacityReached(capacityMessage);
            }
        }
    }
}