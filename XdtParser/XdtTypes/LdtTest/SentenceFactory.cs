namespace XdtParser.XdtTypes.LdtTest;

internal static class SentenceFactory
{
    public static IContainer GetSentence(List<XdtLine> sentenceLines)
    {
        var type = sentenceLines.FirstOrDefault()?.GetPayload() ?? throw new ArgumentException("Empty sentence");
        return type switch
        {
            "8215" => new Sentence8215(),
            _ => throw new ArgumentException($"Unknown sentence identifier '{type}'")
        };
    }
}