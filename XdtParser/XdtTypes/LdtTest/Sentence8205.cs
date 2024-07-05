using XdtParser.Container;
using XdtParser.Enums;

namespace XdtParser.XdtTypes.LdtTest;

internal class Sentence8205 : Sentence
{
    public Field Feld_1 => this.Children.GetField("1234");
    public Field Feld_2 => this.Children.GetField("1235");
    public Field Feld_3 => this.Children.GetField("1236");

    public Sentence8205() : base("8205")
    {
        this
            .WithChild(
                new Field(description: FieldDescFactory.Get("1234"), parent: this, rules: null, presence: Presence.K, multiple: true)
                    .WithChild(
                        new Field(description: FieldDescFactory.Get("1235"), rules: null, presence: Presence.m, multiple: false)
                    )
            );
        this
            .WithChild(
                new Field(description: FieldDescFactory.Get("1236"), rules: null, presence: Presence.K, multiple: false)
            );
    }
}