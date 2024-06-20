using LdtParser.Rule;

namespace LdtParser.Parser;

internal class FieldFactory
{
    internal static Field GetField(string name)
    {
        switch (name)
        {
            case "0001": return new Field(name, FieldFormat.alnum)
                .WithRule(RuleFactory.GetRule("E001"))
                .WithMaxLength(12);
        }

        throw new ArgumentOutOfRangeException(nameof(name));
    }
}