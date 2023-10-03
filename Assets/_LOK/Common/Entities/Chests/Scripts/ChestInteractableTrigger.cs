using LOK.Core.Interactions;
using UnityEngine;

namespace LOK.Common.Chest
{
    public class ChestInteractableTrigger : MonoBehaviour, IInteractable
    {
        [Header("Entity")]
        [SerializeField] private ChestEntity _chestEntity;

        [Header("Trigger")]
        [SerializeField] private Collider2D _trigger;

        public Vector3 Position => transform.position;

        private void OnEnable()
        {
            _UpdateChestTrigger(_chestEntity);
        }

        public void Interact()
        {
            if (_chestEntity.IsOpened) return;
            
            _chestEntity.Open();
            _UpdateChestTrigger(_chestEntity);
        }

        private void _UpdateChestTrigger(ChestEntity chestEntity)
        {
            _trigger.enabled = !chestEntity.IsOpened;
        }
    }
}