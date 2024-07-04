using XdtParser.Container;

namespace XdtParser.Interface;

internal interface IXdtElement : IRunState, IValidatable, IXdtLineConsumer, ICopyable<IXdtElement>
{
    IXdtElement? Parent { get; set; }
    Children Children { get; }
    IXdtElement WithChild(IXdtElement child);
}