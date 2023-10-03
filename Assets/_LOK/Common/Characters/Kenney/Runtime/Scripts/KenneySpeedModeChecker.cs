using System;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneySpeedModeChecker : MonoBehaviour
    {
        [Header("Movements")]
        [SerializeField] private KenneyMovementsData _movementsData;

        private KenneyStateMachine _kenneyStateMachine;
        private KenneySimpleMovement _kenneySimpleMovement;

        private void Awake()
        {
            _kenneyStateMachine = GetComponent<KenneyStateMachine>();
            _kenneySimpleMovement = GetComponent<KenneySimpleMovement>();
            _ChangeBehaviourFromSpeedMode();
        }

        private void Update()
        {
            _ChangeBehaviourFromSpeedMode();
        }

        private void _ChangeBehaviourFromSpeedMode()
        {
            switch (_movementsData.SpeedMode) {
                case KenneySpeedMode.ConstantSpeed:
                    if (!_kenneySimpleMovement.enabled) {
                        _kenneySimpleMovement.enabled = true;
                    }

                    _kenneyStateMachine.enabled = false;
                    break;

                case KenneySpeedMode.DynamicSpeed:
                    if (!_kenneyStateMachine.enabled) {
                        _kenneyStateMachine.enabled = true;
                        _kenneyStateMachine.ChangeState(_kenneyStateMachine.StateIdle);
                    }

                    _kenneySimpleMovement.enabled = false;
                    break;
            }
        }
    }
}