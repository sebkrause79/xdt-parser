using XdtParser.Enums;
using XdtParser.Interface;

namespace XdtParser.Rules;

internal class AllowedRangeRule : BaseRule
{
    private readonly IEnumerable<(int start, int end)> _ranges;

    internal AllowedRangeRule(IEnumerable<(int start, int end)> ranges, RuleCategory category, RuleErrorState errorState = RuleErrorState.F) : base(category, errorState)
    {
        _ranges = ranges;
    }

    public override bool IsValid(string content, IXdtElement? context = null)
    {
        if (!int.TryParse(content, out var value))
        {
            return false;
        }

        return _ranges.Any(r => r.start <= value && value <= r.end);
    }
}