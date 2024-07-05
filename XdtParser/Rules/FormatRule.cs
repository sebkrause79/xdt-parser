using System.Text.RegularExpressions;
using XdtParser.Enums;
using XdtParser.Interface;

namespace XdtParser.Rules;

internal class FormatRule : BaseRule
{
    private readonly List<Regex> _regexes;

    public FormatRule(string pattern, RuleCategory category, RuleErrorState errorState = RuleErrorState.F) : base(category, errorState)
    {
        _regexes = new() { new Regex(pattern) };
    }

    public FormatRule(IEnumerable<string> patterns, RuleCategory category, RuleErrorState errorState = RuleErrorState.F) : base(category, errorState)
    {
        _regexes = patterns.Select(p => new Regex("^" + p + "$")).ToList();
    }

    public override bool IsValid(string content, IXdtElement? context)
    {
        return _regexes.Any(r => r.IsMatch(content));
    }
}