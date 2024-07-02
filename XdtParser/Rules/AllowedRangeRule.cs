using XdtParser.Interface;

namespace XdtParser.Rules;

internal class AllowedRangeRule : IRule
{
    private IEnumerable<(int start, int end)> _ranges;

    internal AllowedRangeRule(IEnumerable<(int start, int end)> ranges)
    {
        _ranges = ranges;
    }
    public bool IsValid(string content, IContainer? context = null)
    {
        if (!int.TryParse(content, out var value))
        {
            return false;
        }

        return _ranges.Any(r => r.start <= value && value <= r.end);
    }
}