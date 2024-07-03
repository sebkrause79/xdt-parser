namespace XdtParser.Interface;

public interface IXdtLineConsumer
{
    bool TakeLine(XdtLine line);
}