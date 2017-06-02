using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class name : Photon.MonoBehaviour
{
    public Text ID;
    public InputField input;
    public GameObject Mymain;


    // Use this for initialization
    void Start()
    {
        //input = GetComponent<InputField>();
        //ID = GetComponent<Text>();

    }
    public void Onclickbuttons()
    {
       // photonView.RPC("changename", PhotonTargets.All, input.text);
        ((MonoBehaviour)Mymain.GetComponent("player")).enabled = true;
    }

    /*[PunRPC]
    void changename(string text, PhotonMessageInfo info)
    {
        ID.text = text;
    }*/

    // Update is called once per frame
    void Update()
    {

        if (photonView.isMine)
        {


        }

    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(ID.text);
        }
        else if (stream.isReading)
        {
            ID.text = (string)stream.ReceiveNext();
        }


    }

}