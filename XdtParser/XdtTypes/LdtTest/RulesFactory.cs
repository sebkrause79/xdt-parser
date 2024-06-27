using System.Dynamic;

namespace XdtParser.XdtTypes.LdtTest;

internal static class RulesFactory
{
    internal static IRule Get(string name) => name switch
    {
        "8215" => new AllowedContentRule(new[] { "8215" }),

        _ => throw new ArgumentException($"Unknown rule identifier '{name}'")
    };
}