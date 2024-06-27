namespace XdtParser.XdtTypes.LdtTest;

internal abstract class Sentence : IContainer
{
    private readonly List<IContainer> _childs = new();
    protected string _name;

    protected IContainer _rootElement => _childs.First();

    protected Sentence(string name)
    {
        _name = name;
        _childs.Add(new Field(FieldDescriptionFactory.Get("8000"), FieldPresence.M, new List<IRule>{RulesFactory.Get(name)}));
        _childs.Add(new Field(FieldDescriptionFactory.Get("8001"), FieldPresence.M, new List<IRule>{RulesFactory.Get(name)}));
    }

    public IContainer AddChild(IContainer child)
    {
        _rootElement.AddChild(child);
        return this;
    }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }

    public bool TakeLines(List<XdtLine> lines)
    {
        if (lines.Count < 3) 
        {
            throw new ArgumentException("Too few lines for a sentence!");
        }

        throw new NotImplementedException();
    }
}