using XdtParser.Container;
using XdtParser.Enums;
using XdtParser.XdtTypes.LdtTest.Factories;

namespace XdtParser.XdtTypes.LdtTest.Sentences;

internal class Sentence8231 : Sentence
{
    public Sentence8231() : base("8231")
    {
        WithChild(
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
        WithChild(
            new Field(
                description: FieldDescFactory.Get("1236"),
                rules: null,
                presence: Presence.K,
                multiple: false
            )
        );
    }
}