using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PhotonControl : MonoBehaviourPun
{
    public float speed = 5.0f;
    public float angleSpeed;

    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            cam.enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;

        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        transform.Translate(dir.normalized * speed * Time.deltaTime);

        transform.eulerAngles += new Vector3(0, Input.GetAxis("Mouse X") * angleSpeed * Time.deltaTime, 0);
    }
}