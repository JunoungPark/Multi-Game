using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public InputField RoomName, RoomPerson;
    public Button RoomJoin, RoomCreate;
    public GameObject RoomPrefab;
    public Transform RoomContent;

    Dictionary<string, RoomInfo> RoomCatalog = new Dictionary<string, RoomInfo>();
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {
        if(RoomName.text.Length > 0)
        {
            RoomJoin.interactable = true;
        }
        else
        {
            RoomJoin.interactable = false;
        }
        if (RoomName.text.Length > 0 && RoomPerson.text.Length > 0)
        {
            RoomCreate.interactable = true;
        }
        else
        {
            RoomCreate.interactable= false;
        }
    }
    public void OnClickCreateRoom()
    {
        RoomOptions room = new RoomOptions();

        room.MaxPlayers = byte.Parse(RoomPerson.text);

        room.IsOpen = true;

        room.IsVisible = true;

        PhotonNetwork.CreateRoom(RoomName.text, room);
    }

    public void OnClickJoinRoom()
    {
        PhotonNetwork.JoinRoom(RoomName.text);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Create Room");
    }

    public void AllDeleteRoom()
    {
        foreach (Transform trans in RoomContent)
        {
            Destroy(trans.gameObject);
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }

    public void RoomCreateObject()
    {
        foreach(RoomInfo info in RoomCatalog.Values)
        {
            GameObject room = Instantiate(RoomPrefab);

            room.transform.SetParent(RoomContent);

            room.GetComponent<Information>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }

    public void UpdateRoom(List<RoomInfo> roomList)
    {
        for(int i = 0; i < roomList.Count; i++)
        {
            if (RoomCatalog.ContainsKey(roomList[i].Name))
            {
                if (roomList[i].RemovedFromList)
                {
                    RoomCatalog.Remove(roomList[i].Name);
                    continue;
                }
            }
            RoomCatalog[roomList[i].Name] = roomList[i];
        }
    }

    public override void OnJoinRoomFailed(short returnCode,string message) 
    {
        Debug.Log($"JoinRoom Failed {returnCode} : {message}");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        AllDeleteRoom();
        UpdateRoom(roomList);
        RoomCreateObject();
    }
}
