namespace LdtParser.Definition.Model
{
    internal class Element
    {
        public string Typ { get; set; }
        public string Name { get; set; }
        public string Anzahl { get; set; }
        public string Bezeichnung { get; set; }
        public string Feldart { get; set; }
        public List<string> Regeln { get; set; } = new();
        public List<Element> Kindelemente { get; set; } = new();
    }
}
