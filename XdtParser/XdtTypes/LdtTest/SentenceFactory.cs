using XdtParser.Interface;

namespace XdtParser.XdtTypes.LdtTest;

internal static class SentenceFactory
{
    public static IContainer GetSentence(List<XdtLine> sentenceLines, IContainer parent)
    {
        var type = sentenceLines.FirstOrDefault()?.GetPayload() ?? throw new ArgumentException("Empty sentence");
        return type switch
        {
            "8205" => new Sentence8205(parent),
            "8215" => new Sentence8215(parent),
            "8220" => new Sentence8220(parent),
            "8221" => new Sentence8221(parent),
            "8230" => new Sentence8230(parent),
            "8231" => new Sentence8231(parent),
            _ => throw new ArgumentException($"Unknown sentence identifier '{type}'")
        };
    }
}