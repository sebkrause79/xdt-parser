using XdtParser.Container;
using XdtParser.Enums;

namespace XdtParser.XdtTypes.LdtTest;

internal class Sentence8231 : Sentence
{
    public Sentence8231() : base("8231")
    {
        this.WithChild(
            new Field(
                    description: FieldDescFactory.Get("1234"),
                    parent: this,
                    rules: null,
                    presence: Presence.K,
                    multiple: true
                )
                .WithChild(
                    new Field(
                        description: FieldDescFactory.Get("1235"),
                        rules: null,
                        presence: Presence.m,
                        multiple: false
                    )
                )
        );
        this.WithChild(
            new Field(
                description: FieldDescFactory.Get("1236"),
                rules: null,
                presence: Presence.K,
                multiple: false
            )
        );
    }
}