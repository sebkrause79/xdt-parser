using XdtParser.Enums;
using XdtParser.Interface;
using XdtParser.Rules;
using XdtParser.XdtTypes.LdtTest.Rules;

namespace XdtParser.XdtTypes.LdtTest.Factories;

internal static class RulesFactory
{
    internal static IRule Get(string name) => name switch
    {
        // Test rules
        "8215" => new AllowedContentRule(new[] { "8215" }, RuleCategory.KBV),
        "NoDot" => new FormatRule(@"\.", RuleCategory.Basis),

        // Allowed content rules

        // TODO allowed content rule E001 - missing content(s) (or range(s)!)
        "E001" => new AllowedContentRule("", RuleCategory.Basis),

        // Format rules

        "F001" => new FormatRule(@"\d{5}", RuleCategory.KBV),
        "F002" => new FormatRule(@"(19|20)\d\d(0\d|1[012])(0[1-9]|[12][0-9]|3[01])", RuleCategory.Basis),
        "F003" => new FormatRule(new[]
        {
            @"(19|20)\d\d(0\d|1[012])(0[1-9]|[12][0-9]|3[01])",
            @"(19|20)\d\d(0\d|1[012])00", 
            @"(19|20)\d\d0000",
            @"00000000"
        }, RuleCategory.Basis),
        "F004" => new FormatRule(new []
        {
            @"[a-ZA-Z]\d\d",
            @"[a-ZA-Z]\d\d\.\d",
            @"[a-ZA-Z]\d\d\.\d\d",
            @"[a-ZA-Z]\d\d\.\d-",
            @"[a-ZA-Z]\d\d\.-"
        }, RuleCategory.Basis),
        "F005" => new FormatRule(@"\d\d[0-6]", RuleCategory.Basis),
        "F006" => new FormatRule(@"\d\d[0-5]\d", RuleCategory.Basis),
        // TODO format rule F007 - missing pattern(s)
        "F007" => new FormatRule(".*", RuleCategory.Basis),

        // Context rules
        "K001" => new K001(RuleCategory.Basis),

        _ => throw new ArgumentException($"Unknown rule identifier '{name}'")
    };
}