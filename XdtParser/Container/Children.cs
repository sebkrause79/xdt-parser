using XdtParser.Enums;
using XdtParser.Interface;

namespace XdtParser.Container;

internal class Children : IValidatable, IXdtLineConsumer, ICopyable<Children>
{
    private List<IContainer> Containers { get; set; } = new();

    private Children? _subChildToAdd = null;

    private Multiplicity _lastMultiplicity = Multiplicity.Single;

    public void WithChild(IXdtElement child)
    {
        if (Containers.Count == 0)
        {
            Containers.Add(new PlainContainer());
        }

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

    public bool GotXdtContent => Containers.Any(c => c.GotXdtContent);

    public bool TakeLine(XdtLine line) => Containers
        .FirstOrDefault(c => c.ContainerState != ContainerState.Finished)?
        .TakeLine(line) ?? false;

    public void UseSubchildForAdding(IXdtElement element) => _subChildToAdd = element.Children;

    internal Field GetField(string fi)
    {
        foreach (var container in Containers)
        {
            var element = container.Elements.FirstOrDefault(x => x is Field field && field.Index == fi);
            if (element is not null)
            {
                return (Field)element;
            }
        }

        return null!;
    }

    public Children GetClearedCopy()
    {
        var result = new Children();
        var dict = Containers.ToDictionary(c => c, c => c.GetClearedCopy());
        foreach (var (oldContainer, newContainer) in dict)
        {
            result.Containers.Add(newContainer);
            var pos = oldContainer.Elements.FindIndex(elem => elem.Children == _subChildToAdd);
            if (pos >= 0)
            {
                result.UseSubchildForAdding(newContainer.Elements[pos]);
            }
        }

        return result;
    }
}