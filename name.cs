using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class name : Photon.MonoBehaviour {
    public Text ID;
    // 這邊不用，避免和 menu 搶 InputField 物件
    //public InputField input;
    //public GameObject Mymain;

    // Use this for initialization
    void Start() {

    }

    // 按下按鈕 On Click()'s cell back
    public void Onclickbuttons() {
        //photonView.RPC("changename", PhotonTargets.All, input.text);
        // 開啟人物移動
        this.GetComponent<player>().enabled = true;   // 這樣寫就好，上面的 public GameObject Mymain 也拔掉了
        //((MonoBehaviour)Mymain.GetComponent("player")).enabled = true;
    }

    /*[PunRPC]
    void changename(string text, PhotonMessageInfo info)
    {
        nameID.text = text;
    }*/

    // Update is called once per frame
    void Update()
    {
        //if (photonView.isMine) { }
    }

    // 同步名字
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            stream.SendNext(ID.text);
        } else if (stream.isReading) {
            ID.text = (string)stream.ReceiveNext();
        }
    }

}