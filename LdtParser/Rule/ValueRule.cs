namespace LdtParser.Rule;

internal class ValueRule : IRule
{
    private List<string> _allowed = new();

    public ErrorState ErrorState {get; init; } = ErrorState.Error;

    internal ValueRule(string name, params string[] values)
    {
        Name = name;
        _allowed.AddRange(values);
    }

    internal ValueRule(string name, ErrorState errorState, params string[] values) : this(name, values)
    {
        ErrorState = errorState;
    }

    internal ValueRule AddNumberRange(int start, int end, int? digits)
    {
        _allowed.AddRange(Enumerable
            .Range(start, end + 1 - start)
            .Select(i => digits.HasValue 
                ? i.ToString($"D{digits.Value}") 
                : i.ToString()));
        return this;
    }

    internal ValueRule AddValue(string value) {
        _allowed.Add(value);
        return this;
    }

    internal ValueRule AddValues(IEnumerable<string> values)
    {
        _allowed.AddRange(values);
        return this;
    }

    public string Name { get; init; }

    public bool Validate()
    {
        throw new NotImplementedException();
    }
}