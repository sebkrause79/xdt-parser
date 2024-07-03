using XdtParser.Enums;
using XdtParser.Interface;

namespace XdtParser.Container;

internal class Children : IValidatable, IXdtLineConsumer
{
    private List<IContainer> Containers { get; set; } = new() { new PlainContainer() };

    private Children? _subChildToAdd = null;

    private Multiplicity _lastMultiplicity = Multiplicity.Single;

    public void WithChild(IXdtElement child)
    {
        if (_subChildToAdd is not null)
        {
            _subChildToAdd.WithChild(child);
            return;
        }

        if (child is Field { Multiplicity: Multiplicity.Multiple })
        {
            if (Containers.Last().Elements.Count == 0)
            {
                Containers = Containers.SkipLast(1).ToList();
            }

            Containers.Add(new Loop());
            _lastMultiplicity = Multiplicity.Multiple;
        }
        else if (child is Field { Multiplicity: Multiplicity.Single } && _lastMultiplicity == Multiplicity.Multiple)
        {
            Containers.Add(new PlainContainer());
            _lastMultiplicity = Multiplicity.Single;
        }

        Containers.Last().Elements.Add(child);
    }

    public bool IsValid() => Containers.TrueForAll(c => c.IsValid());
    
    public bool TakeLine(XdtLine line) => Containers.Last().TakeLine(line);

    public void UseSubchildForAdding(IXdtElement element) => _subChildToAdd = element.Children;
}