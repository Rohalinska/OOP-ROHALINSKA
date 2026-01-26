namespace lab21v4.Strategies
{
    public interface IShippingStrategy
    {
        decimal CalculateCost(decimal distance, decimal weight);
    }
}
