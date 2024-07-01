namespace XdtParser.Container;

public interface IContainer
{
    IContainer Parent { get; }
    List<IContainer> Children { get; set; }

    bool IsValid();

    bool TakeLines(List<XdtLine> lines);
}