using XdtParser.Interface;
using XdtParser.RawContainer;
using XdtParser.XdtTypes;

namespace XdtParser;

public static class XdtParser
{
    public static IXdtRaw GetRaw(string xdt)
    {
        return XdtRaw.ImportXdt(xdt);
    }

    public static IXdtParsed GetLdt(IXdtRaw raw)
    {
        return XdtDocumentFactory.GetLdtDocument(raw.Lines);
    }

    public static IXdtParsed GetLdt(string xdt)
    {
        var raw = GetRaw(xdt);
        return GetLdt(raw);
    }

    public static IXdtParsed GetBdt(IXdtRaw raw)
    {
        return XdtDocumentFactory.GetBdtDocument(raw.Lines);
    }

    public static IXdtParsed GetBdt(string xdt)
    {
        var raw = GetRaw(xdt);
        return GetBdt(raw);
    }

    public static IXdtParsed GetGdt(IXdtRaw raw)
    {
        return XdtDocumentFactory.GetGdtDocument(raw.Lines);
    }

    public static IXdtParsed GetGdt(string xdt)
    {
        var raw = GetRaw(xdt);
        return GetGdt(raw);
    }
}
