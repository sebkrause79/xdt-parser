using XdtParser.Enums;
using XdtParser.Interface;

namespace XdtParser.Rules;

internal abstract class ContextRule : BaseRule
{
    public ContextRule(RuleCategory category, RuleErrorState errorState) : base(category, errorState)
    {
    }

    
}