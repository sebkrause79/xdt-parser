using LdtParser.File;

namespace LdtParser.Container;
public interface IFieldContainer {
    void AddLdtLine(LdtLine ldtLine);
    string GetLdt();
}