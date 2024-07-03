using XdtParser.Container;
using XdtParser.Enums;
using XdtParser.Interface;

namespace XdtParser.XdtTypes.LdtTest;

internal class Sentence8221 : Sentence
{
    public Sentence8221() : base("8221")
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