using XdtParser.Container;

namespace XdtParser.Interface;

internal interface IXdtElement : IRunState, IValidatable, IXdtLineConsumer, ICopyable<IXdtElement>, ITreeView
{
    IXdtElement? Parent { get; set; }
    Children Children { get; }
    IXdtElement WithChild(IXdtElement child);
}