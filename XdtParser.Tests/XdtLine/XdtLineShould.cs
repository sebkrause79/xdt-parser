using FluentAssertions;
using XdtParser.XdtTypes.LdtTest;

namespace XdtParser.Tests.XdtLine;

public class XdtLineShould
{
    [Fact]
    public void ParseXdtCorrectly()
    {
        var xdt = @"
01380008220
0141234Row 1
0141234Row 2
0201235Another Row
01380018220
01380008205
0141234Mein erster
0141234Befund
0201235Another Row
01380018205
01380008205
0141234Mein zweiter
0141234Befund
0201235Another Row
01380018205
01380008221
0141234Row 1
0141234Row 2
0201235Another Row
01380018221
";
        var parsed = XdtDocument.FromXdt(xdt);
        var ldt = parsed.AsLdt();

        ldt.IsValid().Should().BeTrue();

        if (ldt is LdtDocumentLaboratoryToSender doc)
        {
            doc.Befunde.First().Feld_1.Content.Should().Be("Mein erster\r\nBefund");
        }

        parsed["8000"].Should().Be("8215");
        parsed["1234"].Should().Be("Row 1\r\nRow 2");
        parsed["1235"].Should().Be("Another Row");
        parsed["8001"].Should().Be("8215");
        parsed.GetXdt().Should().Be(xdt.TrimStart());
    }
}