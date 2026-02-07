public class NotificationService
{
    private readonly IMessageSender sender;

    public NotificationService(IMessageSender sender)
    {
        this.sender = sender;
    }

    public void SendNotification(string message)
    {
        sender.Send(message);
    }
}
