using XdtParser.ParsedContainer;
using XdtParser.Interface;

namespace XdtParser.Helper;

internal static class TreeHelper
{
    public static Field? GetFieldAbove(this IXdtElement? item)
    {
        do
        {
            item = item?.Parent;
            if (item is null or Field)
            {
                return (Field?)item;
            }
        } while (true);
    }
}