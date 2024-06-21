using Newtonsoft.Json;
using FluentAssertions;

using LdtParser.Definition.Model;

namespace LdtParser.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var sut = JsonConvert.DeserializeObject<Sentences>(System.IO.File
                .ReadAllText(Path.Combine("Definition", "Json", "Sentences.json")));

            sut!.Saetze.First()
                .Elemente.Last()
                .Bezeichnung
                .Should().Be("Satzende");
            sut.Saetze.First()
                .Elemente.First()
                .Kindelemente.First()
                .Kindelemente.First()
                .Bezeichnung
                .Should().Be("Obj_Kopfdaten");
        }
    }
}