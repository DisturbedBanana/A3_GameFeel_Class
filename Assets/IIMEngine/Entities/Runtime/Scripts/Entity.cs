using UnityEngine;

namespace IIMEngine.Entities
{
    public class Entity : MonoBehaviour
    {
        public enum RegisterEvent
        {
            Awake = 0,
            Start,
            Enable
        }

        public enum UnregisterEvent
        {
            Destroy = 0,
            Disable,
        }

        [Header("Register")]
        [SerializeField] private bool _autoRegister = true;
        [SerializeField] private RegisterEvent _registerEvent = RegisterEvent.Enable;

        [Header("Unregister")]
        [SerializeField] private bool _autoUnregister = true;
        [SerializeField] private UnregisterEvent _unregisterEvent = UnregisterEvent.Disable;

        [Header("Unique ID")]
        [SerializeField] private bool _hasUniqueID = false;
        [SerializeField] private string _entityID = "";

        public bool HasUniqueID => _hasUniqueID;
        public string EntityID => _entityID;

        [Header("Groups")]
        [SerializeField] private bool _hasGroups = false;
        [SerializeField] private string[] _groups;

        public bool HasGroups => _hasGroups;
        public string[] Groups => _groups;

        private void Awake()
        {
            if (_autoRegister && _registerEvent == RegisterEvent.Awake) {
                EntitiesGlobal.RegisterEntity(this);
            }
        }

        private void Start()
        {
            if (_autoRegister && _registerEvent == RegisterEvent.Start) {
                EntitiesGlobal.RegisterEntity(this);
            }
        }

        private void OnEnable()
        {
            if (_autoRegister && _registerEvent == RegisterEvent.Enable) {
                EntitiesGlobal.RegisterEntity(this);
            }
        }

        private void OnDisable()
        {
            if (_autoUnregister && _unregisterEvent == UnregisterEvent.Disable) {
                EntitiesGlobal.UnRegisterEntity(this);
            }
        }

        private void OnDestroy()
        {
            if (_autoUnregister && _unregisterEvent == UnregisterEvent.Destroy) {
                EntitiesGlobal.UnRegisterEntity(this);
            }
        }
    }
}