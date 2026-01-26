namespace IndependentWork16
{
    class Program
    {
        static void Main()
        {
            var validator = new OrderValidator();
            var repository = new OrderRepository();
            var emailService = new EmailService();

            var orderService = new OrderService(
                validator,
                repository,
                emailService);

            orderService.ProcessOrder("Замовлення №1");
        }
    }
}
