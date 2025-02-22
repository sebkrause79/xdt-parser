﻿using XdtParser.Enums;
using XdtParser.Interface;
using XdtParser.RawContainer;
using XdtParser.XdtTypes;

namespace XdtParser.ParsedContainer;

public abstract class BaseXdtElement : IXdtElement, IXdtParsed
{
    public virtual IXdtElement? Parent { get; set; }
    public Children Children { get; set; } = new();
    public string Index { get; }
    public DocumentType? DocumentType { get; protected set; } = null;
    protected IXdtElement? _subChildForAdding;

    protected BaseXdtElement(string objectName)
    {
        Index = objectName;
    }

    public IXdtElement WithChild(IXdtElement child)
    {
        child.Parent = _subChildForAdding ?? this;
        Children.WithChild(child);
        return this;
    }

    public ContainerState ContainerState { get; set; } = ContainerState.NotStarted;

    public virtual bool IsValid()
    {
        return Children.IsValid();
    }

    public virtual bool TakeLine(XdtLine line)
    {
        if (ContainerState == ContainerState.Finished)
        {
            return false;
        }

        return Children.TakeLine(line);
    }

    public abstract IXdtElement GetClearedCopy();
    public abstract string GetTreeView(int indent, string indentUnit);
}