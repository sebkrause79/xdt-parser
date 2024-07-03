using XdtParser.Container;
using XdtParser.Enums;
using XdtParser.Interface;

namespace XdtParser.XdtTypes.LdtTest;

internal class Sentence8215 : Sentence
{
    public Sentence8215() : base("8215")
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