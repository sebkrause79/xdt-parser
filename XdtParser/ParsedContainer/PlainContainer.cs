﻿using XdtParser.Enums;
using XdtParser.Helper;
using XdtParser.Interface;
using XdtParser.RawContainer;

namespace XdtParser.ParsedContainer;

internal class PlainContainer : IContainer
{
    public List<IXdtElement> Elements { get; set; } = new();

    public bool GotXdtContent { get; private set; }

    public ContainerState ContainerState { get; set; } = ContainerState.NotStarted;

    public bool IsValid()
    {
        return Elements.TrueForAll(c => c.IsValid());
    }

    public bool TakeLine(XdtLine line)
    {
        ContainerState = ContainerState.Open;
        var success = false;
        foreach (var child in Elements)
        {
            if (child.ContainerState == ContainerState.Finished)
            {
                continue;
            }

            success = child.TakeLine(line);
            if (success)
            {
                GotXdtContent = true;
                break;
            }
        }

        if (Elements.All(c => c.ContainerState == ContainerState.Finished))
        {
            ContainerState = ContainerState.Finished;
        }

        return success;
    }

    public IContainer GetClearedCopy()
    {
        var result = new PlainContainer();
        result.Elements.Clear();
        foreach (var element in Elements)
        {
            result.Elements.Add(element.GetClearedCopy());
        }
        return result;
    }

    public string GetTreeView(int indent, string indentUnit)
    {
        return indentUnit.Repeat(indent) +
               $"PlainContainer: {(IsValid() ? "ok" : "INVALID")}\r\n" +
               string.Join("", Elements.Select(e => e.GetTreeView(indent + 1, indentUnit)));
    }
}