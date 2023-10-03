using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using IIMEngine.Save;
using LOK.Core.Room;
using UnityEngine;
using UnityEngine.UI;

namespace LOK.Core.Debug
{
    public class UIDebugSaveData : MonoBehaviour
    {
        [SerializeField] private Canvas _canvasRoot;
        [SerializeField] private UIDebugSaveDataElement _elementTemplate;
        [SerializeField] private Transform _elementsParent;
        [SerializeField] private Color _elementColorOdd = Color.white;
        [SerializeField] private Color _elementColorEven = Color.black;
        [SerializeField] private Button _resetRoomButton = null;
        [SerializeField] private Button _deleteAllButton = null;

        private UIDebugSaveDataElement[] _globalElements = Array.Empty<UIDebugSaveDataElement>();
        private UIDebugSaveDataElement[] _currentRoomSaveElements = Array.Empty<UIDebugSaveDataElement>();

        private ContentSizeFitter[] _contentFitters = null;

        private Room.Room _currentRoom;

        private void Awake()
        {
            _resetRoomButton.onClick.AddListener(_OnResetRoomButtonClick);
            _deleteAllButton.onClick.AddListener(_OnDeleteButtonClick);
            _contentFitters = _FindContentFitters();
            _CreateGlobalSaveDataElements(SaveKeyUtils.GetGlobalSaveKeys());
        }

        private void _OnResetRoomButtonClick()
        {
            RoomSaveSystem.DeleteRoomCompleted(_currentRoom);
        }

        private void OnDestroy()
        {
            _UnbindEvents();
        }

