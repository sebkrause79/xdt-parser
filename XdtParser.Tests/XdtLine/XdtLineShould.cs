using FluentAssertions;

namespace XdtParser.Tests.XdtLine;

public class XdtLineShould
{
    [Fact]
    public void ParseXdtCorrectly()
    {
        var xdt = @"
01380008215
0141234Row 1
0141234Row 2
0201235Another Row
01380018215
";
        var parsed = XdtDocument.FromXdt(xdt);

        parsed["8000"].Should().Be("8215");
        parsed["1234"].Should().Be("Row 1\r\nRow 2");
        parsed["1235"].Should().Be("Another Row");
        parsed["8001"].Should().Be("8215");
        parsed.GetXdt().Should().Be(xdt.TrimStart());
    }
}