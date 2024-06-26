namespace XdtParser.XdtTypes.LdtTest;

internal class Sentence8215 : Sentence
{
    public Sentence8215() : base("8215")
    {
        _childs = new List<IContainer>()
        {
            new Field(FieldDescriptionFactory.Get("8000"))
                .AddChild(new Field(FieldDescriptionFactory.Get("1234")))
                .AddChild(new Field(FieldDescriptionFactory.Get("1235"))),
            new Field(FieldDescriptionFactory.Get("8001")) 
        };
    }
}