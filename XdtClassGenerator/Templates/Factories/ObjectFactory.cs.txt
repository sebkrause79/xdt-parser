using XdtParser.Enums;
using XdtParser.Interface;
using XdtParser.XdtTypes.LdtTest.Objects;

namespace XdtParser.XdtTypes.LdtTest.Factories;

internal static class ObjectFactory
{
    public static ParsedContainer.Object GetObject(string objectName, string attrFieldIdentifier, Presence? presence = null, bool multiple = false, List<IRule>? rules = null)
    {
        return objectName switch
        {
            "Obj_0001" => new Obj_0001(attrFieldIdentifier, presence, multiple, rules),
            _ => throw new ArgumentException($"Unknown object identifier '{objectName}'")
        };
    }
}