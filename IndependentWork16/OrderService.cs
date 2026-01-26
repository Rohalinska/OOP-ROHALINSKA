namespace IndependentWork16
{
    public class OrderService
    {
        private readonly IOrderValidator _validator;
        private readonly IOrderRepository _repository;
        private readonly IEmailService _emailService;

        public OrderService(
            IOrderValidator validator,
            IOrderRepository repository,
            IEmailService emailService)
        {
            _validator = validator;
            _repository = repository;
            _emailService = emailService;
        }

        public void ProcessOrder(string order)
        {
            _validator.Validate(order);
            _repository.Save(order);
            _emailService.Send(order);
        }
    }
}
