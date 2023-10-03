using LOK.Core.Items;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyLastGainItemSpriteUpdater : MonoBehaviour
    {
        [Header("Entity")]
        [SerializeField] private GameObject _entityRoot;

        [Header("Gain Item Sprite")]
        [SerializeField] private SpriteRenderer _gainItemSpriteRenderer = null;
        [SerializeField] private ItemsSpriteMappingData _itemsSpriteMappingData;

        private IItemAddListener _itemAddListener;

        private void Awake()
        {
            _itemAddListener = _entityRoot.GetComponent<IItemAddListener>();
        }

        private void OnEnable()
        {
            _itemAddListener.OnItemAdd += _OnItemAdd;
        }

        private void OnDisable()
        {
            _itemAddListener.OnItemAdd -= _OnItemAdd;
        }

        private void _OnItemAdd(ItemID itemID)
        {
            _gainItemSpriteRenderer.sprite = _itemsSpriteMappingData.GetSpriteForItemID(itemID);
        }
    }
}