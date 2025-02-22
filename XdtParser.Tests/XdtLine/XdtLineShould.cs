﻿using FluentAssertions;
using XdtParser.RawContainer;
using XdtParser.XdtTypes;
using XdtParser.XdtTypes.LdtTest;

namespace XdtParser.Tests.XdtLine;

public class XdtLineShould
{
    [Fact]
    public void ParseXdtCorrectly()
    {
        var fileTxt = @"
01380008220
0141234Row 1
0141234Row 2
0201235Another Row
01380018220
01380008205
0201234Mein erster
0151234Befund
0201235Another Row
0132001Arzt
0178002Obj_0001
0151237Dr. No
0141238Peter
0178003Obj_0001
0162002Patient
0178002Obj_0001
0161237Muelle.
0161238Herbert
0178003Obj_0001
01380018205
01380008205
0211234Mein zweiter
0151234Befund
0201235Another Row
0132001Arzt
0178002Obj_0001
0151237Dr. No
0141238Peter
0178003Obj_0001
0162002Patient
0178002Obj_0001
0161237Muelle.
0121238Ann
0151238Katrin
0178003Obj_0001
01380018205
01380008221
0141234Row 1
0141234Row 2
0201235Another Row
01380018221
";
        var raw = XdtParser.GetRaw(fileTxt);
        raw["8000"].Should().BeEquivalentTo("8220", "8205", "8205", "8221");
        raw["1234"].Should().Contain("Row 1\r\nRow 2");
        raw["1235"].Should().Contain("Another Row");
        raw["8001"].Should().Contain("8220");
        raw.ExportXdt().Should().Be(fileTxt.TrimStart());

        var xdt = XdtParser.GetLdt(fileTxt);
        xdt.DocumentType.Should().Be(DocumentType.LdtTest_LaboratoryToSender);
        var ldt = (LdtDocumentLaboratoryToSender)xdt;

        var tree = ldt.GetTreeView("|  ");

        ldt.Befunde.First().Feld_1.Content.Should().Be("Mein erster\r\nBefund");
        ldt.Befunde[1].Arzt.Vorname.Content.Should().Be("Peter");
        ldt.Befunde[0].Patient.Vorname.Content.Should().Be("Herbert");
        ldt.Befunde[1].Patient.Vorname.Content.Should().Be("Ann\r\nKatrin");

        ldt.IsValid().Should().BeTrue();
    }
}