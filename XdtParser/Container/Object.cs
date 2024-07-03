using XdtParser.Enums;
using XdtParser.Rules;
using XdtParser.XdtTypes.LdtTest;

namespace XdtParser.Container;

internal abstract class Object : BaseXdtElement
{
    private string _attribute;

    protected Object(string type, string attribute) : base(type)
    {
        _attribute = attribute;

        var start = new Field(description: FieldDescFactory.Get("8002"), parent: this,
            rules: new() { new AllowedContentRule(type) }, multiple: false, presence: Presence.M);
        var end = new Field(description: FieldDescFactory.Get("8003"), parent: this,
            rules: new() { new AllowedContentRule(type) }, multiple: false, presence: Presence.M);

        Children.WithChild(start);
        Children.WithChild(end);
        Children.UseSubchildForAdding(start);
    }
}