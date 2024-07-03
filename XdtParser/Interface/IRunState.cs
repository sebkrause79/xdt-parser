using XdtParser.Enums;

namespace XdtParser.Interface;

internal interface IRunState
{
    ContainerState ContainerState { get; set; }
}