using LdtParser.File;

namespace LdtParser.Parser;

internal class Container : IContainer
{
    private List<IContainer> _items;
    private IContainer? _lastItem;
    public bool IsStarted { get; set; } = false;

    public bool IsFinished { get; set; } = false;

    internal Container(List<IContainer> items) 
    {
        _items = items;
    }

    public bool TryTake(LdtLine ldtLine)
    {
        IsStarted = true;

        foreach(var item in _items)
        {
            if (item.TryTake(ldtLine)) 
            {
                if (item != _lastItem) 
                {
                    if (_lastItem is not null) 
                    {
                        _lastItem.IsFinished = true;
                    }

                    _lastItem = item;
                }
                return true;
            }
        }
        if (_lastItem is not null) 
        {
            _lastItem.IsFinished = true;
        }
        
        IsFinished = true;
        return false;
    }
}