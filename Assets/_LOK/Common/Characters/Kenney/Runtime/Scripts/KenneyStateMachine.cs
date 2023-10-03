using IIMEngine.Movements2D;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyStateMachine : MonoBehaviour
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [Header("Movements")]
        [SerializeField] private KenneyMovementsData _movementsData;

        public KenneyMovementsData MovementsData => _movementsData;
        
        public KenneyStateIdle StateIdle { get; } = new KenneyStateIdle();
        public KenneyStateWalk StateWalk { get; } = new KenneyStateWalk();
        public KenneyStateAccelerate StateAccelerate { get; } = new KenneyStateAccelerate();
        public KenneyStateDecelerate StateDecelerate { get; } = new KenneyStateDecelerate();
        public KenneyStateTurnBackAccelerate StateTurnBackAccelerate { get; } = new KenneyStateTurnBackAccelerate();
        public KenneyStateTurnBackDecelerate StateTurnBackDecelerate { get; } = new KenneyStateTurnBackDecelerate();

        public AKenneyState[] AllStates => new AKenneyState[]
        {
            StateIdle,
            StateWalk,
            StateAccelerate,
            StateDecelerate,
            StateTurnBackAccelerate,
            StateTurnBackDecelerate,
        };

        public AKenneyState StartState => StateIdle;

        public AKenneyState PreviousState { get; private set; }
        public AKenneyState CurrentState { get; private set; }
        
        #pragma warning restore 0414
        #endregion

        private void Awake()
        {
            IMove2DSpeedMaxWriter speedMax = GetComponent<IMove2DSpeedMaxWriter>();
            speedMax.MoveSpeedMax = MovementsData.SpeedMax;
            _InitAllStates();
        }

        private void Start()
        {
            //Call ChangeState using StartState
        }

        private void Update()
        {
            //Call CurrentState StateUpdate
        }

        private void _InitAllStates()
        {
            //Call StateInit for all states
        }

        public void ChangeState(AKenneyState state)
        {
            //Call StateExit for current state (be careful, CurrentState can be null)

            //Change PreviousState to CurrentState
            //Change CurrentState using state in function parameter

            //Call StateEnter for current state (be careful, CurrentState can be null)
        }
    }
}