using System;

namespace LOK.Core.Items
{
    public interface IItemAddDispatcher
    {
        Action<ItemID> OnItemAdd { get; }
    }
}