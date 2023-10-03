using LOK.Core.QRCodes;
using LOK.Core.Room;
using UnityEngine;

public class RoomQRCode : MonoBehaviour, IRoomInitHandler, IRoomStartHandler, IRoomEndHandler, IRoomValidateHandler
{
    [Header("QRCode")]
    [SerializeField] private string _QRCodeID = "";

    [Header("AutoShow")]
    [SerializeField] private bool _autoShowOnStart = true;
    
    public void OnRoomInit(Room room)
    {
        if (room.IsCompleted) {
            QRCodesGlobals.Manager.ValidateQRCode(_QRCodeID);
        }
    }

    public void OnRoomStart(Room room)
    {
        QRCodesGlobals.Manager.FillWithQRCode(_QRCodeID);
        if (_autoShowOnStart) {
            ShowQRCode();
        }
    }

    public void OnRoomEnd(Room room)
    {
        HideQRCode();
    }

    public void OnRoomValidated(Room room)
    {
        ValidateQRCode();
    }

    public void ValidateQRCode()
    {
        QRCodesGlobals.Manager.ValidateQRCode(_QRCodeID);
    }

    public void ShowQRCode()
    {
        QRCodesGlobals.Manager.ShowQRCodePanel();
    }

    public void HideQRCode()
    {
        QRCodesGlobals.Manager.HideQRCodePanel();
    }

}