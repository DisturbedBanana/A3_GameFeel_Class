using System;

namespace LOK.Core.Items
{
    public interface IItemAddListener
    {
        event Action<ItemID> OnItemAdd;
    }
}