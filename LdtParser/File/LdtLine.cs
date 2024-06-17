namespace LdtParser.File;
public class LdtLine {
    public int Length { get; set; }
    public int FieldIdentifier { get; set; }
    public string Payload { get; set; }

    public LdtLine(string txt) {
        Length = int.Parse(txt[..3]);
        FieldIdentifier = int.Parse(txt[3..7]);
        Payload = txt.Trim().Length > 7 
            ? txt.Trim()[7..] 
            : string.Empty;
    }
}