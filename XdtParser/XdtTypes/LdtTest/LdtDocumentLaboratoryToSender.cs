﻿using XdtParser.Container;

namespace XdtParser.XdtTypes.LdtTest;

internal class LdtDocumentLaboratoryToSender : LdtDocument
{
    public List<Sentence8205> Befunde => ((Sentence8205[])_children.ToArray()[1..^1]).ToList();

    public LdtDocumentLaboratoryToSender(List<XdtLine> lines) : base(lines)
    {
    }

    public override bool IsValid()
    {
        var sentences = _children.Select(x => x.Index).ToArray();
        if (sentences.Length < 3)
        {
            return false;
        }

        if (sentences.First() != "8220" || sentences.Last() != "8221")
        {
            return false;
        }

        if (sentences[1..^1].Any(s => s != "8205"))
        {
            return false;
        }

        return base.IsValid();
    }
}