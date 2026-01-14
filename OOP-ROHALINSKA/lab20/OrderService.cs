public class OrderService
{
    private readonly IOrderValidator _validator;
    private readonly IOrderRepository _repository;
    private readonly IEmailService _emailService;

    public OrderService(IOrderValidator validator, IOrderRepository repository, IEmailService emailService)
    {
        _validator = validator;
        _repository = repository;
        _emailService = emailService;
    }

    public void ProcessOrder(Order order)
    {
        Console.WriteLine($"\nProcessing order {order.Id}...");

        if (!_validator.IsValid(order))
        {
            order.Status = OrderStatus.Cancelled;
            Console.WriteLine($"Order {order.Id} is invalid and has been cancelled.");
            return;
        }

        order.Status = OrderStatus.PendingValidation;
        _repository.Save(order);
        _emailService.SendOrderConfirmation(order);

        order.Status = OrderStatus.Processed;
        Console.WriteLine($"Order {order.Id} processed successfully.");
    }
}
