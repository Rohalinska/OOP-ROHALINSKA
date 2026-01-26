using lab21v4.Strategies;

namespace lab21v4
{
    public class DeliveryService
    {
        public decimal CalculateDeliveryCost(
            decimal distance,
            decimal weight,
            IShippingStrategy strategy)
        {
            return strategy.CalculateCost(distance, weight);
        }
    }
}
