namespace LdtParser.File;
public class LdtLine {
    public string Length { get; set; }
    public string FieldIdentifier { get; set; }
    public string Payload { get; set; }

    public LdtLine(string txt) {
        Length = txt[..3];
        FieldIdentifier = txt[3..7];
        Payload = txt.Trim().Length > 7 
            ? txt.Trim()[7..] 
            : string.Empty;
    }
}