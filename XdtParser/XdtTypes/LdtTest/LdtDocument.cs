using XdtParser.Helper;

namespace XdtParser.XdtTypes.LdtTest;

public class LdtDocument
{
    private List<IContainer> _sentences;

    internal LdtDocument(List<XdtLine> lines)
    {
        var blocks = lines.GetBlocks("8000", "8001");
        if (blocks.Any(b => !b.isInBlock))
        {
            throw new ArgumentException("Found row not containing to a sentence");
        }

        //creating models
        _sentences = blocks
            .Select(b => SentenceFactory.GetSentence(b.block))
            .ToList();

        //parsing ldt data
        foreach(var sentence in _sentences) 
        {
            sentence.TakeLines(lines);
        }
    }
}