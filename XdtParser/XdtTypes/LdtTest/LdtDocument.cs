using XdtParser.Container;
using XdtParser.Helper;

namespace XdtParser.XdtTypes.LdtTest;

public class LdtDocument : IContainer
{
    public IContainer Parent => null!;
    public List<IContainer> Children { get; set; }

    internal LdtDocument(List<XdtLine> lines)
    {
        var blocks = lines.GetBlocks("8000", "8001");
        if (blocks.Any(b => !b.isInBlock))
        {
            throw new ArgumentException("Found row not containing to a sentence");
        }

        Children = blocks
            .Select(b => SentenceFactory.GetSentence(b.block, this))
            .ToList();

        TakeLines(lines);
    }

    public bool IsValid()
    {
        return Children.TrueForAll(c => c.IsValid());
    }

    public bool TakeLines(List<XdtLine> lines)
    {
        foreach (var child in Children)
        {
            if (!child.TakeLines(lines))
            {
                return false;
            }
        }
        return true;
    }
}