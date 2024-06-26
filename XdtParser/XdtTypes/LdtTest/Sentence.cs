namespace XdtParser.XdtTypes.LdtTest;

internal abstract class Sentence : IContainer
{
    protected List<IContainer> _childs = new();
    protected string _name;

    protected Sentence(string name)
    {
        _name = name;
    }

    public IContainer AddChild(IContainer child)
    {
        _childs.Add(child);
        return this;
    }

    public bool IsValid()
    {
        if (!(_childs.First() is Field start) || start.FieldIdentifier != "8000" || start.Content != _name)
        {
            return false;
        }

        if (!(_childs.Last() is Field end) || end.FieldIdentifier != "8001" || end.Content != _name)
        {
            return false;
        }

        if (_childs.Count <= 2)
        {
            return false;
        }

        return _childs.All(child => child.IsValid());
    }

    public bool TakeLines(List<XdtLine> lines)
    {
        if (lines.Count < 3) 
        {
            throw new ArgumentException("Too few lines for a sentence!");
        }

        throw new NotImplementedException();
    }

    IContainer IContainer.AddChild(IContainer child)
    {
        throw new NotImplementedException();
    }
}