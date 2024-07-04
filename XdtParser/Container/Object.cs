using XdtParser.Enums;
using XdtParser.Interface;
using XdtParser.Rules;
using XdtParser.XdtTypes.LdtTest;

namespace XdtParser.Container;

internal abstract class Object : BaseXdtElement
{
    private string _attribute;
    private string _type;

    protected Object(string type, string attribute) : base(type)
    {
        _attribute = attribute;
        _type = type;

        var start = new Field(description: FieldDescFactory.Get("8002"), parent: this,
            rules: new() { new AllowedContentRule(type) }, multiple: false, presence: Presence.M);
        var end = new Field(description: FieldDescFactory.Get("8003"), parent: this,
            rules: new() { new AllowedContentRule(type) }, multiple: false, presence: Presence.M);

        Children.WithChild(start);
        Children.WithChild(end);
        Children.UseSubchildForAdding(start);
    }

    public override IXdtElement GetClearedCopy()
    {
        var result = ObjectFactory.GetObject(_type);
        result.Children = Children.GetClearedCopy();
        return result;
    }
}