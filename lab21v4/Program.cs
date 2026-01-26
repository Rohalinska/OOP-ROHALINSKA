using lab21v4.Factories;

namespace lab21v4
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Тип доставки (Standard / Express / International / Night):");
            string type = Console.ReadLine();

            Console.WriteLine("Введіть відстань (км):");
            decimal distance = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Введіть вагу (кг):");
            decimal weight = decimal.Parse(Console.ReadLine());

            var strategy = ShippingStrategyFactory.CreateStrategy(type);
            var service = new DeliveryService();

            decimal cost = service.CalculateDeliveryCost(distance, weight, strategy);

            Console.WriteLine($"Вартість доставки: {cost} грн");
        }
    }
}
