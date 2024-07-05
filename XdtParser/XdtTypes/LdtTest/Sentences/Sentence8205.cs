using XdtParser.Container;
using XdtParser.Enums;
using XdtParser.XdtTypes.LdtTest.Factories;
using XdtParser.XdtTypes.LdtTest.Objects;

namespace XdtParser.XdtTypes.LdtTest.Sentences;

internal class Sentence8205 : Sentence
{
    public Field Feld_1 => Children.GetField("1234");
    public Field Feld_2 => Children.GetField("1235");
    public Obj_0001 Arzt => (Obj_0001)Children.GetObject("Arzt");
    public Obj_0001 Patient => (Obj_0001)Children.GetObject("Patient");

    public Sentence8205() : base("8205")
    {
        
            WithChild(
                new Field(description: FieldDescFactory.Get("1234"), parent: this, rules: null, presence: Presence.K, multiple: true)
                    .WithChild(
                        new Field(description: FieldDescFactory.Get("1235"), rules: null, presence: Presence.m, multiple: false)
                    )
            )
            .WithChild(
                ObjectFactory.GetObject("Obj_0001", "2001", rules: null, presence: Presence.M, multiple: false)
            )
            .WithChild(
                ObjectFactory.GetObject("Obj_0001", "2002", rules: null, presence: Presence.M, multiple: false)
            )
            ;
    }
}