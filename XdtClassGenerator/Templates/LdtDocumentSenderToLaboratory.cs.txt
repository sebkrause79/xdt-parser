using XdtParser.ParsedContainer;
using XdtParser.RawContainer;
using XdtParser.XdtTypes.LdtTest.Sentences;

namespace XdtParser.XdtTypes.LdtTest
{
    internal class LdtDocumentSenderToLaboratory : XdtParsed
    {
        public List<Sentence8215> Auftraege => ((Sentence8215[])_children.ToArray()[1..^1]).ToList();

        public override DocumentType? DocumentType => XdtTypes.DocumentType.LdtTest_SenderToLaboratory;

        public LdtDocumentSenderToLaboratory(List<XdtLine> lines) : base(lines)
        {
        }

        public override bool IsValid()
        {
            var sentences = _children.Select(x => x.Index).ToArray();
            if (sentences.Length < 3)
            {
                return false;
            }

            if (sentences.First() != "8230" || sentences.Last() != "8231")
            {
                return false;
            }

            if (sentences[1..^1].Any(s => s != "8215"))
            {
                return false;
            }

            return base.IsValid();
        }
    }
}
