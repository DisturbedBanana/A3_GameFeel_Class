using System;
using IIMEngine.Save;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LOK.Core.UserProfiles
{
    public class UIPopupPasswordEnterUserName : MonoBehaviour
    {
        public static UIPopupPasswordEnterUserName Instance { get; private set; }

        event Action OnOpen;
        event Action OnClose;
        private event Action<string> OnUserNameValidated;

        [Header("Visuals")]
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _buttonOK;
        [SerializeField] private TMP_InputField _inputFieldUserName;
        [SerializeField] private GameObject _invalidBorderGameObject;

        [Header("Datas")]
        [SerializeField] private TextAsset _passwordsTextAsset;
        private UserProfileData[] _passwordDatas;

        public bool IsOpened { get; private set; } = false;

        private void Awake()
        {
            Instance = this;
            _Init();
        }

        private void _Init()
        {
            _passwordDatas = UserProfilesCSVParser.Parse(_passwordsTextAsset);

            _canvas.gameObject.SetActive(false);

            _inputFieldUserName.onValidateInput += _OnInputFieldUserNameValidate;
            _inputFieldUserName.onSubmit.AddListener(_OnInputFieldUserNameSubmit);
            _buttonOK.onClick.AddListener(_OnButtonOKClicked);
        }

        private void Start()
        {
            string userName = SaveSystem.ReadGlobalString(SaveKeys.USERNAME);
            if (string.IsNullOrEmpty(userName)) {
                Open();
            }
        }

        public void Open()
        {
            _invalidBorderGameObject.SetActive(false);

            _canvas.gameObject.SetActive(true);

            _inputFieldUserName.text = "";
            _inputFieldUserName.Select();

            IsOpened = true;
            OnOpen?.Invoke();
        }

        public void Close()
        {
            _canvas.gameObject.SetActive(false);
            IsOpened = false;
            OnClose?.Invoke();
        }

        private void _OnButtonOKClicked()
        {
            string simplifiedUserName = UserProfilesUtils.SimplifyUserName(_inputFieldUserName.text);
            if (IsUserNameValid(simplifiedUserName)) {
                SaveSystem.WriteGlobalString(SaveKeys.USERNAME, simplifiedUserName);
                OnUserNameValidated?.Invoke(simplifiedUserName);
                Close();
            } else {
                _invalidBorderGameObject.SetActive(true);
            }
        }

        private char _OnInputFieldUserNameValidate(string text, int charindex, char addedchar)
        {
            return addedchar;
        }

        private void _OnInputFieldUserNameSubmit(string userName)
        {
            string simplifiedUserName = UserProfilesUtils.SimplifyUserName(userName);
            if (IsUserNameValid(simplifiedUserName)) {
                SaveSystem.WriteGlobalString(SaveKeys.USERNAME, simplifiedUserName);
                Close();
            } else {
                _invalidBorderGameObject.SetActive(true);
            }
        }


        private bool IsUserNameValid(string userName)
        {
            UserProfileData userProfileData = _GetUserPasswordData(userName);
            return userProfileData != null;
        }

        private UserProfileData _GetUserPasswordData(string username)
        {
            foreach (UserProfileData passwordData in _passwordDatas) {
                if (passwordData.UserName == username) {
                    return passwordData;
                }
            }

            return null;
        }
    }
}