        private void Start()
        {
            _HideSaveDataElementTemplate();
            _HideCanvas();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F6)) {
                _ToggleCanvasVisibility();
                if (_CanvasVisible) {
                    _UpdateGlobalSaveDataElements();
                    _currentRoom = RoomsManager.Instance.CurrentRoom;
                    _CreateRoomSaveDataElements(FindRoomSaveKeys(_currentRoom.gameObject));
                    _BindEvents();
                    _RefreshContentFitters();
                } else {
                    _DestroyRoomSaveDateElements();
                    _UnbindEvents();
                }
            }
        }

        public void _BindEvents()
        {
            SaveEvents.OnKeyChanged += OnSaveKeyChanged;
            RoomEvents.OnRoomStart += _OnRoomStart;
            RoomEvents.OnRoomEnd += _OnRoomEnd;
        }

        public void _UnbindEvents()
        {
            SaveEvents.OnKeyChanged -= OnSaveKeyChanged;
            RoomEvents.OnRoomStart -= _OnRoomStart;
            RoomEvents.OnRoomEnd -= _OnRoomEnd;
        }

        private void _OnRoomStart(Room.Room room)
        {
            _currentRoom = room;
            _CreateRoomSaveDataElements(FindRoomSaveKeys(room.gameObject));
            _RefreshContentFitters();
        }

        private void _OnRoomEnd(Room.Room room)
        {
            _DestroyRoomSaveDateElements();
            _RefreshContentFitters();
        }

        #region Functions Delete Button Handlers

        private void _OnDeleteButtonClick()
        {
            SaveSystem.DeleteAll();
            _UpdateGlobalSaveDataElements();
            _DestroyRoomSaveDateElements();
            _CreateRoomSaveDataElements(FindRoomSaveKeys(RoomsManager.Instance.CurrentRoom.gameObject));
        }

        #endregion

        #region Functions Save Keys

        private void OnSaveKeyChanged(string keyName)
        {
            UIDebugSaveDataElement element = _FindSaveDataElement(keyName);
            if (element != null) {
                element.ValueTextLabel.text = _GetSaveKeyStringValue(element.SaveKey);
                element.HighlightValueTextLabel();
            }
        }

        private string _GetSaveKeyStringValue(SaveKey saveKey)
        {
            switch (saveKey.KeyType) {
                case SaveKeyType.String: return SaveSystem.ReadGlobalString(saveKey.KeyName);
                case SaveKeyType.Int: return SaveSystem.ReadGlobalInt(saveKey.KeyName).ToString();
                case SaveKeyType.Float: return SaveSystem.ReadGlobalFloat(saveKey.KeyName).ToString();
                case SaveKeyType.Bool: return SaveSystem.ReadGlobalBool(saveKey.KeyName).ToString();
            }

            return string.Empty;
        }

        #endregion

        #region Function Canvas

        private bool _CanvasVisible => _canvasRoot.gameObject.activeSelf;

        private void _ShowCanvas()
        {
            _canvasRoot.gameObject.SetActive(true);
        }

        private void _HideCanvas()
        {
            _canvasRoot.gameObject.SetActive(false);
        }

        private void _ToggleCanvasVisibility()
        {
            _canvasRoot.gameObject.SetActive(!_canvasRoot.gameObject.activeSelf);
        }

        #endregion

        #region Functions Save Data Elements

        private void _HideSaveDataElementTemplate()
        {
            _elementTemplate.gameObject.SetActive(false);
        }

        private void _CreateGlobalSaveDataElements(SaveKey[] saveKeys)
        {
            _globalElements = new UIDebugSaveDataElement[saveKeys.Length];
            for (int i = 0; i < saveKeys.Length; ++i) {
                SaveKey saveKey = saveKeys[i];
                Color color = _GetElementColorFromIndex(i);
                UIDebugSaveDataElement element = _CreateSaveDataElement(saveKey, color);
                _globalElements[i] = element;
            }
        }

        private void _UpdateGlobalSaveDataElements()
        {
            foreach (UIDebugSaveDataElement element in _globalElements) {
                element.ValueTextLabel.text = _GetSaveKeyStringValue(element.SaveKey);
            }
        }

        private void _CreateRoomSaveDataElements(SaveKey[] saveKeys)
        {
            _currentRoomSaveElements = new UIDebugSaveDataElement[saveKeys.Length];
            for (int i = 0; i < saveKeys.Length; ++i) {
                SaveKey saveKey = saveKeys[i];
                Color color = _GetElementColorFromIndex(_globalElements.Length + i);
                UIDebugSaveDataElement element = _CreateSaveDataElement(saveKey, color);
                _currentRoomSaveElements[i] = element;
            }
        }

        public void _DestroyRoomSaveDateElements()
        {
            for (int i = _currentRoomSaveElements.Length; i-- > 0;) {
                UIDebugSaveDataElement element = _currentRoomSaveElements[i];
                Destroy(element.gameObject);
            }

            _currentRoomSaveElements = Array.Empty<UIDebugSaveDataElement>();
        }

        private UIDebugSaveDataElement _CreateSaveDataElement(SaveKey saveKey, Color color)
        {
            UIDebugSaveDataElement element = Instantiate(_elementTemplate, _elementsParent);
            element.SaveKey = saveKey;
            element.BackgroundImage.color = color;
            element.KeyTextLabel.text = saveKey.KeyName;
            element.ValueTextLabel.text = _GetSaveKeyStringValue(saveKey);
            element.DeleteButton.onClick.AddListener(() => { _OnDeleteElement(saveKey.KeyName); });
            element.gameObject.SetActive(true);
            return element;
        }

        private UIDebugSaveDataElement _FindSaveDataElement(string saveKeyName)
        {
            foreach (UIDebugSaveDataElement element in _globalElements) {
                if (element.SaveKey.KeyName == saveKeyName) return element;
            }

            foreach (UIDebugSaveDataElement element in _currentRoomSaveElements) {
                if (element.SaveKey.KeyName == saveKeyName) return element;
            }

            return null;
        }


        private Color _GetElementColorFromIndex(int index)
        {
            return index % 2 == 0 ? _elementColorOdd : _elementColorEven;
        }

        private void _OnDeleteElement(string saveKeyName)
        {
            SaveSystem.DeleteGlobalKey(saveKeyName);
        }

        #endregion

        #region Functions Content Fitters

        private ContentSizeFitter[] _FindContentFitters()
        {
            return _canvasRoot.GetComponentsInChildren<ContentSizeFitter>(true);
        }

        private void _RefreshContentFitters()
        {
            StartCoroutine(_CoroutineRefreshContentFitters());
        }

        private IEnumerator _CoroutineRefreshContentFitters()
        {
            yield return new WaitForEndOfFrame();
            for (int i = _contentFitters.Length; i-- > 0;) {
                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_contentFitters[i].transform);
            }
        }

        #endregion

        #region Room Save Utils

        public static SaveKey[] FindRoomSaveKeys(GameObject roomGameObject)
        {
            List<SaveKey> resultList = new List<SaveKey>();
            foreach (Type type in _FindAllTypesInsideRoom(roomGameObject)) {
                foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)) {
                    RoomSaveKeyAttribute attribute = fieldInfo.GetCustomAttribute<RoomSaveKeyAttribute>(true);
                    if (attribute != null) {
                        string saveKeyName = RoomSaveSystem.GenerateRoomSaveKey(roomGameObject.name, fieldInfo.GetValue(null) as string);
                        SaveKey saveKey = new SaveKey(attribute.KeyType, saveKeyName);
                        resultList.Add(saveKey);
                    }
                }
            }

            return resultList.ToArray();
        }

        private static Type[] _FindAllTypesInsideRoom(GameObject roomGameObject)
        {
            List<Type> resultList = new List<Type>();
            Component[] allRoomsComponents = roomGameObject.GetComponentsInChildren<Component>(true);
            foreach (Component component in allRoomsComponents) {
                Type type = component.GetType();
                if (!resultList.Contains(type)) {
                    resultList.Add(type);
                }
            }

            return resultList.ToArray();
        }

        #endregion
    }
}