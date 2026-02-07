using System;
using System.Collections.Generic;

#region Order

// SRP: класс отвечает только за данные и логику заказа
class Order
{
    private List<OrderItem> items = new List<OrderItem>();

    public IPayment Payment { get; set; }
    public IDelivery Delivery { get; set; }

    public void AddItem(string name, int quantity, double price)
    {
        items.Add(new OrderItem(name, quantity, price));
    }

    public double GetTotalPrice()
    {
        double total = 0;

        foreach (var item in items)
        {
            total += item.GetTotalPrice();
        }

        return total;
    }
}

class OrderItem
{
    public string Name { get; }
    public int Quantity { get; }
    public double Price { get; }

    public OrderItem(string name, int quantity, double price)
    {
        Name = name;
        Quantity = quantity;
        Price = price;
    }

    public double GetTotalPrice()
    {
        return Quantity * Price;
    }
}

#endregion

#region Payment

// DIP + ISP
interface IPayment
{
    void ProcessPayment(double amount);
}

// LSP: любой способ оплаты можно подставить вместо IPayment
class CreditCardPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine("Оплата картой: " + amount);
    }
}

class PayPalPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine("Оплата PayPal: " + amount);
    }
}

class BankTransferPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine("Банковский перевод: " + amount);
    }
}

#endregion

#region Delivery

// DIP + ISP
interface IDelivery
{
    void DeliverOrder(Order order);
}

class CourierDelivery : IDelivery
{
    public void DeliverOrder(Order order)
    {
        Console.WriteLine("Доставка курьером");
    }
}

class PostDelivery : IDelivery
{
    public void DeliverOrder(Order order)
    {
        Console.WriteLine("Доставка почтой");
    }
}

class PickUpPointDelivery : IDelivery
{
    public void DeliverOrder(Order order)
    {
        Console.WriteLine("Самовывоз из пункта");
    }
}

#endregion

#region Notification

// ISP
interface INotification
{
    void SendNotification(string message);
}

class EmailNotification : INotification
{
    public void SendNotification(string message)
    {
        Console.WriteLine("Email: " + message);
    }
}

class SmsNotification : INotification
{
    public void SendNotification(string message)
    {
        Console.WriteLine("SMS: " + message);
    }
}

#endregion

#region Discount

// OCP: новые скидки добавляются через новые классы
interface IDiscountRule
{
    double Apply(double total);
}

class PercentageDiscount : IDiscountRule
{
    public double Apply(double total)
    {
        return total * 0.9; // 10% скидка
    }
}

class FixedDiscount : IDiscountRule
{
    public double Apply(double total)
    {
        return total - 500;
    }
}

// SRP: только расчет скидок
class DiscountCalculator
{
    private List<IDiscountRule> rules = new List<IDiscountRule>();

    public void AddRule(IDiscountRule rule)
    {
        rules.Add(rule);
    }

    public double Calculate(double total)
    {
        double result = total;

        foreach (var rule in rules)
        {
            result = rule.Apply(result);
        }

        return result;
    }
}

#endregion

#region Service

// DIP: зависит только от интерфейсов
class OrderService
{
    private readonly INotification notification;
    private readonly DiscountCalculator discountCalculator;

    public OrderService(INotification notification, DiscountCalculator discountCalculator)
    {
        this.notification = notification;
        this.discountCalculator = discountCalculator;
    }

    public void ProcessOrder(Order order)
    {
        double total = order.GetTotalPrice();
        double finalPrice = discountCalculator.Calculate(total);

        order.Payment.ProcessPayment(finalPrice);
        order.Delivery.DeliverOrder(order);

        notification.SendNotification("Заказ оформлен. Итоговая цена: " + finalPrice);
    }
}


class Program
{
    static void Main()
    {
        Order order = new Order();

        order.AddItem("Ноутбук", 1, 300000);
        order.AddItem("Мышь", 1, 5000);

        order.Payment = new CreditCardPayment();
        order.Delivery = new CourierDelivery();

        DiscountCalculator discountCalculator = new DiscountCalculator();
        discountCalculator.AddRule(new PercentageDiscount());

        INotification notification = new EmailNotification();

        OrderService service = new OrderService(notification, discountCalculator);
        service.ProcessOrder(order);
    }
}

