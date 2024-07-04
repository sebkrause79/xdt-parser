namespace XdtParser.Helper;

internal static class StringHelper
{
    public static string Repeat(this string s, int count) =>
        string.Join(string.Empty, Enumerable.Repeat(s, count));
}
