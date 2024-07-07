using XdtParser.Enums;

namespace XdtParser.Interface;

public interface IRunState
{
    ContainerState ContainerState { get; set; }
}