namespace XdtParser.XdtTypes.LdtTest;

internal class Sentence8215 : Sentence
{
    public Sentence8215() : base("8215")
    {
        _rootElement
            .AddChild(new Field(FieldDescriptionFactory.Get("1234"), 
                FieldPresence.K, 
                fieldMultiplicity: FieldMultiplicity.Multiple)
                .AddChild(new Field(FieldDescriptionFactory.Get("1235"), 
                    FieldPresence.m)))
            .AddChild(new Field(FieldDescriptionFactory.Get("1236")))
            ;
    }
}