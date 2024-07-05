using XdtParser.Container;
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

    public static Field? FindFieldAbove(this IXdtElement? item, string needleFi)
    {
        do
        {
            item = item.GetFieldAbove();
        } while (item is Field f && f.Index != needleFi);

        return (Field?)item;
    }
}