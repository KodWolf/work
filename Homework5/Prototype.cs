using System;
using System.Collections.Generic;

public class Product : ICloneable
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public object Clone()
    {
        return new Product
        {
            Name = this.Name,
            Price = this.Price,
            Quantity = this.Quantity
        };
    }
}

public class Discount : ICloneable
{
    public string Description { get; set; }
    public decimal Amount { get; set; }

    public object Clone()
    {
        return new Discount
        {
            Description = this.Description,
            Amount = this.Amount
        };
    }
}

public class Order : ICloneable
{
    public List<Product> Products { get; set; }
    public decimal DeliveryCost { get; set; }
    public Discount Discount { get; set; }
    public string PaymentMethod { get; set; }

    public Order()
    {
        Products = new List<Product>();
    }

    public object Clone()
    {
        Order clonedOrder = new Order();

        foreach (Product product in Products)
        {
            clonedOrder.Products.Add((Product)product.Clone());
        }

        clonedOrder.DeliveryCost = this.DeliveryCost;

        if (this.Discount != null)
        {
            clonedOrder.Discount = (Discount)this.Discount.Clone();
        }

        clonedOrder.PaymentMethod = this.PaymentMethod;

        return clonedOrder;
    }
}

class Program
{
    static void Main()
    {
        Order templateOrder = new Order();

        templateOrder.Products.Add(new Product
        {
            Name = "Ноутбук",
            Price = 50000,
            Quantity = 1
        });

        templateOrder.DeliveryCost = 300;
        templateOrder.Discount = new Discount
        {
            Description = "Скидка 10%",
            Amount = 5000
        };
        templateOrder.PaymentMethod = "Карта";

        Order clonedOrder = (Order)templateOrder.Clone();
        clonedOrder.PaymentMethod = "Наличные";

        Console.WriteLine("Оригинал - Способ оплаты: " + templateOrder.PaymentMethod);
        Console.WriteLine("Клон - Способ оплаты: " + clonedOrder.PaymentMethod);
    }
}