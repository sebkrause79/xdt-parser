using XdtParser.Container;
using XdtParser.Enums;
using XdtParser.Interface;

namespace XdtParser.XdtTypes.LdtTest;

internal class Sentence8205 : Sentence
{
    public Sentence8205(IContainer parent) : base("8205", parent)
    {
        Children = new List<IContainer>()
        {
            new Field(
                description: FieldDescriptionFactory.Get("1234"),
                parent: _rootElement,
                childs: new()
                {
                    new Field(
                        description: FieldDescriptionFactory.Get("1235"),
                        parent: _rootElement,
                        childs: new()
                        {
                        },
                        rules: null,
                        presence: Presence.m,
                        multiplicity: Multiplicity.Single
                    )
                },
                rules: null,
                presence: Presence.K,
                multiplicity: Multiplicity.Multiple
            ),
            new Field(
                description: FieldDescriptionFactory.Get("1236"),
                parent: _rootElement,
                childs: new()
                {
                },
                rules: null,
                presence: Presence.K,
                multiplicity: Multiplicity.Single
            )
        };
    }
}