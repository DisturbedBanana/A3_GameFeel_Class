using UnityEngine;

namespace LOK.Core.Interactions
{
    public interface IInteractable
    {
        void Interact();
        
        Vector3 Position { get; }
    }
}