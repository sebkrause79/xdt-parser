namespace XdtParser.Interface;

public interface IContainer
{
    IContainer Parent { get; }

    string Index { get; }

    List<IContainer> Children { get; set; }

    bool IsValid();

    bool TakeLine(XdtLine line);

    bool IsPassed { get; }
}