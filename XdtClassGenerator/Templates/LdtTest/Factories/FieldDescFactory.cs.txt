using XdtParser.ParsedContainer;

namespace XdtParser.XdtTypes.LdtTest.Factories;

internal static class FieldDescFactory
{
    public static FieldDescription Get(string field)
    {
        return field switch
        {
            "8000" => new FieldDescription() { Id = "8000", Length = "<=9", Format = "alnum", Description = "Satzstart" },
            "8001" => new FieldDescription() { Id = "8001", Length = "<=9", Format = "alnum", Description = "Satzende" },
            "8002" => new FieldDescription() { Id = "8002", Length = "<=9", Format = "alnum", Description = "Objektstart" },
            "8003" => new FieldDescription() { Id = "8003", Length = "<=9", Format = "alnum", Description = "Objektende" },
            "1234" => new FieldDescription() { Id = "1234", Length = "<=9", Format = "alnum", Description = "Feld 1" },
            "1235" => new FieldDescription() { Id = "1235", Length = "<=9", Format = "alnum", Description = "Feld 2" },
            "1236" => new FieldDescription() { Id = "1236", Length = "<=9", Format = "alnum", Description = "Feld 3" },
            "2001" => new FieldDescription() { Id = "2001", Length = "<=9", Format = "alnum", Description = "Arzt" },
            "2002" => new FieldDescription() { Id = "2002", Length = "<=9", Format = "alnum", Description = "Patient" },
            "1237" => new FieldDescription() { Id = "1237", Length = "<=9", Format = "alnum", Description = "Nachname" },
            "1238" => new FieldDescription() { Id = "1238", Length = "<=9", Format = "alnum", Description = "Vorname" },
            _ => throw new ArgumentException($"Unknown field identifier '{field}'")
        };
    }
}