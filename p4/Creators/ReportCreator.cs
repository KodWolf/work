public class ReportCreator : DocumentCreator
{
    public override Document CreateDocument()
    {
        return new Report();
    }
}
