using System;

namespace lab31vN
{
    // --------- MODEL ----------
    public class Order
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public decimal Amount { get; set; }
    }

    // --------- INTERFACES ----------
    public interface IOrderRepository
    {
        void Save(Order order);
        Order GetById(int id);
    }

    public interface IEmailService
    {
        void Send(string email, string message);
    }

    // --------- SERVICE ----------
    public class OrderService
    {
        private readonly IOrderRepository _repo;
        private readonly IEmailService _email;

        public OrderService(IOrderRepository repo, IEmailService email)
        {
            _repo = repo;
            _email = email;
        }

        public void CreateOrder(Order order)
        {
            if (order == null)
                throw new ArgumentException("Order is null");

            if (order.Amount <= 0)
                throw new ArgumentException("Invalid amount");

            _repo.Save(order);
            _email.Send(order.Email, "Order created");
        }

        public Order GetOrder(int id)
        {
            return _repo.GetById(id);
        }
    }
}