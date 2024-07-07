using System.Runtime.CompilerServices;
using XdtParser.Helper;
using XdtParser.Interface;
using XdtParser.RawContainer;
using XdtParser.XdtTypes;
using XdtParser.XdtTypes.LdtTest.Factories;

[assembly: InternalsVisibleTo("XdtParser.Tests")]
namespace XdtParser.ParsedContainer;

public abstract class XdtParsed : IXdtParsed
{
    internal List<Sentence> _children { get; set; }

    public string Index => string.Join(" / ", _children.Select(c => c.Index));

    internal XdtParsed(List<XdtLine> lines)
    {
        var blocks = lines.GetBlocks("8000", "8001");
        if (blocks.Any(b => !b.isInBlock))
        {
            throw new ArgumentException("Found row not containing to a sentence");
        }

        _children = blocks
            .Select(b => SentenceFactory.GetSentence(b.block))
            .ToList();

        TakeLines(lines);
    }

    public virtual bool IsValid()
    {
        return _children.TrueForAll(c => c.IsValid());
    }

    internal void TakeLines(List<XdtLine> lines)
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

    internal bool TakeLine(XdtLine line)
    {
        foreach (var child in _children)
        {
            if (child.TakeLine(line))
            {
                return true;
            }
        }
        return false;
    }

    internal bool IsPassed { get; private set; }

    internal bool BreakOnParseError { get; set; } = true;
    public abstract DocumentType? DocumentType { get; }

    public string GetTreeView(string indentUnit = "  ")
    {
        return $"LDT-Document: {(IsValid() ? "ok" : "INVALID")}\r\n" +
               string.Join("", _children.Select(e => e.GetTreeView(1, indentUnit)));
    }
}