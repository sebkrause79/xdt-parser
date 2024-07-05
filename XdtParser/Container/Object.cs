using XdtParser.Enums;
using XdtParser.Helper;
using XdtParser.Interface;
using XdtParser.Rules;
using XdtParser.XdtTypes.LdtTest.Factories;

namespace XdtParser.Container;

internal abstract class Object : BaseXdtElement
{
    private readonly string _objectName;
    private readonly string _attributeFi;
    private readonly Presence? _presence;
    private readonly bool _multiple;
    private readonly List<IRule>? _rules;

    internal string Attribute { get; init; }

    protected Object(string objectName, string attributeFi, Presence? presence = null, bool multiple = false,
        List<IRule>? rules = null) : base(objectName)
    {
        _objectName = objectName;
        _attributeFi = attributeFi;
        _presence = presence;
        _multiple = multiple;
        _rules = rules;

        var attrFieldDesc = FieldDescFactory.Get(attributeFi);
        Attribute = attrFieldDesc.Description;
        var attrField = new Field(description: attrFieldDesc, 
            parent: this,
            rules: (rules ?? new()).Prepend(new AllowedContentRule(Attribute, RuleCategory.Basis)).ToList(),
            presence: presence, 
            multiple: multiple);
        var start = new Field(description: FieldDescFactory.Get("8002"), parent: attrField,
            rules: new() { new AllowedContentRule(objectName, RuleCategory.Basis) }, multiple: false, presence: Presence.M);
        var end = new Field(description: FieldDescFactory.Get("8003"), parent: attrField,
            rules: new() { new AllowedContentRule(objectName, RuleCategory.Basis) }, multiple: false, presence: Presence.M);

        Children.WithChild(attrField
            .WithChild(start)
            .WithChild(end));
        _subChildForAdding = start;
        Children.UseSubchildForAdding(start);
    }

    public override IXdtElement GetClearedCopy()
    {
        var result = ObjectFactory.GetObject(_objectName, _attributeFi, _presence, _multiple, _rules);
        result.Children = Children.GetClearedCopy();
        return result;
    }

    public override string GetTreeView(int indent, string indentUnit)
    {
        return indentUnit.Repeat(indent) +
               $"Object {_objectName}:  {(IsValid() ? "ok" : "INVALID")}\r\n" +
               Children.GetTreeView(indent + 1, indentUnit);
    }
}