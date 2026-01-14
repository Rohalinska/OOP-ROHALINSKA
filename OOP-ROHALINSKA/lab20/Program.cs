class Program
{
    static void Main()
    {
        // Створюємо компоненти
        IOrderValidator validator = new OrderValidator();
        IOrderRepository repository = new InMemoryOrderRepository();
        IEmailService emailService = new ConsoleEmailService();

        // Створюємо сервіс
        OrderService orderService = new OrderService(validator, repository, emailService);

        // Валідне замовлення
        Order validOrder = new Order(1, "Alice", 100m);
        orderService.ProcessOrder(validOrder);

        // Невалідне замовлення
        Order invalidOrder = new Order(2, "Bob", 0m);
        orderService.ProcessOrder(invalidOrder);

        Console.WriteLine("\nDemo finished. Press any key to exit...");
        Console.ReadKey();
    }
}
