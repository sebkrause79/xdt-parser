using XdtParser.Interface;
using XdtParser.ParsedContainer;
using XdtParser.XdtTypes.LdtTest.Factories;

namespace XdtParser.RawContainer;

internal class XdtRaw : IXdtRaw
{
    private readonly List<XdtLine> _lines = new();

    private XdtRaw(List<XdtLine> lines)
    {
        _lines = lines;
    }

    public List<XdtLine> Lines { get { return _lines; } }

    public static IXdtRaw ImportXdt(string xdt)
    {
        var lines = xdt.Split("\r\n")
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => new XdtLine(s))
            .Aggregate(new List<XdtLine>(), KumulateLines);

        return new XdtRaw(lines);
    }

    public IEnumerable<string> this[string fi] =>
            _lines.Where(l => l.FieldIdentifier == fi)?.Select(l => l.GetPayload())
            ?? throw new ArgumentOutOfRangeException($"Document does not contain field identifier '{fi}'");

    public string ExportXdt() => string.Join("", _lines.Select(l => l.GetXdt()));

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