using XdtParser.Container;

namespace XdtParser.Helper;

internal static class ContainerHelper
{
    public static Field? GetFieldAbove(this IContainer item)
    {
        do
        {
            item = item.Parent;
        } while (item is not Field && item is not null);

        return (Field?)item;
    }
}