namespace XdtParser.XdtTypes.LdtTest;

internal static class FieldDescriptionFactory
{
    public static FieldDescription Get(string field)
    {
        return field switch
        {
            "8000" => new FieldDescription() { Id = "8000", Length = "<=9", Format="alnum", Description="Satzstart" },
            "8001" => new FieldDescription() { Id = "8001", Length = "<=9", Format="alnum", Description="Satzende" },
            "1234" => new FieldDescription() { Id = "1234", Length = "<=9", Format="alnum", Description="Feld 1" },
            "1235" => new FieldDescription() { Id = "1235", Length = "<=9", Format="alnum", Description="Feld 2" },
            "1236" => new FieldDescription() { Id = "1236", Length = "<=9", Format="alnum", Description="Feld 3" },
            _ => throw new ArgumentException($"Unknown field identifier '{field}'")
        };
    }
}