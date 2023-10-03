using System.Collections;
using IIMEngine.Save;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LOK.Core.Debug
{
    public class UIDebugSaveDataElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _keyTextLabel;
        [SerializeField] private TextMeshProUGUI _valueTextLabel;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Button _deleteButton;

        [Header("Highlight")]
        [SerializeField] private Color _highlightColor = Color.green;
        [SerializeField] private float _highlightDuration = 1f;

        private Color _valueTextLabelColorStart = Color.white;
        
        public SaveKey SaveKey { get; set; } = null;
        
        public Image BackgroundImage => _backgroundImage;

        public TextMeshProUGUI KeyTextLabel => _keyTextLabel;
        
        public TextMeshProUGUI ValueTextLabel => _valueTextLabel;

        public Button DeleteButton => _deleteButton;

        private void Awake()
        {
            _valueTextLabelColorStart = _valueTextLabel.color;
        }

        public void HighlightValueTextLabel()
        {
            StartCoroutine(_CoroutineHighlightValueTextLabel());
        }

        private IEnumerator _CoroutineHighlightValueTextLabel()
        {
            Color startColor = _highlightColor;
            Color endColor = _valueTextLabelColorStart;
            float timer = 0f;
            while (timer < _highlightDuration) {
                yield return null;
                timer += Time.unscaledDeltaTime;
                float ratio = timer / _highlightDuration;
                _valueTextLabel.color = Color.Lerp(startColor, endColor, ratio);
                
            }
            _valueTextLabel.color = endColor;
        }
    }
}