namespace XdtParser;

internal class XdtLine {
    internal string FieldIdentifier { get; set; }
    internal readonly List<string> Payload = new();

    internal XdtLine(string txt) 
    {
        FieldIdentifier = txt[3..7];
        Payload.Add(txt.Trim().Length > 7 
            ? txt.Trim()[7..] 
            : string.Empty);
    }

    internal bool MergeLine(XdtLine? line)
    {
        if (line is null || line.FieldIdentifier != FieldIdentifier)
        {
            return false;
        }

        Payload.AddRange(line.Payload);
        return true;
    }

    internal string GetXdt()
    {
        return string.Join("", Payload.Select(p => 
            $"{p.Length + 9:D3}{FieldIdentifier}{p}\r\n"));
    }
}