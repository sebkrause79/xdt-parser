using LdtParser.File;

namespace LdtParser.Parser;

internal interface IContainer
{
    bool TryTake(LdtLine ldtLine);
    bool IsStarted { get; }
    bool IsFinished { get; set; }
}