using System.Runtime.CompilerServices;
using XdtParser.Helper;
using XdtParser.Interface;

[assembly:InternalsVisibleTo("XdtParser.Tests")]
namespace XdtParser.XdtTypes.LdtTest;

public abstract class LdtDocument : IContainer
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

    public virtual bool IsValid()
    {
        return Children.TrueForAll(c => c.IsValid());
    }

    public void TakeLines(List<XdtLine> lines)
    {
        foreach (var line in lines)
        {
            var success = TakeLine(line);
            if (!success && BreakOnParseError)
            {
                break;
            }
        }
    }

    public bool TakeLine(XdtLine line)
    {
        var success = false;
        foreach (var child in Children)
        {
            success |= child.TakeLine(line);
        }
        return success;
    }

    public bool IsPassed { get; private set; }

    public bool BreakOnParseError { get; set; } = true;
}