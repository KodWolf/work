using System;
using System.Collections.Generic;
using System.IO;


public class ReportStyle
{
    public string BackgroundColor { get; set; }
    public string FontColor { get; set; }
    public int FontSize { get; set; }

    public ReportStyle()
    {
        BackgroundColor = "White";
        FontColor = "Black";
        FontSize = 12;
    }
}

public class Report
{
    public string Header { get; set; }
    public string Content { get; set; }
    public string Footer { get; set; }
    public List<string> Sections { get; private set; }
    public ReportStyle Style { get; set; }

    public Report()
    {
        Sections = new List<string>();
        Style = new ReportStyle();
    }

    public void Export(string format)
    {
        string fileName = $"report.{format}";

        if (format == "txt")
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine(Header);
                writer.WriteLine();
                writer.WriteLine(Content);

                foreach (string section in Sections)
                {
                    writer.WriteLine();
                    writer.WriteLine(section);
                }

                writer.WriteLine();
                writer.WriteLine(Footer);
            }
            Console.WriteLine($"Сохранено в {fileName}");
        }
        else if (format == "html")
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("<html><body>");
                writer.WriteLine($"<h1>{Header}</h1>");
                writer.WriteLine($"<p>{Content}</p>");

                foreach (string section in Sections)
                {
                    writer.WriteLine($"<p>{section}</p>");
                }

                writer.WriteLine($"<footer>{Footer}</footer>");
                writer.WriteLine("</body></html>");
            }
            Console.WriteLine($"Сохранено в {fileName}");
        }
    }
}

public interface IReportBuilder
{
    void SetHeader(string header);
    void SetContent(string content);
    void SetFooter(string footer);
    void AddSection(string sectionName, string sectionContent);
    void SetStyle(ReportStyle style);
    Report GetReport();
}


public class TextReportBuilder : IReportBuilder
{
    private Report _report;

    public TextReportBuilder()
    {
        _report = new Report();
    }

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

    public void AddSection(string sectionName, string sectionContent)
    {
        _report.Sections.Add($"{sectionName}: {sectionContent}");
    }

    public void SetStyle(ReportStyle style)
    {
        _report.Style = style;
    }

    public Report GetReport()
    {
        return _report;
    }
}

public class HtmlReportBuilder : IReportBuilder
{
    private Report _report;

    public HtmlReportBuilder()
    {
        _report = new Report();
    }

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

    public void AddSection(string sectionName, string sectionContent)
    {
        _report.Sections.Add($"<h2>{sectionName}</h2><p>{sectionContent}</p>");
    }

    public void SetStyle(ReportStyle style)
    {
        _report.Style = style;
    }

    public Report GetReport()
    {
        return _report;
    }
}

public class PdfReportBuilder : IReportBuilder
{
    private Report _report;

    public PdfReportBuilder()
    {
        _report = new Report();
    }

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

    public void AddSection(string sectionName, string sectionContent)
    {
        _report.Sections.Add($"{sectionName}: {sectionContent}");
    }

    public void SetStyle(ReportStyle style)
    {
        _report.Style = style;
    }

    public Report GetReport()
    {
        return _report;
    }
}


public class ReportDirector
{
    public void ConstructReport(IReportBuilder builder, ReportStyle style)
    {
        builder.SetStyle(style);
        builder.SetHeader("Отчет");
        builder.SetContent("Основное содержание");
        builder.AddSection("Раздел 1", "Данные раздела 1");
        builder.AddSection("Раздел 2", "Данные раздела 2");
        builder.SetFooter("Конец отчета");
    }
}


class BuilderTest
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Тестирование Builder ===\n");

        ReportDirector director = new ReportDirector();
        ReportStyle style = new ReportStyle();

        Console.WriteLine("Создание текстового отчета:");
        TextReportBuilder textBuilder = new TextReportBuilder();
        director.ConstructReport(textBuilder, style);
        Report textReport = textBuilder.GetReport();
        textReport.Export("txt");

        Console.WriteLine("\nСоздание HTML отчета:");
        HtmlReportBuilder htmlBuilder = new HtmlReportBuilder();
        director.ConstructReport(htmlBuilder, style);
        Report htmlReport = htmlBuilder.GetReport();
        htmlReport.Export("html");

        Console.WriteLine("\nСоздание PDF отчета:");
        PdfReportBuilder pdfBuilder = new PdfReportBuilder();
        director.ConstructReport(pdfBuilder, style);
        Report pdfReport = pdfBuilder.GetReport();
        Console.WriteLine("PDF отчет создан (требуется библиотека iTextSharp)");

        Console.WriteLine("\nГотово");
        Console.ReadKey();
    }
}