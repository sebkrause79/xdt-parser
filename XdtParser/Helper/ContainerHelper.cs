using XdtParser.ParsedContainer;
using XdtParser.Interface;

namespace XdtParser.Helper;

internal static class ContainerHelper
{
    public static List<Field> GetFields(this IContainer container)
    {
        var result = container.Elements.OfType<Field>().ToList();
        foreach(var field in container.Elements.OfType<Field>())
        {
            result.AddRange(field.Children.Containers.SelectMany(c => c.GetFields()));
        }
        return result;
    }
}
