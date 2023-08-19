using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOrJoinRoomCanvas : MonoBehaviour
{
    [SerializeField]
    private CreateRoomMenu _createRoomMenu;
    [SerializeField]
    private RoomListingMenu _roomListingMenu;
    
    private RoomCanvases _roomsCanvases;

    public void FirstInitialize(RoomCanvases canvases)
    {
        _roomsCanvases = canvases;
        _createRoomMenu.FirstInitialize(canvases);
        _roomListingMenu.FirstInitialize(canvases);
    }
}
