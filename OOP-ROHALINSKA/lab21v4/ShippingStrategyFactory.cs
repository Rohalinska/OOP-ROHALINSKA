using lab21v4.Strategies;

namespace lab21v4.Factories
{
    public static class ShippingStrategyFactory
    {
        public static IShippingStrategy CreateStrategy(string deliveryType)
        {
            return deliveryType.ToLower() switch
            {
                "standard" => new StandardShippingStrategy(),
                "express" => new ExpressShippingStrategy(),
                "international" => new InternationalShippingStrategy(),
                "night" => new NightShippingStrategy(),
                _ => throw new ArgumentException("Невідомий тип доставки")
            };
        }
    }
}
