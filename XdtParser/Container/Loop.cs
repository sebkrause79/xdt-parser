using XdtParser.Enums;
using XdtParser.Helper;
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
        if (TryAddToNewContainer(line))
        {
            return true;
        }

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

        if (TryAddToNewContainer(line))
        {
            return true;
        }

        return success;
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

    public string GetTreeView(int indent, string indentUnit)
    {
        return indentUnit.Repeat(indent) +
               $"LoopContainer:\r\n" +
               string.Join("", _elements.Select(e => e.GetTreeView(indent + 1, indentUnit)));
    }

    private bool TryAddToNewContainer(XdtLine line)
    {
        if (Elements.All(c => c is { ContainerState: ContainerState.Finished, Children.GotXdtContent: true }))
        {
            _elements.Add(_elements.First().GetClearedCopy());

            if (TakeLine(line))
            {
                return true;
            }

            if (Elements.TrueForAll(c => c is { ContainerState: ContainerState.Finished, Children.GotXdtContent: false } or Field { HasContent: false, Children.GotXdtContent: false }))
            {
                _elements.Remove(_elements.Last());
                ContainerState = ContainerState.Finished;
            }
        }

        return false;
    }
}