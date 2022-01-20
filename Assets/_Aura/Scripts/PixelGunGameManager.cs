using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PixelGunGameManager : MonoBehaviourPunCallbacks
{
    #region Fields
    [SerializeField] GameObject playerPrefab;
    #endregion

    #region Singleton
    private static PixelGunGameManager instance;
    public static PixelGunGameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<PixelGunGameManager>();
                if(instance == null)
                {
                    instance = new GameObject().AddComponent<PixelGunGameManager>();
                }
            }
            return instance;
        }
    }
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            if (playerPrefab != null)
            {
                var randomSpawnPosXZ = Random.Range(-30, 30);

                PhotonNetwork.Instantiate(playerPrefab.name,
                    new Vector3(randomSpawnPosXZ, 0, randomSpawnPosXZ),
                    Quaternion.identity);
            }
        }

    }
    #endregion

    #region Public Methods
    public void PlayerDie()
    {
        PhotonNetwork.LeaveRoom();
    }
    #endregion

    #region Photon Callbacks
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log(newPlayer.NickName + " Has entered the " +
            PhotonNetwork.CurrentRoom.Name + " Game Room!");
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        SceneManager.LoadScene(0);
    }
    #endregion
}
