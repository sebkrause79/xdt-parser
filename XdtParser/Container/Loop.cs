using XdtParser.Enums;
using XdtParser.Interface;

namespace XdtParser.Container;

internal class Loop : IContainer
{
    private readonly List<IContainer> _elements = new() { new PlainContainer() } ;

    public List<IXdtElement> Elements
    {
        get => _elements.Last().Elements;
    }

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
                break;
            }
        }

        if (Elements.All(c => c.ContainerState == ContainerState.Finished))
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
        throw new NotImplementedException();
    }
}