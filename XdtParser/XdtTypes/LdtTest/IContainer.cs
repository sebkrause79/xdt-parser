namespace XdtParser.XdtTypes.LdtTest;

internal interface IContainer
{
    bool IsValid();

    bool TakeLines(List<XdtLine> lines);

    IContainer AddChild(IContainer child);
}