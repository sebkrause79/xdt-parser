namespace XdtParser.XdtTypes.LdtTest;

internal class Field : IContainer
{
    public FieldDescription _description;
    private List<string> _content;
    private List<IRule> _rules;
    private FieldPresence? _fieldPresence;
    private List<IContainer> _childs = new();

    public string FieldIdentifier => _description.Id;
    public string Content => string.Join("\r\n", _content ?? new List<string>());

    public Field(FieldDescription description, FieldPresence? presence = null, List<IRule>? rules = null)
    {
        _description = description;
        _fieldPresence = presence;
        _rules = rules ?? new();
    }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }

    public bool TakeLines(List<XdtLine> lines)
    {
        var line = lines.First();
        if (line.FieldIdentifier == FieldIdentifier)
        {
            _content ??= new();
            _content.Add(line.GetPayload());
            lines.RemoveAt(0);
            return true;
        }

        foreach(var child in _childs)
        {
            if (child.TakeLines(lines) == true)
            {
                return true;
            }
        }

        return false;
    }

    public IContainer AddChild(IContainer child)
    {
        _childs.Add(child);
        return this;
    }
}