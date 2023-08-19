using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LeaveRoomMenu : MonoBehaviour
{
    private RoomCanvases _roomsCanvases;
    
    public void FirstInitialize(RoomCanvases canvases)
    {
        _roomsCanvases = canvases;
    }
    
    public void OnClick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
        _roomsCanvases.CurrentRoomCanvas.Hide();
    }
}
