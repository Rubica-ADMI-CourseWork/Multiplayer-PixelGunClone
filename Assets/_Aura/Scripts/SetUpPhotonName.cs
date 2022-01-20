using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SetUpPhotonName : MonoBehaviour
{
 public void SetupUserName(string userName)
    {
        //null check on input
        if (string.IsNullOrEmpty(userName))
        {
            Debug.Log("Username empty.");
            return;
        }

        //set the username on the photon network.
        PhotonNetwork.NickName = userName;
        
    }
}
