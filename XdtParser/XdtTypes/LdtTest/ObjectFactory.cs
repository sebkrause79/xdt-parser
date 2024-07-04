namespace XdtParser.XdtTypes.LdtTest;

internal static class ObjectFactory
{
    public static Container.Object GetObject(string objectName)
    {
        return objectName switch
        {
            _ => throw new ArgumentException($"Unknown object identifier '{objectName}'")
        };
    }
}