using System;
using UnityEngine;

namespace LOK.Core.QRCodes
{
    [Serializable]
    public class QRCodeData
    {
        [SerializeField] private string _name = "";
        [SerializeField] private string _title = "";
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _URL = "";

        public string Name => _name;
        public string Title => _title;
        public Sprite Sprite => _sprite;
        public string URL => _URL;
    }
}