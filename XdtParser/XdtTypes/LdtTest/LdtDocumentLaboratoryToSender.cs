namespace XdtParser.XdtTypes.LdtTest
{
    internal class LdtDocumentLaboratoryToSender : LdtDocument
    {
        public LdtDocumentLaboratoryToSender(List<XdtLine> lines) : base(lines)
        {
        }

        public override bool IsValid()
        {
            var sentences = Children.Select(x => x.Index).ToArray();
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
}
