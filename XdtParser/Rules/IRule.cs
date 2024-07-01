using XdtParser.Container;

namespace XdtParser.Rules;

internal interface IRule
{
    bool IsValid(string content, IContainer? context);
}