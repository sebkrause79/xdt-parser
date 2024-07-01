﻿using XdtParser.Container;
using XdtParser.Interface;

namespace XdtParser.Helper;

internal static class ContainerHelper
{
    public static Field? GetFieldAbove(this IContainer item)
    {
        do
        {
            item = item.Parent;
            if (item is null or Field)
            {
                return (Field?)item;
            }
        } while (true);
    }
}