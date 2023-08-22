using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class RandomCustomPropertyGenerator : MonoBehaviour
{
    [SerializeField]
    private Text _text;
    
    private ExitGames.Client.Photon.Hashtable _customProperties = new ExitGames.Client.Photon.Hashtable();
        
    private void SetCustomNumber()
    {
        System.Random rnd = new System.Random();
        int random = rnd.Next(0, 100);
        
        _text.text = random.ToString();
        
        _customProperties["RandomNumber"] = random;
        PhotonNetwork.LocalPlayer.CustomProperties = _customProperties;
    }
    
    public void OnClick_Button()
    {
        SetCustomNumber();
    }
}
