using XdtParser.Container;

namespace XdtParser.XdtTypes.LdtTest;

internal static class SentenceFactory
{
    public static Sentence GetSentence(List<XdtLine> sentenceLines)
    {
        var type = sentenceLines.FirstOrDefault()?.GetPayload() ?? throw new ArgumentException("Empty sentence");
        return type switch
        {
            "8205" => new Sentence8205(),
            "8215" => new Sentence8215(),
            "8220" => new Sentence8220(),
            "8221" => new Sentence8221(),
            "8230" => new Sentence8230(),
            "8231" => new Sentence8231(),
            _ => throw new ArgumentException($"Unknown sentence identifier '{type}'")
        };
    }
}