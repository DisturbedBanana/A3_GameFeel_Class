using IIMEngine.Movements2D;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneySimpleMovement : MonoBehaviour
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [Header("Movements")]
        [SerializeField] private KenneyMovementsData _movementsData;
        
        #pragma warning restore 0414
        #endregion

        private void Awake()
        {
            //Find Movable Interfaces (at the root of this gameObject)
            //You will need to :
            // - Check if movements are locked
            // - Read Move Dir
            // - Write Move Orient
            // - Write Move Speed
        }

        private void Update()
        {
            //If Movements are locked
            //Force MoveSpeed to 0

            //If there is MoveDir
                //Set MoveSpeed to MovementsData.SpeedMax
                //Set Move OrientDir to Movedir
            //Else
                //Set MoveSpeed to 0
        }
    }
}