namespace LdtParser.Rule;

internal class ValueRuleFactory
{
    internal static IRule GetRule(string name)
    {
        switch(name)
        {
            case "E001": return new ValueRule(name, ErrorState.Warning, "LDT3.2.16");
            case "E002": return new ValueRule(name, "1", "3", "5");
            case "E003": return new ValueRule(name).AddNumberRange(2, 999, 3);
            case "E004": return new ValueRule(name, "8220", "8221", "8230", "8231", "8205", "8215");
        }

        return null!;
    }
}