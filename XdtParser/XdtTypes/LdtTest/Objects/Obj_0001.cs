using XdtParser.Container;
using XdtParser.Enums;
using XdtParser.Interface;
using XdtParser.XdtTypes.LdtTest.Factories;

namespace XdtParser.XdtTypes.LdtTest.Objects;

internal class Obj_0001 : Container.Object
{
    public Field Nachname => Children.GetField("1237");
    public Field Vorname => Children.GetField("1238");

    public Obj_0001(string attributeFi, Presence? presence = null, bool multiple = false, List<IRule>? rules = null) : base("Obj_0001", attributeFi, presence, multiple, rules)
    {
        
            WithChild(
                new Field(description: FieldDescFactory.Get("1237"), rules: new() {RulesFactory.Get("NoDot")}, presence: Presence.K, multiple: false)
            )
            .WithChild(
                new Field(description: FieldDescFactory.Get("1238"), rules: null, presence: Presence.K, multiple: true)
            );
    }
}