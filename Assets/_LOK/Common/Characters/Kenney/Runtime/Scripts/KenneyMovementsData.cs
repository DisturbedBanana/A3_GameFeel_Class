using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public enum KenneySpeedMode
    {
        ConstantSpeed = 0,
        DynamicSpeed,
    }
    
    [CreateAssetMenu(fileName = "Kenney_Movement_Data", menuName= "LOK/Entities/Kenney/MovementsData")]
    public class KenneyMovementsData : ScriptableObject
    {
        [Header("Speed Mode")]
        [SerializeField] private KenneySpeedMode _speedMode = KenneySpeedMode.ConstantSpeed;

        public KenneySpeedMode SpeedMode {
            get => _speedMode;
            set => _speedMode = value;
        }

        //Constant Speed Settings
        [Header("Constant Speed")]
        [SerializeField] private float _speed = 5f;
        public float Speed => _speed;
        
        //Dynamic Speed Settings
        [Header("Speed Max")]
        [SerializeField] private float _speedMax = 5f;

        public float SpeedMax => _speedMax;
        
        //Start Acceleration
        [Header("Start Acceleration")]
        [SerializeField] private float _startAccelerationDuration = 1f;

        public float StartAccelerationDuration => _startAccelerationDuration;
        
        //Stop Deceleration
        [Header("Stop Deceleration")]
        [SerializeField] private float _stopDecelerationDuration = 1f;

        public float StopDecelerationDuration => _stopDecelerationDuration;
        
        //Turn Back
        [Header("Turn Back")]
        [SerializeField] private float _turnBackAngleThreshold = 120f;
        [SerializeField] private float _turnBackDecelerationDuration = 1f;
        [SerializeField] private float _turnBackAccelerationDuration = 1f;

        public float TurnBackAngleThreshold => _turnBackAngleThreshold;
        public float TurnBackDecelerationDuration => _turnBackDecelerationDuration;
        public float TurnBackAccelerationDuration => _turnBackAccelerationDuration;
    }
}