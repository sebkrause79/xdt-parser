using XdtParser.XdtTypes;

namespace XdtParser.Interface;

public interface IXdtParsed
{
    string Index { get; }

    DocumentType? DocumentType { get; }
}