using XdtParser.Enums;
using XdtParser.Interface;
using XdtParser.Rules;
using XdtParser.XdtTypes.LdtTest;

namespace XdtParser.Container;

internal abstract class Object : IContainer
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

    protected Object(string name, IContainer parent)
    {
        _name = name;
        Parent = parent;

        _elements.Add(new Field(description: FieldDescriptionFactory.Get("8002"),
            parent: this,
            childs: new(),
            rules: new()
            {
                new AllowedContentRule(name)
            },
            multiplicity: Multiplicity.Single,
            presence: Presence.M));

        _elements.Add(new Field(description: FieldDescriptionFactory.Get("8003"),
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

    public bool TakeLine(XdtLine line)
    {
        throw new NotImplementedException();
    }

    public bool IsPassed { get; }

    public void TakeLines(List<XdtLine> lines)
    {
        throw new NotImplementedException();
    }
}