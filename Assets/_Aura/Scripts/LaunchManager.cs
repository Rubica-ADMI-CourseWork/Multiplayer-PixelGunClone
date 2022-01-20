using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class LaunchManager : MonoBehaviourPunCallbacks
{
    #region Fields
    [SerializeField] GameObject enterGamePanel;
    [SerializeField] GameObject connectingPanel;
    [SerializeField] GameObject createRoomPanel;
    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    private void Start()
    {
        enterGamePanel.SetActive(true);
        connectingPanel.SetActive(false);
        createRoomPanel.SetActive(false);
    } 
    #endregion

    #region Photon Callbacks
    public override void OnConnected()
    {
        Debug.Log("Connected to internet");
    }

    public override void OnConnectedToMaster()
    {
        createRoomPanel.SetActive(true);
        connectingPanel.SetActive(false);
        Debug.Log(PhotonNetwork.NickName + " Connected to Photon");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        //create a random room
        CreateRandomGameRoom();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log(PhotonNetwork.NickName + "Has joined " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log(newPlayer.NickName + " Hasjoined "+PhotonNetwork.CurrentRoom.Name);
        Debug.Log(" Player Count: "+ PhotonNetwork.CurrentRoom.PlayerCount);
    }

    #endregion

    #region Public Methods

    public void ConnectToPhoton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            enterGamePanel.SetActive(false);
            connectingPanel.SetActive(true);
        }
    }

    public void CreateJoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    #endregion

    #region Private Methods
    private void CreateRandomGameRoom()
    {
        //random room name
        string randomRoomName = "Room: " + Random.Range(0, 10000);

        //random room roomoptions
        var roomOPs = new RoomOptions();
        roomOPs.MaxPlayers = 20;
        roomOPs.IsOpen = true;
        roomOPs.IsVisible = true;

        //create random room and pass in room options
        PhotonNetwork.CreateRoom(randomRoomName,roomOPs);
    }
    #endregion
}
