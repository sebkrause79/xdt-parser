using System.Runtime.CompilerServices;
using System.Xml.Linq;
using XdtParser.Container;
using XdtParser.Helper;
using XdtParser.Interface;
using XdtParser.XdtTypes.LdtTest.Factories;

[assembly: InternalsVisibleTo("XdtParser.Tests")]
namespace XdtParser.XdtTypes.LdtTest;

public abstract class LdtDocument : IUserCallable
{
    internal List<Sentence> _children { get; set; }

    public string Index => string.Join(" / ", _children.Select(c => c.Index));

    internal LdtDocument(List<XdtLine> lines)
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
        foreach (var child in _children)
        {
            if (child.TakeLine(line))
            {
                return true;
            }
        }
        return false;
    }

    public bool IsPassed { get; private set; }

    public bool BreakOnParseError { get; set; } = true;

    public string this[string fi]
    {
        get { throw new NotImplementedException(); }
        set { throw new NotImplementedException(); }
    }

    public string GetTreeView(string indentUnit = "  ")
    {
        return $"LDT-Document: {(IsValid() ? "ok" : "INVALID")}\r\n" +
               string.Join("", _children.Select(e => e.GetTreeView(1, indentUnit)));
    }
}