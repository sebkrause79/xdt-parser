using XdtParser.Enums;
using XdtParser.Interface;
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

    public string Index => _name;

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
        return _elements.TrueForAll(e => e.IsValid());
    }

    public bool TakeLine(XdtLine line)
    {
        if (IsPassed)
        {
            return false;
        }

        foreach (var item in _elements)
        {
            if (item.TakeLine(line))
            {
                return true;
            }
        }

        IsPassed = true;

        return false;
    }

    public bool IsPassed { get; private set; } = false;
}