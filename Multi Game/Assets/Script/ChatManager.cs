using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class ChatManager : MonoBehaviourPunCallbacks
{
    public InputField input;

    public GameObject ChatPrefab;

    public Transform ChatContent;
    // Start is called before the first frame update
    public void RpcAddChat(string msg)
    {
        GameObject chat = Instantiate(ChatPrefab);
        chat.GetComponent<Text>().text = msg;

        chat.transform.SetParent(ChatContent);

        input.ActivateInputField();

        input.text = "";
    }

    [PunRPC]
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (input.text.Length == 0) return;

            string chat = PhotonNetwork.NickName + " : " + input.text;

            //photonView.RPC("RpcAddChat", RpcTarget.All, chat);

        }

    }
}
