using XdtParser.Enums;
using XdtParser.Helper;
using XdtParser.Interface;

namespace XdtParser.Container;

internal class Children : IValidatable, IXdtLineConsumer, ICopyable<Children>, ITreeView
{
    public List<IContainer> Containers { get; private set; } = new();

    private Children? _subChildToAdd = null;

    private Multiplicity _lastMultiplicity = Multiplicity.Single;

    public void WithChild(IXdtElement child)
    {

        if (_subChildToAdd is not null)
        {
            _subChildToAdd.WithChild(child);
            return;
        }

        if (Containers.Count == 0)
        {
            Containers.Add(new PlainContainer());
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

    public bool HasChildren => Containers.Any();

    public bool GotXdtContent => Containers.Any(c => c.GotXdtContent);

    public bool TakeLine(XdtLine line)
    {
        foreach (var container in Containers.Where(c => c.ContainerState != ContainerState.Finished))
        {
            if (container.TakeLine(line))
            {
                return true;
            }
        }

        return false;
    }

    public void UseSubchildForAdding(IXdtElement element) => _subChildToAdd = element.Children;

    internal Field GetField(string fi)
    {
        var x1 = Containers.Union(_subChildToAdd?.Containers ?? new List<IContainer>());
        var x2 = x1.SelectMany(c => c.GetFields());
        var x3 = x2.FirstOrDefault(f => f.Index == fi)!;
        return x3;
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

    public string GetTreeView(int indent, string indentUnit)
    {
        return indentUnit.Repeat(indent) +
        $"Children:\r\n" +
               string.Join("", Containers.Select(e => e.GetTreeView(indent + 1, indentUnit)));
    }
}