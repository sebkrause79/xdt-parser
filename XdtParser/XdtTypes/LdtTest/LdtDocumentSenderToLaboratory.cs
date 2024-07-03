using XdtParser.Container;

namespace XdtParser.XdtTypes.LdtTest
{
    internal class LdtDocumentSenderToLaboratory : LdtDocument
    {
        public List<Sentence8215> Auftraege => ((Sentence8215[])_children.ToArray()[1..^1]).ToList();

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
