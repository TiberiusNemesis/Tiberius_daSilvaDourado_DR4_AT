namespace TravelAgency.Delegates
{
    public delegate decimal CalculateDelegate(decimal originalPrice);

    public class DiscountCalculator
    {
        public static decimal ApplyTenPercentDiscount(decimal originalPrice)
        {
            return originalPrice * 0.9m;
        }
    }
}