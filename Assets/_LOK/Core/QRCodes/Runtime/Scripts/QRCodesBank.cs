using UnityEngine;

namespace LOK.Core.QRCodes
{
    [CreateAssetMenu(fileName = "QRCodesBank", menuName= "LOK/QRCodes/QRCodesBank")]
    public class QRCodesBank : ScriptableObject
    {
        [SerializeField] private QRCodeData[] _datas;

        public QRCodeData[] Datas => _datas;
    }
}