using System.Collections.Generic;

namespace LOK.Core.Items
{
    public interface IItemsListWriter
    {
        List<ItemID> Items { get; }
    }
}