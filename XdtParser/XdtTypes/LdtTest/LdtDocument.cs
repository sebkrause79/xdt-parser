using XdtParser.Container;
using XdtParser.Helper;
using XdtParser.Interface;

namespace XdtParser.XdtTypes.LdtTest;

public class LdtDocument : IContainer
{
    public IContainer Parent => null!;
    public List<IContainer> Children { get; set; }

    public string Index => string.Join(" / ", Children.Select(c => c.Index));

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

    public void TakeLines(List<XdtLine> lines)
    {
        foreach (var child in Children)
        {
            child.TakeLines(lines);
        }
    }
}