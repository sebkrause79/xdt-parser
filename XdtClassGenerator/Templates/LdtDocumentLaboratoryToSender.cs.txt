using XdtParser.ParsedContainer;
using XdtParser.RawContainer;
using XdtParser.XdtTypes.LdtTest.Sentences;

namespace XdtParser.XdtTypes.LdtTest;

public class LdtDocumentLaboratoryToSender : XdtParsed
{
    public List<Sentence8205> Befunde => _children
            .Skip(1)
            .SkipLast(1)
            .Select(x => (Sentence8205)x)
            .ToList();

    public override DocumentType? DocumentType => XdtTypes.DocumentType.LdtTest_LaboratoryToSender;

    public LdtDocumentLaboratoryToSender(List<XdtLine> lines) : base(lines)
    {
    }

    public override bool IsValid()
    {
        var sentences = _children.Select(x => x.Index).ToArray();
        if (sentences.Length < 3)
        {
            return false;
        }

        if (sentences.First() != "8220" || sentences.Last() != "8221")
        {
            return false;
        }

        if (sentences[1..^1].Any(s => s != "8205"))
        {
            return false;
        }

        return base.IsValid();
    }
}