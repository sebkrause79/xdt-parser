namespace XdtParser.Interface;

internal interface IContainer : IRunState, IValidatable, IXdtLineConsumer
{
    List<IXdtElement> Elements { get; }
}