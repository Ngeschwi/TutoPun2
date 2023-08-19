using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoomMenu : MonoBehaviour
{
    [SerializeField]
    private Text _roomName;
    
    public void OnClick_CreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;

        if (_roomName.text.Length == 0)
            return;
        
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(_roomName.text, roomOptions, TypedLobby.Default);
    }
    
    public void OnCreatedRoom()
    {
        Debug.Log("Created room successfully.", this);
    }
    
    public void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room, " + message, this);
    }
}
