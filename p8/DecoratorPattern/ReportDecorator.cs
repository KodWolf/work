namespace DecoratorPattern
{
    public abstract class ReportDecorator : IReport
    {
        protected IReport _report;

        public ReportDecorator(IReport report)
        {
            _report = report;
        }

        public virtual string Generate()
        {
            return _report.Generate();
        }
    }
}