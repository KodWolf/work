public class SmsSender : IMessageSender
{
    public void Send(string message)
    {
        Console.WriteLine("SMS sent: " + message);
    }
}
