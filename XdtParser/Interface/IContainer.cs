namespace XdtParser.Interface;

public interface IContainer
{
    IContainer Parent { get; }

    string Index { get; }

    List<IContainer> Children { get; set; }

    bool IsValid();

    void TakeLines(List<XdtLine> lines);
}