namespace XdtParser.XdtTypes.LdtTest;

internal class AllowedContentRule : IRule
{
    private IEnumerable<string> _whitelist;

    internal AllowedContentRule(IEnumerable<string> contents)
    {
        _whitelist = contents;
    }

    public bool IsValid(string content, IContainer? context)
    {
        return _whitelist.Contains(content);
    }
}