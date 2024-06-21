using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("LdtParser.Tests")]
namespace LdtParser.Definition.Model
{
    internal class Sentences
    {
        public string LdtVersion { get; set; }
        public List<Satz> Saetze { get; set; } = new();
    }
}
