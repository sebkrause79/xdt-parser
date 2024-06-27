namespace XdtParser.XdtTypes.LdtTest;

internal interface IRule
{
    bool IsValid(string content, IContainer? context);
}