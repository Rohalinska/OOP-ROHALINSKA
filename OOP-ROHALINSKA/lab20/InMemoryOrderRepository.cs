using System.Collections.Generic;

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly Dictionary<int, Order> _orders = new();

    public void Save(Order order)
    {
        _orders[order.Id] = order;
        Console.WriteLine($"Order {order.Id} saved in repository.");
    }

    public Order GetById(int id)
    {
        _orders.TryGetValue(id, out var order);
        return order;
    }
}
