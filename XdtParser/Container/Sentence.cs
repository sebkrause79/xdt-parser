using XdtParser.Enums;
using XdtParser.Helper;
using XdtParser.Interface;
using XdtParser.Rules;
using XdtParser.XdtTypes.LdtTest;

namespace XdtParser.Container;

internal abstract class Sentence : BaseXdtElement
{
    private readonly string _objectName;
    public override IXdtElement? Parent
    {
        get => null!;
        set { }
    }

    protected Sentence(string objectName) : base(objectName)
    {
        _objectName = objectName;

        var start = new Field(description: FieldDescFactory.Get("8000"), parent: this,
            rules: new() { new AllowedContentRule(objectName) }, multiple: false, presence: Presence.M);
        var end = new Field(description: FieldDescFactory.Get("8001"), parent: this,
            rules: new() { new AllowedContentRule(objectName) }, multiple: false, presence: Presence.M);

        Children.WithChild(start);
        Children.WithChild(end);
        _subChildForAdding = start;
        Children.UseSubchildForAdding(start);
    }

    public override IXdtElement GetClearedCopy()
    {
        throw new InvalidOperationException("A sentence may not be copied");
    }

    public override string GetTreeView(int indent, string indentUnit)
    {
        return indentUnit.Repeat(indent) + 
               $"Sentence {_objectName}:\r\n" + 
               Children.GetTreeView(indent + 1, indentUnit);
    }
}