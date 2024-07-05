using XdtParser.Enums;
using XdtParser.Interface;

namespace XdtParser.Rules;

internal abstract class BaseRule : IRule
{
    protected RuleCategory _category;
    protected RuleErrorState _errorState;

    protected BaseRule(RuleCategory category, RuleErrorState errorState)
    {
        _category = category;
        _errorState = errorState;
    }

    public abstract bool IsValid(string content, IXdtElement? context);
}