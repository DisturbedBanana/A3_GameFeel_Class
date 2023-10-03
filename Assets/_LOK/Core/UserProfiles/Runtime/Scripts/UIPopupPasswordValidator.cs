using System;
using IIMEngine.Save;
using LOK.Passwords;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LOK.Core.UserProfiles
{
    public class UIPopupPasswordValidator : MonoBehaviour
    {
        public static UIPopupPasswordValidator Instance { get; private set; }

        public event Action OnOpen;
        public event Action OnClose;
        public event Action OnPasswordValidated;

        [Header("Datas")]
        [SerializeField] private TextAsset _passwordsTextAsset;
        private UserProfileData[] _passwordDatas;

        [Header("Visuals")]
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _buttonOK;
        [SerializeField] private Button _buttonClose;
        [SerializeField] private TMP_InputField _inputFieldPassword;
        [SerializeField] private GameObject _invalidBorderGameObject;

        public string CurrentUser { get; private set; }
        public string CurrentValidatorID { get; private set; }

        public bool IsOpened { get; private set; } = false;

        private void Awake()
        {
            Instance = this;
            _Init();
        }

        private void Update()
        {
            if (!IsOpened) return;
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Close();
            }
        }

        private void _Init()
        {
            _passwordDatas = UserProfilesCSVParser.Parse(_passwordsTextAsset);
            _inputFieldPassword.onValidateInput += _OnInputFieldPasswordValidate;
            _inputFieldPassword.onSubmit.AddListener(_OnInputFieldPasswordSubmit);
            _buttonOK.onClick.AddListener(_OnButtonOKClicked);
            _buttonClose.onClick.AddListener(_OnButtonCloseClicked);

            _canvas.gameObject.SetActive(false);
        }

        public void Open(string validatorID)
        {
            _invalidBorderGameObject.SetActive(false);

            CurrentUser = SaveSystem.ReadGlobalString(SaveKeys.USERNAME);
            CurrentValidatorID = validatorID;
            _canvas.gameObject.SetActive(true);

            _inputFieldPassword.text = "";
            _inputFieldPassword.Select();

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
            if (_IsPasswordValid(_inputFieldPassword.text)) {
                OnPasswordValidated?.Invoke();
                Close();
            } else {
                _invalidBorderGameObject.SetActive(true);
            }
        }

        private char _OnInputFieldPasswordValidate(string text, int charindex, char addedchar)
        {
            char c = addedchar;
            if (c >= 'A' && c <= 'Z') {
                return c;
            }

            if (c >= '0' && c <= '9') {
                return c;
            }

            if (c >= 'a' && c <= 'z') {
                return char.ToUpper(c);
            }

            return '\0';
        }

        private void _OnInputFieldPasswordSubmit(string password)
        {
            if (_IsPasswordValid(password)) {
                OnPasswordValidated?.Invoke();
                Close();
            } else {
                _invalidBorderGameObject.SetActive(true);
            }
        }

        private void _OnButtonCloseClicked()
        {
            Close();
        }

        private bool _IsPasswordValid(string password)
        {
            UserProfileData userProfileData = _GetUserPasswordData(CurrentUser);
            if (userProfileData == null) return false;

            string cryptPassword = PasswordCrypter.CryptPassword(password);
            string validPassword = userProfileData.GetPassword(CurrentValidatorID);

            return cryptPassword == validPassword;
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