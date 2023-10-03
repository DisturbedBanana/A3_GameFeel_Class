using UnityEngine;
using UnityEngine.EventSystems;

namespace LOK.Core.QRCodes
{
    public class QRCodeClickArea : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Texture2D _cursorPointerTexture = null;

        private Vector2 _cursorOffset = Vector2.zero;
        
        public string URL { get; set; }

        private void Awake()
        {
            _cursorOffset.x = _cursorPointerTexture.width / 2f;
            _cursorOffset.y = _cursorPointerTexture.height / 2f;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (string.IsNullOrEmpty(URL)) return;
            Application.OpenURL(URL);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Cursor.SetCursor(_cursorPointerTexture, _cursorOffset, CursorMode.Auto);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        private void OnDisable()
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}