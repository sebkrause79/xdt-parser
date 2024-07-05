using XdtParser.Enums;
using XdtParser.Interface;

namespace XdtParser.Rules;

internal class AllowedContentRule : BaseRule
{
    private readonly IEnumerable<string> _whitelist;

    internal AllowedContentRule(IEnumerable<string> contents, RuleCategory category, RuleErrorState errorState = RuleErrorState.F) : base(category, errorState)
    {
        _whitelist = contents;
    }

    internal AllowedContentRule(string content, RuleCategory category, RuleErrorState errorState = RuleErrorState.F) : base(category, errorState)
    {
        _whitelist = new[] { content };
    }

    public override bool IsValid(string content, IXdtElement? context)
    {
        return _whitelist.Contains(content);
    }
}