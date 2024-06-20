using LdtParser.File;
using LdtParser.Rule;

namespace LdtParser.Parser;

internal class Field : IContainer
{
    private string _fieldIdentifier;
    private List<IRule> _rules = new();
    private List<string> _payloads = new();
    private IContainer? _child;
    internal FieldType FieldType { get; init; } = FieldType.Nothing;
    internal FieldFormat FieldFormat { get; init; }
    internal List<(int start, int end)> Lengths { get; init; } = new();

    public bool IsStarted => _payloads.Any();
    public bool IsFinished { get; set; } = false;

    internal Field(string fieldIdentifier, FieldFormat fieldFormat, FieldType fieldType = FieldType.Nothing, IContainer? child = null)
    {
        _fieldIdentifier = fieldIdentifier;
        _child = child;
        FieldType = fieldType;
        FieldFormat = fieldFormat;
    }

    internal Field WithLength(int length) => WithLength(length, length);

    internal Field WithLength(int start, int end)
    {
        Lengths.Add((start, end));
        return this;
    }

    internal Field WithMaxLength(int max) => WithLength(0, max);

    internal Field WithRule(IRule rule)
    {
        _rules.Add(rule);
        return this;
    }

    internal Field WithRules(IEnumerable<IRule> rules) 
    {
        _rules.AddRange(rules);
        return this;
    }

    public bool TryTake(LdtLine ldtLine)
    {
        if (ldtLine.FieldIdentifier == _fieldIdentifier)
        {
            _payloads.Add(ldtLine.Payload);
            return true;
        }
        if (_child is not null) {
            return _child.TryTake(ldtLine);
        }
        return false;
    }
}