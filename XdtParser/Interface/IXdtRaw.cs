using XdtParser.RawContainer;

namespace XdtParser.Interface
{
    public interface IXdtRaw
    {
        IEnumerable<string> this[string fi] { get; }

        string ExportXdt();

        List<XdtLine> Lines { get; }
    }
}