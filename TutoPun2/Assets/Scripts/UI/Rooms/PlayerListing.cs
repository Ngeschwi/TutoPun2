using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class PlayerListing : MonoBehaviour
{
    [SerializeField]
    private Text _text;
    
    public Player Player { get; private set; }
    
    public void SetPlayerInfo(Player player)
    {
        Player = player;
        
        int random = player.CustomProperties["RandomNumber"] != null ? (int)player.CustomProperties["RandomNumber"] : 0;
        
        _text.text = random.ToString() + " , " + player.NickName;
        
    }
}
