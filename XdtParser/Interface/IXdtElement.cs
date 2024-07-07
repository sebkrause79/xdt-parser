using XdtParser.ParsedContainer;

namespace XdtParser.Interface;

public interface IXdtElement : IRunState, IValidatable, IXdtLineConsumer, ICopyable<IXdtElement>, ITreeView
{
    IXdtElement? Parent { get; set; }
    Children Children { get; }
    IXdtElement WithChild(IXdtElement child);
}