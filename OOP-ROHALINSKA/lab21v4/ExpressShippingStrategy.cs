namespace lab21v4.Strategies
{
    public class ExpressShippingStrategy : IShippingStrategy
    {
        public decimal CalculateCost(decimal distance, decimal weight)
        {
            return distance * 2.5m + weight * 1.0m + 50;
        }
    }
}
