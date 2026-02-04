
public class Order
{
    public List<OrderItem> Items { get; } = new List<OrderItem>();
    public IPayment Payment { get; set; }
    public IDelivery Delivery { get; set; }
    public Customer Customer { get; set; }

    public void AddItem(Product product, int quantity)
    {
        Items.Add(new OrderItem(product, quantity));
    }

    public double CalculateTotal()
    {
        return Items.Sum(item => item.Product.Price * item.Quantity);
    }

    public double CalculateTotalWithDiscount(DiscountCalculator discountCalculator)
    {
        double total = CalculateTotal();
        return discountCalculator.CalculateDiscountedPrice(total);
    }

    public void ProcessOrder()
    {
        Payment.ProcessPayment(CalculateTotal());
        Delivery.DeliverOrder(this);
    }
}

public class OrderItem
{
    public Product Product { get; }
    public int Quantity { get; }

    public OrderItem(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }
}

public class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
}

public class Customer
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}

public interface IPayment
{
    void ProcessPayment(double amount);
}

public class CreditCardPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Оплата картой: {amount} руб.");
    }
}

public class PayPalPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Оплата через PayPal: {amount} руб.");
    }
}

public class BankTransferPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Банковский перевод: {amount} руб.");
    }
}

public interface IDelivery
{
    void DeliverOrder(Order order);
}

public class CourierDelivery : IDelivery
{
    public void DeliverOrder(Order order)
    {
        Console.WriteLine($"Доставка курьером для заказа клиента {order.Customer.Name}");
    }
}

public class PostDelivery : IDelivery
{
    public void DeliverOrder(Order order)
    {
        Console.WriteLine($"Доставка почтой для заказа клиента {order.Customer.Name}");
    }
}

public class PickUpPointDelivery : IDelivery
{
    public void DeliverOrder(Order order)
    {
        Console.WriteLine($"Самовывоз из пункта выдачи для заказа клиента {order.Customer.Name}");
    }
}

public interface INotification
{
    void SendNotification(string message);
}

public class EmailNotification : INotification
{
    private readonly string _email;

    public EmailNotification(string email)
    {
        _email = email;
    }

    public void SendNotification(string message)
    {
        Console.WriteLine($"Email отправлен на {_email}: {message}");
    }
}

public class SmsNotification : INotification
{
    private readonly string _phone;

    public SmsNotification(string phone)
    {
        _phone = phone;
    }

    public void SendNotification(string message)
    {
        Console.WriteLine($"SMS отправлено на {_phone}: {message}");
    }
}

public class NotificationService
{
    private readonly List<INotification> _notifications = new List<INotification>();

    public void AddNotification(INotification notification)
    {
        _notifications.Add(notification);
    }

    public void SendAll(string message)
    {
        foreach (var notification in _notifications)
        {
            notification.SendNotification(message);
        }
    }
}

public interface IDiscountStrategy
{
    double CalculateDiscount(double totalAmount);
}

public class PercentageDiscount : IDiscountStrategy
{
    private readonly double _percentage;

    public PercentageDiscount(double percentage)
    {
        _percentage = percentage;
    }

    public double CalculateDiscount(double totalAmount)
    {
        return totalAmount * (_percentage / 100);
    }
}

public class FixedAmountDiscount : IDiscountStrategy
{
    private readonly double _amount;

    public FixedAmountDiscount(double amount)
    {
        _amount = amount;
    }

    public double CalculateDiscount(double totalAmount)
    {
        return Math.Min(_amount, totalAmount);
    }
}

public class DiscountCalculator
{
    private readonly List<IDiscountStrategy> _strategies = new List<IDiscountStrategy>();

    public void AddStrategy(IDiscountStrategy strategy)
    {
        _strategies.Add(strategy);
    }

    public double CalculateDiscountedPrice(double totalAmount)
    {
        double discount = 0;
        foreach (var strategy in _strategies)
        {
            discount += strategy.CalculateDiscount(totalAmount);
        }
        return totalAmount - discount;
    }
}

class Program
{
    static void Main()
    {
        var product1 = new Product { Name = "Ноутбук", Price = 50000 };
        var product2 = new Product { Name = "Мышь", Price = 1500 };

        var customer = new Customer
        {
            Name = "Иван Иванов",
            Email = "ivan@mail.ru",
            Phone = "+79991234567"
        };

        var order = new Order
        {
            Customer = customer
        };
        order.AddItem(product1, 1);
        order.AddItem(product2, 1);

        order.Payment = new CreditCardPayment();
        order.Delivery = new CourierDelivery();

        var discountCalculator = new DiscountCalculator();
        discountCalculator.AddStrategy(new PercentageDiscount(10)); 
        discountCalculator.AddStrategy(new FixedAmountDiscount(1000)); 

        double originalTotal = order.CalculateTotal();
        double discountedTotal = order.CalculateTotalWithDiscount(discountCalculator);

        Console.WriteLine($"Итого без скидок: {originalTotal} руб.");
        Console.WriteLine($"Итого со скидками: {discountedTotal} руб.");

        var notificationService = new NotificationService();
        notificationService.AddNotification(new EmailNotification(customer.Email));
        notificationService.AddNotification(new SmsNotification(customer.Phone));

        order.ProcessOrder();

        notificationService.SendAll($"Ваш заказ на сумму {discountedTotal} руб. обработан и будет доставлен!");

        Console.WriteLine("\n--- Добавляем новый способ оплаты (OCP в действии) ---");

        order.Payment = new BankTransferPayment();
        order.ProcessOrder();
    }
}