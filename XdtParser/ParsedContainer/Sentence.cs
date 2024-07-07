using XdtParser.Enums;
using XdtParser.Helper;
using XdtParser.Interface;
using XdtParser.Rules;
using XdtParser.XdtTypes.LdtTest.Factories;

namespace XdtParser.ParsedContainer;

public abstract class Sentence : BaseXdtElement
{
    private readonly string _objectName;
    public override IXdtElement? Parent
    {
        get => null!;
        set { }
    }

    protected Sentence(string objectName, string sentenceStartFi, string sentenceEndFi) : base(objectName)
    {
        _objectName = objectName;

        var start = new Field(description: FieldDescFactory.Get(sentenceStartFi), parent: this,
            rules: new() { new AllowedContentRule(objectName, RuleCategory.Basis) }, multiple: false, presence: Presence.M);
        var end = new Field(description: FieldDescFactory.Get(sentenceEndFi), parent: this,
            rules: new() { new AllowedContentRule(objectName, RuleCategory.Basis) }, multiple: false, presence: Presence.M);

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
               $"Sentence {_objectName}: {(IsValid() ? "ok" : "INVALID")}\r\n" + 
               Children.GetTreeView(indent + 1, indentUnit);
    }
}