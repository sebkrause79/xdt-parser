namespace LdtParser.Rule;

internal class RuleFactory{
    internal static IRule GetRule(string name)
    {
        return name[0] switch 
        {
            'E' => ValueRuleFactory.GetRule(name),
            _ => throw new ArgumentOutOfRangeException(nameof(name))
        };
    }
}