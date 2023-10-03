using System;
using UnityEngine;

namespace LOK.Core.Items
{
    [CreateAssetMenu(fileName = "ItemsSpriteMapping", menuName= "LOK/Items/ItemsSpriteMapping")]
    public class ItemsSpriteMappingData : ScriptableObject
    {
        [Serializable]
        public class SpriteMapping
        {
            [SerializeField] private ItemID _itemID = ItemID.Undefined;
            [SerializeField] private Sprite _sprite;

            public ItemID ItemID => _itemID;
            public Sprite Sprite => _sprite;
        }

        [SerializeField] private SpriteMapping[] _allMappings;
        
        public Sprite GetSpriteForItemID(ItemID itemID)
        {
            foreach (SpriteMapping spriteMapping in _allMappings) {
                if (spriteMapping.ItemID == itemID) {
                    return spriteMapping.Sprite;
                }
            }
            return null;
        }
    }
}