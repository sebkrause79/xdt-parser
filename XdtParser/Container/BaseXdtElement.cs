using XdtParser.Enums;
using XdtParser.Interface;

namespace XdtParser.Container;

internal abstract class BaseXdtElement : IXdtElement, IUserCallable
{
    public virtual IXdtElement? Parent { get; set; }
    public Children Children { get; } = new();
    public string Index { get; }

    protected BaseXdtElement(string type)
    {
        Index = type;
    }

    public IXdtElement WithChild(IXdtElement child)
    {
        child.Parent = this;
        Children.WithChild(child);
        return this;
    }

    public ContainerState ContainerState { get; set; } = ContainerState.NotStarted;

    public virtual bool IsValid()
    {
        return Children.IsValid();
    }

    public virtual bool TakeLine(XdtLine line)
    {
        if (ContainerState == ContainerState.Finished)
        {
            return false;
        }

        return Children.TakeLine(line);
    }
}