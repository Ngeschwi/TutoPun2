using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class PlayerListing : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text _text;
    
    public Player Player { get; private set; }
    public bool Ready = false;
    
    public void SetPlayerInfo(Player player)
    {
        Player = player;
        
        SetPlayerText(player);
    }
    
    public override void OnPlayerPropertiesUpdate(Player target, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (target != null && target == Player)
        {
            if (changedProps.ContainsKey("RandomNumber"))
            {
                SetPlayerText(target);
            }
        }
    }

    private void SetPlayerText(Player player)
    {
        int random = player.CustomProperties["RandomNumber"] != null ? (int)player.CustomProperties["RandomNumber"] : 0;
        
        _text.text = random.ToString() + " , " + player.NickName;
    }
}
