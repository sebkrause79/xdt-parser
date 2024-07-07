using XdtParser.ParsedContainer;
using XdtParser.RawContainer;
using XdtParser.XdtTypes.LdtTest;

namespace XdtParser.XdtTypes;

internal static class XdtDocumentFactory
{
    public static XdtParsed GetLdtDocument(List<XdtLine> lines)
    {
        var first = lines.FirstOrDefault()?.GetPayload();
        var version = lines.FirstOrDefault(l => l.FieldIdentifier == "0001")?.GetPayload() ?? string.Empty;
        if (version.ToLower().Contains("ldt2"))
        {
            throw new NotImplementedException("LDT2 not implemented");
        }

        return first switch
        {
            "8230" => new LdtDocumentSenderToLaboratory(lines),
            "8220" => new LdtDocumentLaboratoryToSender(lines),
            _ => throw new ArgumentException($"Unknown LDT document type starting with sentence type '{first}'!")
        };
    }

    public static XdtParsed GetBdtDocument(List<XdtLine> lines)
    {
        throw new NotImplementedException();
    }

    public static XdtParsed GetGdtDocument(List<XdtLine> lines)
    {
        throw new NotImplementedException();
    }
}