using XdtParser.Enums;
using XdtParser.Helper;
using XdtParser.Interface;
using XdtParser.Rules;

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

    public bool IsPassed { get; private set; } = false;

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
            if (_rules.Any(r => r is ContextRule && !r.IsValid(Content, this)))
            {
                return false;
            }

            if (!HasContent && (parentField?.HasContent ?? false))
            {
                return false;
            }
        }

        if (_presence == Presence.k)
        {
            if (_rules.Any(r => r is ContextRule && !r.IsValid(Content, this)))
            {
                return false;
            }

            if (HasContent && !(parentField?.HasContent ?? false))
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

    public bool TakeLine(XdtLine line)
    {
        if (IsPassed)
        {
            return false;
        }

        if (line.FieldIdentifier == _fieldIdentifier)
        {
            _content ??= new();
            _content.Add(line.GetPayload());
            return true;
        }

        foreach (var child in Children)
        {
            if (child.TakeLine(line))
            {
                return true;
            }
        }

        IsPassed = true;
        return false;
    }
}