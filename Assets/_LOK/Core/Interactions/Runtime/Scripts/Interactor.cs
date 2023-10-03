using System.Collections.Generic;
using UnityEngine;

namespace LOK.Core.Interactions
{
    public class Interactor : MonoBehaviour
    {
        public bool CanInteract { get; set; } = true;
        
        public List<IInteractable> InteractablesNearBy { get; private set; } = new List<IInteractable>();

        public bool HasInteractablesNearBy => InteractablesNearBy.Count > 0;
        
        public IInteractable FindClosestInteractableNearBy(Vector3 position)
        {
            IInteractable closestInteractable = null;
            float closestDist = Mathf.Infinity;
            foreach (IInteractable interactable in InteractablesNearBy) {
                float dist = (position - interactable.Position).sqrMagnitude;
                if (dist < closestDist) {
                    closestDist = dist;
                    closestInteractable = interactable;
                }
            }
            return closestInteractable;
        }
    }
}