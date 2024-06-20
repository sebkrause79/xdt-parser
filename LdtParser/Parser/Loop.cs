using LdtParser.File;

namespace LdtParser.Parser;

internal class Loop : IContainer
{
    private Func<IContainer> _itemFactory;
    private List<IContainer> _items = new();

    public bool IsStarted {get; private set; } = false;

    public bool IsFinished { get; set; } = false;

    internal Loop(Func<IContainer> itemFactory)
    {
        _itemFactory = itemFactory;
    }

    public bool TryTake(LdtLine ldtLine)
    {
        var firstRun = !_items.Any();
        if (firstRun || _items.Last().IsFinished)
        {
            _items.Add(_itemFactory());
        }
        var item = _items.Last();
        if (item.TryTake(ldtLine)) 
        {
            IsStarted = true;
            return true;
        }

        item.IsFinished = true;
        if (firstRun)
        {
            _items.Clear();
        }
        
        IsFinished = true;
        return false;
    }
}