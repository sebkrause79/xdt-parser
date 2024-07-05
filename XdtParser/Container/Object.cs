using XdtParser.Enums;
using XdtParser.Helper;
using XdtParser.Interface;
using XdtParser.Rules;
using XdtParser.XdtTypes.LdtTest;

namespace XdtParser.Container;

internal abstract class Object : BaseXdtElement
{
    private readonly string _objectName;

    protected Object(string objectName) : base(objectName)
    {
        _objectName = objectName;

        var start = new Field(description: FieldDescFactory.Get("8002"), parent: this,
            rules: new() { new AllowedContentRule(objectName) }, multiple: false, presence: Presence.M);
        var end = new Field(description: FieldDescFactory.Get("8003"), parent: this,
            rules: new() { new AllowedContentRule(objectName) }, multiple: false, presence: Presence.M);

        Children.WithChild(start);
        Children.WithChild(end);
        _subChildForAdding = start;
        Children.UseSubchildForAdding(start);
    }

    public override IXdtElement GetClearedCopy()
    {
        var result = ObjectFactory.GetObject(_objectName);
        result.Children = Children.GetClearedCopy();
        return result;
    }

    public override string GetTreeView(int indent, string indentUnit)
    {
        return indentUnit.Repeat(indent) +
               $"Object {_objectName}:\r\n" +
               Children.GetTreeView(indent + 1, indentUnit);
    }
}