using XdtParser.Enums;
using XdtParser.Rules;
using XdtParser.XdtTypes.LdtTest;

namespace XdtParser.Container;

internal abstract class Sentence : IContainer
{
    protected string _name;

    private readonly List<IContainer> _elements = new();

    protected IContainer _rootElement => _elements.First();

    public IContainer Parent { get; init; }
    public List<IContainer> Children
    {
        get => _rootElement.Children; set => _rootElement.Children = value;
    }

    protected Sentence(string name, IContainer parent)
    {
        _name = name;
        Parent = parent;

        _elements.Add(new Field(description: FieldDescriptionFactory.Get("8000"),
            parent: this,
            childs: new(),
            rules: new()
            {
                new AllowedContentRule(name)
            },
            multiplicity: Multiplicity.Single,
            presence: Presence.M));

        _elements.Add(new Field(description: FieldDescriptionFactory.Get("8001"),
            parent: this,
            childs: null,
            rules: new()
            {
                new AllowedContentRule(name)
            },
            multiplicity: Multiplicity.Single,
            presence: Presence.M));
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

        foreach (var item in _elements)
        {
            if (!item.TakeLines(lines))
            {
                return false;
            }
        }

        return true;
    }
}