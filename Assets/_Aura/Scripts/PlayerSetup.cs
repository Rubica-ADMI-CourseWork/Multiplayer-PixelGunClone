using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using System;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
   [SerializeField] TMP_Text playerNameText;
    Camera playerCam;
    private void Awake()
    {
        playerCam = transform.GetComponentInChildren<Camera>();
    }
    private void Start()
    {
        if (photonView.IsMine)
        {
            GetComponent<MovementController>().enabled = true;
            playerCam.enabled = true;
        }
        else
        {
            GetComponent<MovementController>().enabled = false;
            playerCam.enabled = false;

        }

        SetupPlayerUI();
    }

    private void SetupPlayerUI()
    {
        if(playerNameText != null)
        {
            playerNameText.text = photonView.Owner.NickName;
        }  
    }
}
