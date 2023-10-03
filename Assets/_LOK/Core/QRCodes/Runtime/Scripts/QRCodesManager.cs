using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LOK.Core.QRCodes
{
    public class QRCodesManager : MonoBehaviour
    {
        [Header("Bank")]
        [SerializeField] private QRCodesBank _bank;

        [Header("Visuals")]
        [SerializeField] private Canvas _canvas = null;
        [SerializeField] private Image _QRCodeImageRenderer = null;
        [SerializeField] private QRCodeClickArea _QRCodeClickArea = null;
        [SerializeField] private TextMeshProUGUI _titleTextLabel = null;
        [SerializeField] private Image _greenTickImageRenderer = null;

        private List<string> _QRCodesValidatedNames = new List<string>();

        private string _currentQRCodeName = "";


        private void Awake()
        {
            QRCodesGlobals.Manager = this;
            _canvas.gameObject.SetActive(false);
            _greenTickImageRenderer.enabled = false;
        }

        public void FillWithQRCode(string QRCodeName)
        {
            QRCodeData data = _FindQRCodeData(QRCodeName);
            if (data == null) return;

            _currentQRCodeName = QRCodeName;

            _QRCodeImageRenderer.sprite = data.Sprite;
            _QRCodeClickArea.URL = data.URL;
            _titleTextLabel.text = data.Title;

            _greenTickImageRenderer.enabled = _QRCodesValidatedNames.Contains(QRCodeName);
        }

        public void ValidateQRCode(string QRCodeName)
        {
            if (!_QRCodesValidatedNames.Contains(QRCodeName)) {
                _QRCodesValidatedNames.Add(QRCodeName);
            }

            if (QRCodeName == _currentQRCodeName) {
                _greenTickImageRenderer.enabled = true;
            }
        }

        public void ShowQRCodePanel()
        {
            _canvas.gameObject.SetActive(true);
        }

        public void HideQRCodePanel()
        {
            _canvas.gameObject.SetActive(false);
            _greenTickImageRenderer.enabled = false;
        }

        private QRCodeData _FindQRCodeData(string name)
        {
            foreach (QRCodeData data in _bank.Datas) {
                if (data.Name == name) return data;
            }

            return null;
        }
    }
}