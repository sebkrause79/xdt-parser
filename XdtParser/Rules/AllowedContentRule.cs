using XdtParser.Interface;

namespace XdtParser.Rules;

internal class AllowedContentRule : IRule
{
    private readonly IEnumerable<string> _whitelist;

    internal AllowedContentRule(IEnumerable<string> contents)
    {
        _whitelist = contents;
    }

    internal AllowedContentRule(string content)
    {
        _whitelist = new[] { content };
    }

    public bool IsValid(string content, IXdtElement? context)
    {
        return _whitelist.Contains(content);
    }
}