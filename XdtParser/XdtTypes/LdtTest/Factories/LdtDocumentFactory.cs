namespace XdtParser.XdtTypes.LdtTest.Factories;

internal static class LdtDocumentFactory
{
    public static LdtDocument GetDocument(List<XdtLine> lines)
    {
        var first = lines.FirstOrDefault()?.GetPayload();
        return first switch
        {
            "8230" => new LdtDocumentSenderToLaboratory(lines),
            "8220" => new LdtDocumentLaboratoryToSender(lines),
            _ => throw new ArgumentException($"Unknown LDT document type starting with sentence type '{first}'!")
        };
    }
}