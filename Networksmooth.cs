using UnityEngine;
using System.Collections;

public class Networksmooth : Photon.MonoBehaviour {
    Vector3 realposition = Vector3.zero;
    Quaternion realrotation = Quaternion.identity;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(photonView.isMine)
        {
            //do nothing
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, realposition, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, realrotation, 0.1f);
        }
	}
    public void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //is our player
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            //someone else
            realposition = (Vector3)stream.ReceiveNext();
            realrotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
