namespace IndependentWork16
{
    public class EmailService : IEmailService
    {
        public void Send(string order)
        {
            Console.WriteLine("Відправка email...");
        }
    }
}
