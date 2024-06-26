namespace XdtParser.XdtTypes.LdtTest;

internal class Field : IContainer
{
    public FieldDescription _description;
    private List<string> _content;
    private List<IRule> _rules;

    public string FieldIdentifier => _description.Id;
    public string Content => string.Join("\r\n", _content ?? new List<string>());

    public Field(FieldDescription description, List<string> content, List<IRule> rules)
    {
        _description = description;
        _content = content;
        _rules = rules;
    }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }
}