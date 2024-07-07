namespace XdtParser.Interface;

public interface IRule
{
    bool IsValid(string content, IXdtElement? context);
}