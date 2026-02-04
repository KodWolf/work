namespace ConsolAPP17
{ 
class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }

    }
class Order
{
    public int id { get; set; }
    public decimal price { get; set; }

    public string name { get; set; }

    public int count { get; set; }

    public int paymethod { get; set; }

    public List<Product> Products { get; set; }

}

}