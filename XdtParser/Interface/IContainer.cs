namespace XdtParser.Interface;

internal interface IContainer : IRunState, IValidatable, IXdtLineConsumer, ICopyable<IContainer>
{
    List<IXdtElement> Elements { get; }
    bool GotXdtContent { get; }
}