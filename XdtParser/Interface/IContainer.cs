namespace XdtParser.Interface;

public interface IContainer : IRunState, IValidatable, IXdtLineConsumer, ICopyable<IContainer>, ITreeView
{
    List<IXdtElement> Elements { get; }
    bool GotXdtContent { get; }
}