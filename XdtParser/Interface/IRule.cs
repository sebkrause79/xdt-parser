namespace XdtParser.Interface;

internal interface IRule
{
    bool IsValid(string content, IXdtElement? context);
}