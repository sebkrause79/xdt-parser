using XdtParser.RawContainer;

namespace XdtParser.Helper;

internal static class XdtLineExtensions
{
    public static List<(List<XdtLine> block, bool isInBlock)> GetBlocks(this List<XdtLine> lines, string blockStart, string blockEnd)
    {
        var result = new List<(List<XdtLine>, bool)>();
        var queue = new Queue<XdtLine>(lines);
        var block = new List<XdtLine>();
        var isInBlock = false;
        while (queue.Count > 0)
        {
            var line = queue.Dequeue();
            if (line.FieldIdentifier == blockStart)
            {
                if (isInBlock)
                {
                    throw new ArgumentException("Sentences may not be nested");
                }

                if (block.Any())
                {
                    result.Add((block, false));
                    block = new();
                }

                isInBlock = true;
                block.Add(line);
            }
            else if (line.FieldIdentifier == blockEnd)
            {
                if (!isInBlock)
                {
                    throw new ArgumentException("Sentences may not be nested");
                }

                block.Add(line);
                result.Add((block, true));
                block = new();

                isInBlock = false;
            }
            else
            {
                block.Add(line);
            }
        }

        return result;
    }
}