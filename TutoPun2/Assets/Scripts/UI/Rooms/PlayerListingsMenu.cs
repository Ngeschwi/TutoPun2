using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;
    
    [SerializeField]
    private PlayerListing _playerListingPrefab;

    [SerializeField] 
    private Text _readyUpText;
    
    private List<PlayerListing> _listings = new List<PlayerListing>();
    private RoomCanvases _roomCanvases;
    private bool _isReady = false;
    
    private void Awake()
    {
        GetCurrentRoomPlayers();    
    }

    public override void OnEnable()
    {
        base.OnEnable();
        
        SetReadyUp(false);
    }
    
    public override void OnDisable()
    {
        base.OnDisable();
        for (int i = 0; i < _listings.Count; i++)
        {
            Destroy(_listings[i].gameObject);
        }
        _listings.Clear();
    }
    
    public void FirstInitialize(RoomCanvases canvases)
    {
        _roomCanvases = canvases;
    }
    
    private void SetReadyUp(bool state)
    {
        _isReady = state;
        if (_isReady)
        {
            _readyUpText.text = "Ready";
        }
        else
        {
            _readyUpText.text = "Not Ready";
        }
    }
    
    private void GetCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }

        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
        {
            return;
        }

        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
    }

    private void AddPlayerListing(Player player)
    {
        int index = _listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            _listings[index].SetPlayerInfo(player);
        }
        else
        {
            PlayerListing listing = Instantiate(_playerListingPrefab, _content);
            if (listing != null)
            {
                listing.SetPlayerInfo(player);
                _listings.Add(listing);
            }
        }
    }
    
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        _roomCanvases.CurrentRoomCanvas.LeaveRoomMenu.OnClick_LeaveRoom();
    }
    
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
    }
    
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }
    
    public void OnClick_StartGame()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        
        for (int i = 0; i < _listings.Count; i++)
        {
            if (_listings[i].Player != PhotonNetwork.LocalPlayer)
            {
                if (!_listings[i].Ready)
                {
                    return;
                }
            }
        }
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.LoadLevel(1);
    }

    public void OnClick_ReadyUp()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            SetReadyUp(!_isReady);
            base.photonView.RPC("RPC_ChangedReadyState", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer, _isReady);
        }
    }
    
    [PunRPC]
    private void RPC_ChangedReadyState(Player player, bool ready)
    {
        int index = _listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            _listings[index].Ready = ready;
        }
    }
}
