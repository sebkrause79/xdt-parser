namespace XdtParser.Helper;

internal static class XdtListExtensions
{
    public static List<(List<XdtLine> block, bool isInBlock)> GetBlocks(this List<XdtLine> lines, string blockStart, string blockEnd)
    {
        var result = new List<(List<XdtLine>, bool)>();
        var skip = 0;
        while (skip < lines.Count)
        {
            var block = lines
                .Skip(skip)
                .TakeWhile(l => l.FieldIdentifier != blockStart)
                .ToList();
            skip += block.Count;
            if (block.Any())
            {
                result.Add((block, false));
            }
            block = lines
                .Skip(skip)
                .TakeWhile(l => l.FieldIdentifier != blockEnd)
                .Take(1)
                .ToList();
            skip += block.Count;
            if (block.Any())
            {
                result.Add((block, true));
            }
        }

        return result;
    }
}