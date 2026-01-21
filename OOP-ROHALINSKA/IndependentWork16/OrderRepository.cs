namespace IndependentWork16
{
    public class OrderRepository : IOrderRepository
    {
        public void Save(string order)
        {
            Console.WriteLine("Збереження замовлення в БД...");
        }
    }
}
