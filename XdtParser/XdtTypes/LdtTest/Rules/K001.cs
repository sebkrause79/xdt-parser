using XdtParser.Enums;
using XdtParser.Interface;
using XdtParser.Rules;

namespace XdtParser.XdtTypes.LdtTest.Rules;

internal class K001 : ContextRule
{
    public K001(RuleCategory category, RuleErrorState errorState = RuleErrorState.F) : base(category, errorState)
    {
    }

    public override bool IsValid(string content, IXdtElement? context)
    {
        // TODO context rule K001 - missing validating logic
        throw new NotImplementedException();
    }
}