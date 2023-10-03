using System.Collections.ObjectModel;

namespace LOK.Core.Items
{
    public interface IItemListReader
    {
        ReadOnlyCollection<ItemID> Items { get; }
    }
}