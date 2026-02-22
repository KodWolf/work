using System;

public class Report
{
    public string Header { get; set; }
    public string Content { get; set; }
    public string Footer { get; set; }

    public void Show()
    {
        Console.WriteLine(Header);
        Console.WriteLine(Content);
        Console.WriteLine(Footer);
    }
}

public interface IReportBuilder
{
    void SetHeader(string header);
    void SetContent(string content);
    void SetFooter(string footer);
    Report GetReport();
}

public class TextReportBuilder : IReportBuilder
{
    private Report _report = new Report();

    public void SetHeader(string header)
    {
        _report.Header = header;
    }

    public void SetContent(string content)
    {
        _report.Content = content;
    }

    public void SetFooter(string footer)
    {
        _report.Footer = footer;
    }

    public Report GetReport()
    {
        return _report;
    }
}

public class HtmlReportBuilder : IReportBuilder
{
    private Report _report = new Report();

    public void SetHeader(string header)
    {
        _report.Header = $"<h1>{header}</h1>";
    }

    public void SetContent(string content)
    {
        _report.Content = $"<p>{content}</p>";
    }

    public void SetFooter(string footer)
    {
        _report.Footer = $"<footer>{footer}</footer>";
    }

    public Report GetReport()
    {
        return _report;
    }
}

public class ReportDirector
{
    public void ConstructReport(IReportBuilder builder, string header, string content, string footer)
    {
        builder.SetHeader(header);
        builder.SetContent(content);
        builder.SetFooter(footer);
    }
}

class Program
{
    static void Main()
    {
        ReportDirector director = new ReportDirector();

        TextReportBuilder textBuilder = new TextReportBuilder();
        director.ConstructReport(textBuilder, "Отчет о продажах", "Продано 100 единиц", "Конец отчета");
        Report textReport = textBuilder.GetReport();
        textReport.Show();

        Console.WriteLine();

        HtmlReportBuilder htmlBuilder = new HtmlReportBuilder();
        director.ConstructReport(htmlBuilder, "Отчет о продажах", "Продано 100 единиц", "Конец отчета");
        Report htmlReport = htmlBuilder.GetReport();
        htmlReport.Show();
    }
}