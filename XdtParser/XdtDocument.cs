using XdtParser.XdtTypes.LdtTest;

namespace XdtParser;

public class XdtDocument
{
    private readonly List<XdtLine> _lines = new();

    private XdtDocument(List<XdtLine> lines)
    {
        _lines = lines;
    }

    public static XdtDocument FromXdt(string xdt)
    {
        var lines = xdt.Split("\r\n")
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => new XdtLine(s))
            .Aggregate(new List<XdtLine>(), KumulateLines);

        return new XdtDocument(lines);
    }

    public string this[string fi] => 
        string.Join(
            "\r\n", 
            _lines.First(l => l.FieldIdentifier == fi)?.Payload 
            ?? throw new ArgumentOutOfRangeException($"Document does not contain field identifier '{fi}'"));

    public string GetXdt() => string.Join("", _lines.Select(l => l.GetXdt()));

    public LdtDocument AsLdt()
    {
        return LdtDocumentFactory.GetDocument(_lines);
    }

    private static List<XdtLine> KumulateLines(List<XdtLine> list, XdtLine next)
    {
        var prev = list.LastOrDefault();
        if (prev is null || !prev.MergeLine(next))
        {
            list.Add(next);
        }

        return list;
    }
}