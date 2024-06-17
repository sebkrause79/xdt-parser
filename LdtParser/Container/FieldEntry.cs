using LdtParser.File;

namespace LdtParser.Container;
public class FieldEntry : IFieldContainer
{
    private int _fieldIdentifier;
    private List<string> _lines = new();

    public void AddLdtLine(LdtLine ldtLine)
    {
        _fieldIdentifier = ldtLine.FieldIdentifier;
        _lines.Add(ldtLine.Payload);
    }

    public string GetLdt()
    {
        throw new NotImplementedException();
    }
}