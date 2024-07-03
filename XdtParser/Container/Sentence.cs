using XdtParser.Enums;
using XdtParser.Interface;
using XdtParser.Rules;
using XdtParser.XdtTypes.LdtTest;

namespace XdtParser.Container;

internal abstract class Sentence : BaseXdtElement
{
    public override IXdtElement? Parent
    {
        get => null!;
        set { }
    }

    protected Sentence(string type) : base(type)
    {
        var start = new Field(description: FieldDescFactory.Get("8000"), parent: this,
            rules: new() { new AllowedContentRule(type) }, multiple: false, presence: Presence.M);
        var end = new Field(description: FieldDescFactory.Get("8001"), parent: this,
            rules: new() { new AllowedContentRule(type) }, multiple: false, presence: Presence.M);

        Children.WithChild(start);
        Children.WithChild(end);
        Children.UseSubchildForAdding(start);
    }
}