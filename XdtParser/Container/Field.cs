using XdtParser.Enums;
using XdtParser.Helper;
using XdtParser.Interface;
using XdtParser.Rules;

namespace XdtParser.Container;

internal sealed class Field : BaseXdtElement
{
    private readonly FieldDescription _description;
    private List<string> _content = null!;
    private readonly List<IRule> _rules;
    private readonly Presence? _presence;
    private string _fieldIdentifier => _description.Id;

    public Multiplicity Multiplicity { get; }

    public string Content => string.Join("\r\n", _content ?? new List<string>());

    public bool HasContent => _content is { Count: > 0 };

    public Field(FieldDescription description, IXdtElement? parent = null, List<IRule>? rules = null, Presence? presence = null,
        bool multiple = false) : base(description.Id)
    {
        _description = description;
        _presence = presence;
        _rules = rules ?? new();
        Multiplicity = multiple ? Multiplicity.Multiple : Multiplicity.Single;
        Parent = parent;
    }

    public override bool IsValid()
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

        return Children.IsValid();
    }

    public override bool TakeLine(XdtLine line)
    {
        if (ContainerState == ContainerState.Finished)
        {
            return false;
        }

        ContainerState = ContainerState.Open;

        if (line.FieldIdentifier == _fieldIdentifier)
        {
            _content ??= new();
            _content.Add(line.GetPayload());
            return true;
        }

        if (Children.TakeLine(line))
        {
            return true;
        }

        ContainerState = ContainerState.Finished;
        return false;
    }

    public override IXdtElement GetClearedCopy()
    {
        var result = new Field(_description, Parent, _rules, _presence, Multiplicity == Multiplicity.Multiple)
        {
            Children = Children.GetClearedCopy()
        };
        return result;
    }

    public override string GetTreeView(int indent, string indentUnit)
    {
        return indentUnit.Repeat(indent) + 
               $"Field {_fieldIdentifier}: {Content.ReplaceLineEndings(" // ")}\r\n" + 
               Children.GetTreeView(indent + 1, indentUnit);
    }
}