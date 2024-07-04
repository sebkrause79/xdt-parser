using XdtParser.Enums;
using XdtParser.Interface;

namespace XdtParser.Container;

internal class Loop : IContainer
{
    private readonly List<IContainer> _elements = new();

    public List<IXdtElement> Elements
    {
        get
        {
            if (_elements.Count == 0)
            {
                _elements.Add(new PlainContainer());
            }
            return _elements.Last().Elements;
        }
    }

    public bool GotXdtContent { get; private set; }

    public ContainerState ContainerState { get; set; } = ContainerState.NotStarted;

    public bool IsValid()
    {
        return Elements.TrueForAll(c => c.IsValid());
    }

    public bool TakeLine(XdtLine line)
    {
        ContainerState = ContainerState.Open;
        var success = false;
        foreach (var child in Elements)
        {
            if (child.ContainerState == ContainerState.Finished)
            {
                continue;
            }

            success = child.TakeLine(line);
            if (success)
            {
                GotXdtContent = true;
                break;
            }
        }

        if (Elements.All(c => c is { ContainerState: ContainerState.Finished, Children.GotXdtContent: true }))
        {
            AddNextContainer();

            if (TakeLine(line))
            {
                return true;
            }

            if (Elements.TrueForAll(c => c.ContainerState == ContainerState.Open))
            {
                _elements.Remove(_elements.Last());
                ContainerState = ContainerState.Finished;
            }
        }

        return success;
    }

    private void AddNextContainer()
    {
        _elements.Add(_elements.First().GetClearedCopy());
    }

    public IContainer GetClearedCopy()
    {
        var result = new Loop();
        foreach (var child in Elements)
        {
            result.Elements.Add(child.GetClearedCopy());
        }

        return result;
    }
}