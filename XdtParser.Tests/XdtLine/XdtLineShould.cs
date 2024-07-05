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
0201234Mein erster
0151234Befund
0201235Another Row
01380018205
01380008205
0211234Mein zweiter
0151234Befund
0201235Another Row
0171236Optional
01380018205
01380008221
0141234Row 1
0141234Row 2
0201235Another Row
01380018221
";
        var parsed = XdtDocument.FromXdt(xdt);
        parsed["8000"].Should().Be("8220");
        parsed["1234"].Should().Be("Row 1\r\nRow 2");
        parsed["1235"].Should().Be("Another Row");
        parsed["8001"].Should().Be("8220");
        parsed.GetXdt().Should().Be(xdt.TrimStart());
        
        var ldt = parsed.AsLdt();

        var tree = ldt.GetTreeView("|  ");


        if (ldt is LdtDocumentLaboratoryToSender doc)
        {
            var field2_2 = doc.Befunde[1].Feld_1;

            doc.Befunde.First().Feld_1.Content.Should().Be("Mein erster\r\nBefund");
        }

        ldt.IsValid().Should().BeTrue();

    }
}