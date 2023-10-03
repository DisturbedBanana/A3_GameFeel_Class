using IIMEngine.Entities.Target;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace LOK.Core.Items.Feel
{
    [AddComponentMenu("")]
    [FeedbackPath("Items/Items Transfer")]
    public class MMF_Items_Transfer : MMF_Feedback
    {
        [MMFInspectorGroup("Target Source", true)]
        [SerializeField] private EntityTarget _targetSource;
        private IItemsListWriter _itemsListWriterSource;
        
        [MMFInspectorGroup("Target Destination", true)]
        [SerializeField] private EntityTarget _targetDestination;
        private IItemsListWriter _itemsListWriterDestination;
        private IItemAddDispatcher _itemAddDispatcherDestination;

        protected override void CustomInitialization(MMF_Player owner)
        {
            _itemsListWriterSource = _targetSource.FindFirstResult<IItemsListWriter>();
            _itemsListWriterDestination = _targetDestination.FindFirstResult<IItemsListWriter>();
            _itemAddDispatcherDestination = _targetDestination.FindFirstResult<IItemAddDispatcher>();
        }

        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            foreach (ItemID itemID in _itemsListWriterSource.Items) {
                _itemsListWriterDestination.Items.Add(itemID);
                _itemAddDispatcherDestination.OnItemAdd?.Invoke(itemID);
            }
            _itemsListWriterSource.Items.Clear();
        }
    }
}