using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace LOK.Core.Items
{
    public class ItemsPossessor : MonoBehaviour,
        IItemListReader, IItemsListWriter,
        IItemAddListener, IItemAddDispatcher
    {
        [SerializeField] private List<ItemID> _items;

        ReadOnlyCollection<ItemID> IItemListReader.Items => _items.AsReadOnly();

        List<ItemID> IItemsListWriter.Items => _items;
        
        private Action<ItemID> _onItemAdd;

        event Action<ItemID> IItemAddListener.OnItemAdd {
            add => _onItemAdd += value;
            remove => _onItemAdd -= value;
        }

        Action<ItemID> IItemAddDispatcher.OnItemAdd => _onItemAdd;
    }
}