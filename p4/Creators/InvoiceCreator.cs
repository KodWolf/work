public class InvoiceCreator : DocumentCreator
{
    public override Document CreateDocument()
    {
        return new Invoice();
    }
}
