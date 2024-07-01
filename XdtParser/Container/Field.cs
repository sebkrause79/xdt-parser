using XdtParser.Enums;
using XdtParser.Helper;
using XdtParser.Interface;

namespace XdtParser.Container;

internal class Field : IContainer
{
    private readonly FieldDescription _description;
    private List<string> _content = null!;
    private List<IRule> _rules;
    private Presence? _presence;
    private Multiplicity _multiplicity;
    private string _fieldIdentifier => _description.Id;

    public IContainer Parent { get; set; }
    public List<IContainer> Children { get; set; }

    public string Content => string.Join("\r\n", _content ?? new List<string>());

    public string Index => _fieldIdentifier;
    public bool HasContent => _content is { Count: > 0 };

    public Field(FieldDescription description, IContainer parent, List<IContainer>? childs = null, List<IRule>? rules = null, Presence? presence = null,
        Multiplicity multiplicity = Multiplicity.Single)
    {
        _description = description;
        _presence = presence;
        _rules = rules ?? new();
        _multiplicity = multiplicity;
        Children = childs ?? new();
        Parent = parent;
    }

    public bool IsValid()
    {
        var parentField = this.GetFieldAbove();
        if (_presence == Presence.M)
        {
            if (!HasContent)
            {
                return false;
            }
        }

        if (_presence == Presence.m)
        {
            if (!HasContent && parentField.HasContent)
            {
                return false;
            }
        }

        if (_presence == Presence.k)
        {
            if (HasContent && !parentField.HasContent)
            {
                return false;
            }
        }

        if (_rules.Any(r => !r.IsValid(Content, this)))
        {
            return false;
        }

        return Children.TrueForAll(child => child.IsValid());
    }

    public void TakeLines(List<XdtLine> lines)
    {
        var line = lines.First();
        if (line.FieldIdentifier == _fieldIdentifier)
        {
            _content ??= new();
            _content.Add(line.GetPayload());
            lines.RemoveAt(0);
        }

        foreach (var child in Children)
        {
            child.TakeLines(lines);
        }
    }
}